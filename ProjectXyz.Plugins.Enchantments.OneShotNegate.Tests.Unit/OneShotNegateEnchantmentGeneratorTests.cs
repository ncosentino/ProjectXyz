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
            var statId = Guid.NewGuid();
            var statusTypeId = Guid.NewGuid();
            var triggerId = Guid.NewGuid();
            var enchantmentWeatherId = Guid.NewGuid();

            var randomizer = new Mock<IRandom>(MockBehavior.Strict);

            var enchantment = new Mock<IOneShotNegateEnchantment>(MockBehavior.Strict);

            var oneShotNegateEnchantmentDefinition = new Mock<IOneShotNegateEnchantmentDefinition>(MockBehavior.Strict);
            oneShotNegateEnchantmentDefinition
                .Setup(x => x.StatId)
                .Returns(statId);

            var enchantmentDefinition = new Mock<IEnchantmentDefinition>(MockBehavior.Strict);
            enchantmentDefinition
                .Setup(x => x.StatusTypeId)
                .Returns(statusTypeId);
            enchantmentDefinition
                .Setup(x => x.TriggerId)
                .Returns(triggerId);
            enchantmentDefinition
                .Setup(x => x.EnchantmentWeatherId)
                .Returns(enchantmentWeatherId);

            var oneShotNegateEnchantmentFactory = new Mock<IOneShotNegateEnchantmentFactory>(MockBehavior.Strict);
            oneShotNegateEnchantmentFactory
                .Setup(x => x.Create(It.IsAny<Guid>(), statusTypeId, triggerId, Enumerable.Empty<Guid>(), statId))
                .Returns(enchantment.Object);

            var enchantmentWeather = new Mock<IEnchantmentWeather>(MockBehavior.Strict);
            enchantmentWeather
                .Setup(x => x.WeatherIds)
                .Returns(Enumerable.Empty<Guid>());

            var enchantmentWeatherRepository = new Mock<IEnchantmentWeatherRepository>(MockBehavior.Strict);
            enchantmentWeatherRepository
                .Setup(x => x.GetById(enchantmentWeatherId))
                .Returns(enchantmentWeather.Object);

            var enchantmentGenerator = OneShotNegateEnchantmentGenerator.Create(
                oneShotNegateEnchantmentFactory.Object,
                enchantmentWeatherRepository.Object);

            // Execute
            var result = enchantmentGenerator.Generate(
                randomizer.Object,
                enchantmentDefinition.Object,
                oneShotNegateEnchantmentDefinition.Object);

            // Assert
            Assert.Equal(enchantment.Object, result);
            
            oneShotNegateEnchantmentDefinition.Verify(x => x.StatId, Times.Once);

            enchantmentDefinition.Verify(x => x.StatusTypeId, Times.Once);            
            enchantmentDefinition.Verify(x => x.TriggerId, Times.Once);
            enchantmentDefinition.Verify(x => x.EnchantmentWeatherId, Times.Once);
            
            oneShotNegateEnchantmentFactory.Verify(
                x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<IEnumerable<Guid>>(),
                    It.IsAny<Guid>()),
                Times.Once);

            enchantmentWeather.Verify(x => x.WeatherIds, Times.Once);

            enchantmentWeatherRepository.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        }
        #endregion
    }
}