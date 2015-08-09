using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Enchantments.Calculations;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Application.Tests.Enchantments.Mocks;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Application.Tests.Enchantments
{
    [ApplicationLayer]
    [Enchantments]
    public class EnchantmentCalculatorTests
    {
        [Fact]
        public void Calculate_SingleEnchantmentSingleCalculator_ReturnsResult()
        {
            // Setup
            var enchantment = new MockAdditiveEnchantmentBuilder()
                .WithStatId(ActorStats.MaximumLife)
                .WithValue(10)
                .Build();

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

            var enchantmentTypeCalculator = new Mock<IEnchantmentTypeCalculator>(MockBehavior.Strict);
            enchantmentTypeCalculator
                .Setup(x => x.Calculate(stats, new[] { enchantment }))
                .Returns(enchantmentTypeCalculatorResult.Object);

            var enchantmentCalculatorResult = new Mock<IEnchantmentCalculatorResult>(MockBehavior.Strict);

            var enchantmentCalculatorResultFactory = new Mock<IEnchantmentCalculatorResultFactory>(MockBehavior.Strict);
            enchantmentCalculatorResultFactory
                .Setup(x => x.Create(new[] { enchantment }, stats))
                .Returns(enchantmentCalculatorResult.Object);

            var calculator = EnchantmentCalculator.Create(
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
        }
    }
}