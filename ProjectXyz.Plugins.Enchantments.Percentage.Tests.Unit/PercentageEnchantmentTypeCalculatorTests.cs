using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Enchantments.Percentage.Tests.Unit
{
    [DataLayer]
    [Enchantments]
    public class PercentageEnchantmentTypeCalculatorTests
    {
        #region Methods
        [Fact]
        public void Calculate_NoEnchantments_NoDifference()
        {
            // Setup
            var stats = new Mock<IStatCollection>(MockBehavior.Strict);
            stats
                .Setup(x => x.GetEnumerator())
                .Returns(Enumerable.Empty<IStat>().GetEnumerator());

            var enchantments = new Mock<IMutableEnchantmentCollection>(MockBehavior.Strict);
            enchantments
                .Setup(x => x.GetEnumerator())
                .Returns(Enumerable.Empty<IEnchantment>().GetEnumerator());

            var statFactory = new Mock<IStatFactory>(MockBehavior.Strict);

            var enchantmentContext = new Mock<IEnchantmentContext>(MockBehavior.Strict);

            var enchantmentTypeCalculator = PercentageEnchantmentTypeCalculator.Create(statFactory.Object);

            // Execute
            var result = enchantmentTypeCalculator.Calculate(enchantmentContext.Object, stats.Object, enchantments.Object);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.ProcessedEnchantments);
            Assert.Empty(result.RemovedEnchantments);
            Assert.Empty(result.Stats);

            stats.Verify(x => x.GetEnumerator(), Times.Once);

            enchantments.Verify(x => x.GetEnumerator(), Times.Once);
        }
        
        [Fact]
        public void Calculate_MultipleEnchantmentNoStat_OneStatZeroValue()
        {
            // Setup
            var statId = Guid.NewGuid();
            const double VALUE1 = 1.5;
            const double VALUE2 = 0.75;

            var stats = new Mock<IStatCollection>(MockBehavior.Strict);
            stats
                .Setup(x => x.GetEnumerator())
                .Returns(Enumerable.Empty<IStat>().GetEnumerator());

            var enchantment1 = new Mock<IPercentageEnchantment>(MockBehavior.Strict);
            enchantment1
                .Setup(x => x.StatId)
                .Returns(statId);
            enchantment1
                .Setup(x => x.Value)
                .Returns(VALUE1);
            enchantment1
                .Setup(x => x.WeatherIds)
                .Returns(Enumerable.Empty<Guid>());

            var enchantment2 = new Mock<IPercentageEnchantment>(MockBehavior.Strict);
            enchantment2
                .Setup(x => x.StatId)
                .Returns(statId);
            enchantment2
                .Setup(x => x.Value)
                .Returns(VALUE2);
            enchantment2
                .Setup(x => x.WeatherIds)
                .Returns(Enumerable.Empty<Guid>());

            var enchantments = new IEnchantment[] { enchantment1.Object, enchantment2.Object };

            var stat1 = new Mock<IStat>(MockBehavior.Strict);
            stat1
                .Setup(x => x.Value)
                .Returns(0);

            var stat2 = new Mock<IStat>(MockBehavior.Strict);
            stat2
                .Setup(x => x.Id)
                .Returns(statId);

            var statFactory = new Mock<IStatFactory>(MockBehavior.Strict);
            statFactory
                .SetupSequence(x => x.CreateStat(statId, 0))
                .Returns(stat1.Object)
                .Returns(stat2.Object);

            var enchantmentContext = new Mock<IEnchantmentContext>(MockBehavior.Strict);

            var enchantmentTypeCalculator = PercentageEnchantmentTypeCalculator.Create(statFactory.Object);

            // Execute
            var result = enchantmentTypeCalculator.Calculate(enchantmentContext.Object, stats.Object, enchantments);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.ProcessedEnchantments.Count());
            Assert.Contains(enchantment1.Object, result.ProcessedEnchantments);
            Assert.Contains(enchantment2.Object, result.ProcessedEnchantments);
            Assert.Empty(result.RemovedEnchantments);
            Assert.Equal(1, result.Stats.Count());
            Assert.Contains(stat2.Object, result.Stats);

            stats.Verify(x => x.GetEnumerator(), Times.Once);

            enchantment1.Verify(x => x.StatId, Times.AtLeastOnce);
            enchantment1.Verify(x => x.Value, Times.Once);
            enchantment1.Verify(x => x.WeatherIds, Times.Once);
            enchantment2.Verify(x => x.StatId, Times.AtLeastOnce);
            enchantment2.Verify(x => x.Value, Times.Once);
            enchantment2.Verify(x => x.WeatherIds, Times.Once);

            stat1.Verify(x => x.Value, Times.Once);
            stat2.Verify(x => x.Id, Times.Once);

            statFactory.Verify(x => x.CreateStat(It.IsAny<Guid>(), 0), Times.Exactly(2));
        }

        [Fact]
        public void Calculate_MultipleEnchantmentOneStat_ExpectedStatValue()
        {
            // Setup
            var statId = Guid.NewGuid();
            const double INITIAL_VALUE = 5;
            const double MULTIPLIER1 = 1.5;
            const double MULTIPLIER2 = 0.75;

            var initialStat = new Mock<IStat>(MockBehavior.Strict);
            initialStat
                .Setup(x => x.Value)
                .Returns(5);
            initialStat
                .Setup(x => x.Id)
                .Returns(statId);

            var stats = new Mock<IStatCollection>(MockBehavior.Strict);
            stats
                .Setup(x => x.GetEnumerator())
                .Returns(new List<IStat>() { initialStat.Object }.GetEnumerator());

            var enchantment1 = new Mock<IPercentageEnchantment>(MockBehavior.Strict);
            enchantment1
                .Setup(x => x.StatId)
                .Returns(statId);
            enchantment1
                .Setup(x => x.Value)
                .Returns(MULTIPLIER1);
            enchantment1
                .Setup(x => x.WeatherIds)
                .Returns(Enumerable.Empty<Guid>());

            var enchantment2 = new Mock<IPercentageEnchantment>(MockBehavior.Strict);
            enchantment2
                .Setup(x => x.StatId)
                .Returns(statId);
            enchantment2
                .Setup(x => x.Value)
                .Returns(MULTIPLIER2);
            enchantment2
                .Setup(x => x.WeatherIds)
                .Returns(Enumerable.Empty<Guid>());

            var enchantments = new IEnchantment[] { enchantment1.Object, enchantment2.Object };

            var stat1 = new Mock<IStat>(MockBehavior.Strict);
            stat1
                .Setup(x => x.Value)
                .Returns(INITIAL_VALUE * MULTIPLIER1);

            var stat2 = new Mock<IStat>(MockBehavior.Strict);
            stat2
                .Setup(x => x.Id)
                .Returns(statId);

            var statFactory = new Mock<IStatFactory>(MockBehavior.Strict);
            statFactory
                .Setup(x => x.CreateStat(statId, INITIAL_VALUE * MULTIPLIER1))
                .Returns(stat1.Object);
            statFactory
                .Setup(x => x.CreateStat(statId, INITIAL_VALUE * MULTIPLIER1 * MULTIPLIER2))
                .Returns(stat2.Object);

            var enchantmentContext = new Mock<IEnchantmentContext>(MockBehavior.Strict);

            var enchantmentTypeCalculator = PercentageEnchantmentTypeCalculator.Create(statFactory.Object);

            // Execute
            var result = enchantmentTypeCalculator.Calculate(enchantmentContext.Object, stats.Object, enchantments);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.ProcessedEnchantments.Count());
            Assert.Contains(enchantment1.Object, result.ProcessedEnchantments);
            Assert.Contains(enchantment2.Object, result.ProcessedEnchantments);
            Assert.Empty(result.RemovedEnchantments);
            Assert.Equal(1, result.Stats.Count());
            Assert.Contains(stat2.Object, result.Stats);

            stats.Verify(x => x.GetEnumerator(), Times.Once);

            enchantment1.Verify(x => x.StatId, Times.AtLeastOnce);
            enchantment1.Verify(x => x.Value, Times.Once);
            enchantment1.Verify(x => x.WeatherIds, Times.Once);
            enchantment2.Verify(x => x.StatId, Times.AtLeastOnce);
            enchantment2.Verify(x => x.Value, Times.Once);
            enchantment2.Verify(x => x.WeatherIds, Times.Once);

            stat1.Verify(x => x.Value, Times.Once);
            stat2.Verify(x => x.Id, Times.Once);

            statFactory.Verify(x => x.CreateStat(It.IsAny<Guid>(), It.IsAny<double>()), Times.Exactly(2));
        }
        #endregion
    }
}