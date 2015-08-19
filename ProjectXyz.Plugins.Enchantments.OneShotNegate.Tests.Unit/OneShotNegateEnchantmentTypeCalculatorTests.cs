using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;
using IMutableEnchantmentCollection = ProjectXyz.Application.Interface.Enchantments.IMutableEnchantmentCollection;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate.Tests.Unit
{
    [DataLayer]
    [Enchantments]
    public class OneShotNegateEnchantmentTypeCalculatorTests
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

            var enchantmentContext = new Mock<IEnchantmentContext>(MockBehavior.Strict);

            var enchantmentTypeCalculator = OneShotNegateEnchantmentTypeCalculator.Create(statusNegationRepository.Object);

            // Execute
            var result = enchantmentTypeCalculator.Calculate(enchantmentContext.Object, stats.Object, enchantments.Object);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.ProcessedEnchantments);
            Assert.Empty(result.RemovedEnchantments);
            Assert.Empty(result.Stats);

            stats.Verify(x => x.GetEnumerator(), Times.Once);

            enchantments.Verify(x => x.GetEnumerator(), Times.Exactly(2));
        }

        [Fact]
        public void Calculate_OneNegationMultipleEnchantments_TwoEnchantmentRemoved()
        {
            // Setup
            var statId = Guid.NewGuid();
            var statusTypeId = Guid.NewGuid();

            var initialStat = new Mock<IStat>(MockBehavior.Strict);
            initialStat
                .Setup(x => x.Id)
                .Returns(statId);

            var stats = new Mock<IStatCollection>(MockBehavior.Strict);
            stats
                .Setup(x => x.GetEnumerator())
                .Returns(new List<IStat>() { initialStat.Object }.GetEnumerator());

            var modifierEnchantment = new Mock<IEnchantment>(MockBehavior.Strict);
            modifierEnchantment
                .Setup(x => x.StatusTypeId)
                .Returns(statusTypeId);

            var untouchedEnchantment = new Mock<IEnchantment>(MockBehavior.Strict);
            untouchedEnchantment
                .Setup(x => x.StatusTypeId)
                .Returns(Guid.NewGuid());

            var negationEnchantment = new Mock<IOneShotNegateEnchantment>(MockBehavior.Strict);
            negationEnchantment
                .Setup(x => x.StatId)
                .Returns(statId);
            negationEnchantment
                .Setup(x => x.WeatherIds)
                .Returns(Enumerable.Empty<Guid>());

            var enchantments = new[] { modifierEnchantment.Object, untouchedEnchantment.Object, negationEnchantment.Object };

            var statusNegation = new Mock<IStatusNegation>(MockBehavior.Strict);
            statusNegation
                .Setup(x => x.EnchantmentStatusId)
                .Returns(statusTypeId);
            statusNegation
                .Setup(x => x.StatId)
                .Returns(statId);

            var statusNegationRepository = new Mock<IStatusNegationRepository>(MockBehavior.Strict);
            statusNegationRepository
                .Setup(x => x.GetAll())
                .Returns(new[] { statusNegation.Object });

            var enchantmentContext = new Mock<IEnchantmentContext>(MockBehavior.Strict);
            
            var enchantmentTypeCalculator = OneShotNegateEnchantmentTypeCalculator.Create(statusNegationRepository.Object);

            // Execute
            var result = enchantmentTypeCalculator.Calculate(enchantmentContext.Object, stats.Object, enchantments);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.ProcessedEnchantments.Count());
            Assert.Contains(negationEnchantment.Object, result.ProcessedEnchantments);
            Assert.Equal(2, result.RemovedEnchantments.Count());
            Assert.Contains(modifierEnchantment.Object, result.RemovedEnchantments);
            Assert.Contains(negationEnchantment.Object, result.RemovedEnchantments);
            Assert.Equal(1, result.Stats.Count);
            Assert.Contains(initialStat.Object, result.Stats);

            initialStat.Verify(x => x.Id, Times.Once);

            stats.Verify(x => x.GetEnumerator(), Times.Once);

            modifierEnchantment.Verify(x => x.StatusTypeId, Times.Exactly(3));

            untouchedEnchantment.Verify(x => x.StatusTypeId, Times.Once);

            negationEnchantment.Verify(x => x.StatId, Times.Once);
            negationEnchantment.Verify(x => x.WeatherIds, Times.Once);

            statusNegation.Verify(x => x.EnchantmentStatusId, Times.Once);
            statusNegation.Verify(x => x.StatId, Times.Once);

            statusNegationRepository.Verify(x => x.GetAll(), Times.Once);
        }
        #endregion
    }
}