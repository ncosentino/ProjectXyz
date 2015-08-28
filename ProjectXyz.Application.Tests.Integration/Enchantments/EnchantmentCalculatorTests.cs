using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Enchantments.Calculations;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Plugins.Enchantments.Expression;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Application.Tests.Integration.Enchantments
{
    [ApplicationLayer]
    [Enchantments]
    public class EnchantmentCalculatorTests
    {
        [Fact]
        public void Calculate_SingleEnchantmentSingleCalculator_ReturnsResult()
        {
            // Setup
            var enchantment = ExpressionEnchantment.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Enumerable.Empty<Guid>(),
                TimeSpan.Zero,
                ActorStats.MaximumLife,
                "VALUE",
                0,
                Enumerable.Empty<KeyValuePair<string, Guid>>(),
                new[] { new KeyValuePair<string, double>("VALUE", 10) });

            var stats = StatCollection.Create();

            var enchantmentTypeCalculatorResult = new Mock<IEnchantmentTypeCalculatorResult>(MockBehavior.Strict);
            enchantmentTypeCalculatorResult
                .Setup(x => x.ProcessedEnchantments)
                .Returns(new[] { enchantment });
            enchantmentTypeCalculatorResult
                .Setup(x => x.RemovedEnchantments)
                .Returns(Enumerable.Empty<IEnchantment>());
            enchantmentTypeCalculatorResult
                .Setup(x => x.Stats)
                .Returns(stats);

            var enchantmentContext = new Mock<IEnchantmentContext>(MockBehavior.Strict);

            var enchantmentTypeCalculator = new Mock<IEnchantmentTypeCalculator>(MockBehavior.Strict);
            enchantmentTypeCalculator
                .Setup(x => x.Calculate(enchantmentContext.Object, stats, new[] { enchantment }))
                .Returns(enchantmentTypeCalculatorResult.Object);

            var enchantmentCalculatorResult = new Mock<IEnchantmentCalculatorResult>(MockBehavior.Strict);

            var enchantmentCalculatorResultFactory = new Mock<IEnchantmentCalculatorResultFactory>(MockBehavior.Strict);
            enchantmentCalculatorResultFactory
                .Setup(x => x.Create(new[] { enchantment }, stats))
                .Returns(enchantmentCalculatorResult.Object);

            var calculator = EnchantmentCalculator.Create(
                enchantmentContext.Object,
                enchantmentCalculatorResultFactory.Object,
                new[]
                {
                    enchantmentTypeCalculator.Object,
                });

            // Execute
            var result = calculator.Calculate(
                stats, 
                new[]
                {
                    enchantment,
                });

            // Assert
            Assert.Equal(enchantmentCalculatorResult.Object, result);

            enchantmentTypeCalculatorResult.Verify(x => x.ProcessedEnchantments, Times.Once);
            enchantmentTypeCalculatorResult.Verify(x => x.RemovedEnchantments, Times.Exactly(2));
            enchantmentTypeCalculatorResult.Verify(x => x.Stats, Times.Once);

            enchantmentTypeCalculator.Verify(x => x.Calculate(It.IsAny<IEnchantmentContext>(), It.IsAny<IStatCollection>(), It.IsAny<IEnumerable<IEnchantment>>()), Times.Once);

            enchantmentCalculatorResultFactory.Verify(x => x.Create(It.IsAny<IEnumerable<IEnchantment>>(), It.IsAny<IStatCollection>()));
        }
    }
}