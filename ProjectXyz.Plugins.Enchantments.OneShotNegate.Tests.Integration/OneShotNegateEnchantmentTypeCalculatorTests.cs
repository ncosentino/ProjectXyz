using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Enchantments.Calculations;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Weather;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate.Tests.Integration
{
    [ApplicationLayer]
    [Enchantments]
    public class OneShotNegateEnchantmentTypeCalculatorTests
    {
        #region Methods
        [Fact]
        public void Calculate_NoEnchantments_NoDifference()
        {
            // Setup
            var stats = StatCollection.Create();

            var enchantments = EnchantmentCollection.Create();

            var statusNegation = new Mock<IStatusNegation>(MockBehavior.Strict);
            statusNegation
                .Setup(x => x.EnchantmentStatusId)
                .Returns(Guid.NewGuid());
            statusNegation
                .Setup(x => x.StatId)
                .Returns(Guid.NewGuid());

            var statusNegationRepository = new Mock<IStatusNegationRepository>(MockBehavior.Strict);
            statusNegationRepository
                .Setup(x => x.GetAll())
                .Returns(new[] { statusNegation.Object });

            var weatherTypeGroupingRepository = new Mock<IWeatherGroupingRepository>(MockBehavior.Strict);

            var enchantmentTypeCalculatorResultFactory = EnchantmentTypeCalculatorResultFactory.Create();

            var enchantmentContext = new Mock<IEnchantmentContext>(MockBehavior.Strict);

            var enchantmentTypeCalculator = OneShotNegateEnchantmentTypeCalculator.Create(
                statusNegationRepository.Object,
                weatherTypeGroupingRepository.Object,
                enchantmentTypeCalculatorResultFactory);

            // Execute
            var result = enchantmentTypeCalculator.Calculate(enchantmentContext.Object, stats, enchantments);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.ProcessedEnchantments);
            Assert.Empty(result.RemovedEnchantments);
            Assert.Empty(result.Stats);
        }

        [Fact]
        public void Calculate_OneNegationMultipleEnchantments_TwoEnchantmentRemoved()
        {
            // Setup
            var statDefinitionId = Guid.NewGuid();
            var statusTypeId = Guid.NewGuid();
            var weatherTypeGroupingId = Guid.NewGuid();

            var initialStat = Stat.Create(Guid.NewGuid(), statDefinitionId, 0);

            var stats = StatCollection.Create(new[] { initialStat });

            var modifierEnchantment = new Mock<IEnchantment>(MockBehavior.Strict);
            modifierEnchantment
                .Setup(x => x.StatusTypeId)
                .Returns(statusTypeId);
            modifierEnchantment
                .Setup(x => x.WeatherGroupingId)
                .Returns(weatherTypeGroupingId);

            var untouchedEnchantment = new Mock<IEnchantment>(MockBehavior.Strict);
            untouchedEnchantment
                .Setup(x => x.StatusTypeId)
                .Returns(Guid.NewGuid());
            untouchedEnchantment
                .Setup(x => x.WeatherGroupingId)
                .Returns(weatherTypeGroupingId);

            var negationEnchantment = new Mock<IOneShotNegateEnchantment>(MockBehavior.Strict);
            negationEnchantment
                .Setup(x => x.StatId)
                .Returns(statDefinitionId);
            negationEnchantment
                .Setup(x => x.WeatherGroupingId)
                .Returns(weatherTypeGroupingId);

            var enchantments = EnchantmentCollection.Create(new List<IEnchantment>()
            {
                modifierEnchantment.Object, 
                untouchedEnchantment.Object,
                negationEnchantment.Object
            });

            var statusNegation = StatusNegation.Create(Guid.NewGuid(), statDefinitionId, statusTypeId);

            var statusNegationRepository = new Mock<IStatusNegationRepository>(MockBehavior.Strict);
            statusNegationRepository
                .Setup(x => x.GetAll())
                .Returns(new[] { statusNegation });

            var weatherTypeGroupingRepository = new Mock<IWeatherGroupingRepository>(MockBehavior.Strict);
            weatherTypeGroupingRepository
                .Setup(x => x.GetByGroupingId(weatherTypeGroupingId))
                .Returns(new IWeatherGrouping[0]);

            var enchantmentTypeCalculatorResultFactory = EnchantmentTypeCalculatorResultFactory.Create();

            var enchantmentContext = new Mock<IEnchantmentContext>(MockBehavior.Strict);
            
            var enchantmentTypeCalculator = OneShotNegateEnchantmentTypeCalculator.Create(
                statusNegationRepository.Object,
                weatherTypeGroupingRepository.Object,
                enchantmentTypeCalculatorResultFactory);

            // Execute
            var result = enchantmentTypeCalculator.Calculate(enchantmentContext.Object, stats, enchantments);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.ProcessedEnchantments.Count());
            Assert.Contains(negationEnchantment.Object, result.ProcessedEnchantments);
            Assert.Equal(2, result.RemovedEnchantments.Count());
            Assert.Contains(modifierEnchantment.Object, result.RemovedEnchantments);
            Assert.Contains(negationEnchantment.Object, result.RemovedEnchantments);
            Assert.Equal(1, result.Stats.Count);
            Assert.Contains(initialStat, result.Stats);

            modifierEnchantment.Verify(x => x.StatusTypeId, Times.Exactly(2));
            modifierEnchantment.Verify(x => x.WeatherGroupingId, Times.Once);

            untouchedEnchantment.Verify(x => x.StatusTypeId, Times.Once);
            untouchedEnchantment.Verify(x => x.WeatherGroupingId, Times.Once);

            negationEnchantment.Verify(x => x.StatId, Times.Once);
            negationEnchantment.Verify(x => x.WeatherGroupingId, Times.Once);

            statusNegationRepository.Verify(x => x.GetAll(), Times.Once);

            weatherTypeGroupingRepository.Verify(x => x.GetByGroupingId(It.IsAny<Guid>()), Times.Exactly(3));
        }

        [Fact]
        public void Calculate_OneNegationWithNothingToNegate_OneShotStillApplies()
        {
            // Setup
            var statDefinitionId = Guid.NewGuid();
            var statusTypeId = Guid.NewGuid();
            var weatherTypeGroupingId = Guid.NewGuid();

            var stats = new Mock<IStatCollection>(MockBehavior.Strict);
            stats
                .Setup(x => x.GetEnumerator())
                .Returns(new List<IStat>().GetEnumerator());

            var negationEnchantment = new Mock<IOneShotNegateEnchantment>(MockBehavior.Strict);
            negationEnchantment
                .Setup(x => x.StatId)
                .Returns(statDefinitionId);
            negationEnchantment
                .Setup(x => x.WeatherGroupingId)
                .Returns(weatherTypeGroupingId);

            var enchantments = new Mock<IEnumerable<IEnchantment>>(MockBehavior.Strict);
            enchantments
                .Setup(x => x.GetEnumerator())
                .Returns(new List<IEnchantment>()
                {
                    negationEnchantment.Object
                }.GetEnumerator());

            var statusNegation = StatusNegation.Create(Guid.NewGuid(), statDefinitionId, statusTypeId);

            var statusNegationRepository = new Mock<IStatusNegationRepository>(MockBehavior.Strict);
            statusNegationRepository
                .Setup(x => x.GetAll())
                .Returns(new[] { statusNegation });

            var weatherTypeGroupingRepository = new Mock<IWeatherGroupingRepository>(MockBehavior.Strict);
            weatherTypeGroupingRepository
                .Setup(x => x.GetByGroupingId(weatherTypeGroupingId))
                .Returns(new IWeatherGrouping[0]);

            var enchantmentTypeCalculatorResultFactory = EnchantmentTypeCalculatorResultFactory.Create();

            var enchantmentContext = new Mock<IEnchantmentContext>(MockBehavior.Strict);

            var enchantmentTypeCalculator = OneShotNegateEnchantmentTypeCalculator.Create(
                statusNegationRepository.Object, 
                weatherTypeGroupingRepository.Object,
                enchantmentTypeCalculatorResultFactory);

            // Execute
            var result = enchantmentTypeCalculator.Calculate(enchantmentContext.Object, stats.Object, enchantments.Object);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.ProcessedEnchantments.Count());
            Assert.Contains(negationEnchantment.Object, result.ProcessedEnchantments);
            Assert.Equal(1, result.RemovedEnchantments.Count());
            Assert.Contains(negationEnchantment.Object, result.RemovedEnchantments);
            Assert.Empty(result.Stats);

            stats.Verify(x => x.GetEnumerator(), Times.Once);

            negationEnchantment.Verify(x => x.StatId, Times.Once);
            negationEnchantment.Verify(x => x.WeatherGroupingId, Times.Once);

            enchantments.Verify(x => x.GetEnumerator(), Times.Once);

            statusNegationRepository.Verify(x => x.GetAll(), Times.Once);

            weatherTypeGroupingRepository.Verify(x => x.GetByGroupingId(It.IsAny<Guid>()), Times.Once);
        }
        #endregion
    }
}