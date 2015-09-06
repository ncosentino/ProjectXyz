using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Weather;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate.Tests.Unit
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

            var weatherTypeGroupingRepository = new Mock<IWeatherGroupingRepository>(MockBehavior.Strict);

            var enchantmentTypeCalculatorResult = new Mock<IEnchantmentTypeCalculatorResult>(MockBehavior.Strict);

            var enchantmentTypeCalculatorResultFactory = new Mock<IEnchantmentTypeCalculatorResultFactory>(MockBehavior.Strict);
            enchantmentTypeCalculatorResultFactory
                .Setup(x => x.Create(
                    It.IsAny<IEnumerable<IEnchantment>>(),
                    It.IsAny<IEnumerable<IEnchantment>>(),
                    It.Is<IStatCollection>(s => !s.Any())))
                .Returns(enchantmentTypeCalculatorResult.Object);

            var enchantmentContext = new Mock<IEnchantmentContext>(MockBehavior.Strict);

            var enchantmentTypeCalculator = OneShotNegateEnchantmentTypeCalculator.Create(
                statusNegationRepository.Object,
                weatherTypeGroupingRepository.Object,
                enchantmentTypeCalculatorResultFactory.Object);

            // Execute
            var result = enchantmentTypeCalculator.Calculate(enchantmentContext.Object, stats.Object, enchantments.Object);

            // Assert
            Assert.Equal(enchantmentTypeCalculatorResult.Object, result);

            stats.Verify(x => x.GetEnumerator(), Times.Once);

            enchantments.Verify(x => x.GetEnumerator(), Times.Once);
        }
        #endregion
    }
}