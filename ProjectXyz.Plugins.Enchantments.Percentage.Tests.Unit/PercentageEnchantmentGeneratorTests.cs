﻿using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Enchantments.Percentage.Tests.Unit
{
    [ApplicationLayer]
    [Enchantments]
    public class PercentageEnchantmentGeneratorTests
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
            randomizer
                .SetupSequence(x => x.NextDouble())
                .Returns(0.5)
                .Returns(0.5);

            var enchantment = new Mock<IPercentageEnchantment>(MockBehavior.Strict);

            var percentageEnchantmentDefinition = new Mock<IPercentageEnchantmentDefinition>(MockBehavior.Strict);
            percentageEnchantmentDefinition
                .Setup(x => x.StatId)
                .Returns(statId);
            percentageEnchantmentDefinition
                .Setup(x => x.MinimumValue)
                .Returns(0);
            percentageEnchantmentDefinition
                .Setup(x => x.MaximumValue)
                .Returns(100);

            var enchantmentDefinition = new Mock<IEnchantmentDefinition>(MockBehavior.Strict);
            enchantmentDefinition
                .Setup(x => x.StatusTypeId)
                .Returns(statusTypeId);
            enchantmentDefinition
                .Setup(x => x.TriggerId)
                .Returns(triggerId);
            enchantmentDefinition
                .Setup(x => x.MinimumDuration)
                .Returns(TimeSpan.FromSeconds(0));
            enchantmentDefinition
                .Setup(x => x.MaximumDuration)
                .Returns(TimeSpan.FromSeconds(2));
            enchantmentDefinition
                .Setup(x => x.EnchantmentWeatherId)
                .Returns(enchantmentWeatherId);

            var percentageEnchantmentFactory = new Mock<IPercentageEnchantmentFactory>(MockBehavior.Strict);
            percentageEnchantmentFactory
                .Setup(x => x.Create(It.IsAny<Guid>(), statusTypeId, triggerId, Enumerable.Empty<Guid>(), TimeSpan.FromSeconds(1), statId, 50))
                .Returns(enchantment.Object);

            var enchantmentWeather = new Mock<IEnchantmentWeather>(MockBehavior.Strict);
            enchantmentWeather
                .Setup(x => x.WeatherIds)
                .Returns(Enumerable.Empty<Guid>());

            var enchantmentWeatherRepository = new Mock<IEnchantmentWeatherRepository>(MockBehavior.Strict);
            enchantmentWeatherRepository
                .Setup(x => x.GetById(enchantmentWeatherId))
                .Returns(enchantmentWeather.Object);

            var enchantmentGenerator = PercentageEnchantmentGenerator.Create(
                percentageEnchantmentFactory.Object,
                enchantmentWeatherRepository.Object);

            // Execute
            var result = enchantmentGenerator.Generate(
                randomizer.Object,
                enchantmentDefinition.Object,
                percentageEnchantmentDefinition.Object);

            // Assert
            Assert.Equal(enchantment.Object, result);

            randomizer.Verify(x => x.NextDouble(), Times.Exactly(2));

            percentageEnchantmentDefinition.Verify(x => x.StatId, Times.Once);
            percentageEnchantmentDefinition.Verify(x => x.MinimumValue, Times.Exactly(2));
            percentageEnchantmentDefinition.Verify(x => x.MaximumValue, Times.Once);

            enchantmentDefinition.Verify(x => x.StatusTypeId, Times.Once);            
            enchantmentDefinition.Verify(x => x.TriggerId, Times.Once);
            enchantmentDefinition.Verify(x => x.MinimumDuration, Times.Exactly(2));
            enchantmentDefinition.Verify(x => x.MaximumDuration, Times.Once);
            enchantmentDefinition.Verify(x => x.EnchantmentWeatherId, Times.Once);
            
            percentageEnchantmentFactory.Verify(
                x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<IEnumerable<Guid>>(),
                    It.IsAny<TimeSpan>(),
                    It.IsAny<Guid>(),
                    It.IsAny<double>()),
                Times.Once);

            enchantmentWeather.Verify(x => x.WeatherIds, Times.Once);

            enchantmentWeatherRepository.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        }
        #endregion
    }
}