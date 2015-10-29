using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate.Tests.Unit
{
    [ApplicationLayer]
    [Enchantments]
    public class OneShotNegateEnchantmentGeneratorTests
    {
        #region Methods
        [Fact]
        public void Generate_ValidArguments_Success()
        {
            // Setup
            var enchantmentDefinitionId = Guid.NewGuid();
            var statId = Guid.NewGuid();
            var statusTypeId = Guid.NewGuid();
            var triggerId = Guid.NewGuid();
            var enchantmentTypeId = Guid.NewGuid();
            var weatherTypeGroupingId = Guid.NewGuid();

            var randomizer = new Mock<IRandom>(MockBehavior.Strict);

            var enchantment = new Mock<IOneShotNegateEnchantment>(MockBehavior.Strict);

            var oneShotNegateEnchantmentDefinition = new Mock<IOneShotNegateEnchantmentDefinition>(MockBehavior.Strict);
            oneShotNegateEnchantmentDefinition
                .Setup(x => x.StatId)
                .Returns(statId);

            var enchantmentDefinition = new Mock<IEnchantmentDefinition>(MockBehavior.Strict);
            enchantmentDefinition
                .Setup(x => x.Id)
                .Returns(enchantmentDefinitionId);
            enchantmentDefinition
                .Setup(x => x.StatusTypeId)
                .Returns(statusTypeId);
            enchantmentDefinition
                .Setup(x => x.TriggerId)
                .Returns(triggerId);
            enchantmentDefinition
                .Setup(x => x.EnchantmentTypeId)
                .Returns(enchantmentTypeId);

            var oneShotNegateEnchantmentFactory = new Mock<IOneShotNegateEnchantmentFactory>(MockBehavior.Strict);
            oneShotNegateEnchantmentFactory
                .Setup(x => x.Create(It.IsAny<Guid>(), statusTypeId, triggerId, enchantmentTypeId, weatherTypeGroupingId, statId))
                .Returns(enchantment.Object);

            var enchantmentWeather = new Mock<IEnchantmentDefinitionWeatherGrouping>(MockBehavior.Strict);
            enchantmentWeather
                .Setup(x => x.WeatherTypeGroupingId)
                .Returns(weatherTypeGroupingId);

            var enchantmentDefinitionWeatherGroupingRepository = new Mock<IEnchantmentDefinitionWeatherTypeGroupingRepository>(MockBehavior.Strict);
            enchantmentDefinitionWeatherGroupingRepository
                .Setup(x => x.GetByEnchantmentDefinitionId(enchantmentDefinitionId))
                .Returns(enchantmentWeather.Object);

            var enchantmentGenerator = OneShotNegateEnchantmentGenerator.Create(
                oneShotNegateEnchantmentFactory.Object,
                enchantmentDefinitionWeatherGroupingRepository.Object);

            // Execute
            var result = enchantmentGenerator.Generate(
                randomizer.Object,
                enchantmentDefinition.Object,
                oneShotNegateEnchantmentDefinition.Object);

            // Assert
            Assert.Equal(enchantment.Object, result);
            
            oneShotNegateEnchantmentDefinition.Verify(x => x.StatId, Times.Once);

            enchantmentDefinition.Verify(x => x.Id, Times.Once);
            enchantmentDefinition.Verify(x => x.StatusTypeId, Times.Once);            
            enchantmentDefinition.Verify(x => x.TriggerId, Times.Once);
            enchantmentDefinition.Verify(x => x.EnchantmentTypeId, Times.Once);

            oneShotNegateEnchantmentFactory.Verify(
                x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>()),
                Times.Once);

            enchantmentWeather.Verify(x => x.WeatherTypeGroupingId, Times.Once);
            
            enchantmentDefinitionWeatherGroupingRepository.Verify(x => x.GetByEnchantmentDefinitionId(It.IsAny<Guid>()), Times.Once);
        }
        #endregion
    }
}