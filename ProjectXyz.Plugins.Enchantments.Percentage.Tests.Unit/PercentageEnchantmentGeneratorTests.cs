using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Enchantments.Percentage.Tests.Unit
{
    [ApplicationLayer]
    [Enchantments]
    public class AditiveEnchantmentGeneratorTests
    {
        #region Methods
        [Fact]
        public void Generate_ValidArguments_Success()
        {
            // SETUP
            Guid STAT_ID = Guid.NewGuid();
            Guid STATUS_TYPE_ID = Guid.NewGuid();
            Guid TRIGGER_ID = Guid.NewGuid();

            var randomizer = new Mock<IRandom>();
            randomizer
                .SetupSequence(x => x.NextDouble())
                .Returns(0.5)
                .Returns(0.5);

            var enchantment = new Mock<IPercentageEnchantment>();

            var enchantmentDefinition = new Mock<IPercentageEnchantmentDefinition>();
            enchantmentDefinition
                .Setup(x => x.StatusTypeId)
                .Returns(STATUS_TYPE_ID);
            enchantmentDefinition
                .Setup(x => x.StatId)
                .Returns(STAT_ID);
            enchantmentDefinition
                .Setup(x => x.TriggerId)
                .Returns(TRIGGER_ID);
            enchantmentDefinition
                .Setup(x => x.MinimumValue)
                .Returns(0);
            enchantmentDefinition
                .Setup(x => x.MaximumValue)
                .Returns(100);
            enchantmentDefinition
                .Setup(x => x.MinimumDuration)
                .Returns(TimeSpan.FromSeconds(0));
            enchantmentDefinition
                .Setup(x => x.MaximumDuration)
                .Returns(TimeSpan.FromSeconds(2));
            
            var percentageEnchantmentFactory = new Mock<IPercentageEnchantmentFactory>();
            percentageEnchantmentFactory
                .Setup(x => x.Create(It.IsAny<Guid>(), STATUS_TYPE_ID, TRIGGER_ID, TimeSpan.FromSeconds(1), STAT_ID, 50))
                .Returns(enchantment.Object);

            var enchantmentGenerator = PercentageEnchantmentGenerator.Create(percentageEnchantmentFactory.Object);

            // Execute
            var result = enchantmentGenerator.Generate(
                randomizer.Object,
                enchantmentDefinition.Object);

            // Assert
            Assert.Equal(enchantment.Object, result);

            randomizer.Verify(x => x.NextDouble(), Times.Exactly(2));

            enchantmentDefinition.Verify(x => x.StatusTypeId, Times.Once);
            enchantmentDefinition.Verify(x => x.StatId, Times.Once);
            enchantmentDefinition.Verify(x => x.TriggerId, Times.Once);
            enchantmentDefinition.Verify(x => x.MinimumValue, Times.Exactly(2));
            enchantmentDefinition.Verify(x => x.MaximumValue, Times.Once);
            enchantmentDefinition.Verify(x => x.MinimumDuration, Times.Exactly(2));
            enchantmentDefinition.Verify(x => x.MaximumDuration, Times.Once);
            
            percentageEnchantmentFactory.Verify(
                x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<TimeSpan>(),
                    It.IsAny<Guid>(),
                    It.IsAny<double>()),
                Times.Once);
        }
        #endregion
    }
}