using System.Collections.Generic;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Enchantments.Calculations;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Framework.Interface;
using Xunit;

namespace ProjectXyz.Game.Tests.Functional.Enchantments
{
    public sealed class EnchantmentApplierTests
    {
        #region Constants
        private static readonly TestFixture FIXTURE = new TestFixture();
        #endregion

        #region Methods
        private static IEnumerable<object[]> GetSingleEnchantmentNoBaseStatsTheoryData()
        {
            yield return new object[] { FIXTURE.Enchantments.Buffs.StatA, 5 };
            yield return new object[] { FIXTURE.Enchantments.Debuffs.StatA, -5 };
            yield return new object[] { FIXTURE.Enchantments.Buffs.StatB, 5 };
            yield return new object[] { FIXTURE.Enchantments.Buffs.StatC, 10 };
            yield return new object[] { FIXTURE.Enchantments.Debuffs.StatC, 5 };
            yield return new object[] { FIXTURE.Enchantments.PreNullifyStatA, -1 };
            yield return new object[] { FIXTURE.Enchantments.PostNullifyStatA, -1 };
            yield return new object[] { FIXTURE.Enchantments.RecursiveStatA, 0 };
        }

        private static IEnumerable<object[]> GetSingleEnchantmentNoBaseStatsOverTimeTheoryData()
        {
            var doubleDuration = FIXTURE.UnitInterval.Multiply(2);
            var halfDuration = FIXTURE.UnitInterval.Divide(2);

            yield return new object[] { FIXTURE.Enchantments.Buffs.StatA, FIXTURE.UnitInterval, 5 };
            yield return new object[] { FIXTURE.Enchantments.Buffs.StatA, doubleDuration, 5 };
            yield return new object[] { FIXTURE.Enchantments.Buffs.StatA, halfDuration, 5 };
            yield return new object[] { FIXTURE.Enchantments.BuffsOverTime.StatA, FIXTURE.UnitInterval, 10 };
            yield return new object[] { FIXTURE.Enchantments.BuffsOverTime.StatA, doubleDuration, 20 };
            yield return new object[] { FIXTURE.Enchantments.BuffsOverTime.StatA, halfDuration, 5 };
        }
        #endregion

        #region Tests
        [Theory,
         MemberData(nameof(GetSingleEnchantmentNoBaseStatsTheoryData))]
        private void ApplyEnchantments_SingleEnchantmentNoBaseStats_SingleStatExpectedValue(
            IEnchantment enchantment,
            double expectedValue)
        {
            var baseStats = new Dictionary<IIdentifier, double>();
            var enchantmentCalculatorContext = EnchantmentCalculatorContext.None.WithEnchantments(enchantment);
            var result = FIXTURE.EnchantmentApplier.ApplyEnchantments(
                enchantmentCalculatorContext,
                baseStats);

            Assert.Equal(1, result.Count);
            Assert.Equal(expectedValue, result[enchantment.StatDefinitionId]);
        }

        [Theory,
         MemberData(nameof(GetSingleEnchantmentNoBaseStatsOverTimeTheoryData))]
        private void ApplyEnchantments_SingleEnchantmentNoBaseStatsOverTime_SingleStatExpectedValue(
            IEnchantment enchantment,
            IInterval elapsed,
            double expectedValue)
        {
            var baseStats = new Dictionary<IIdentifier, double>();
            var enchantmentCalculatorContext = EnchantmentCalculatorContext.None
                .WithElapsed(elapsed)
                .WithEnchantments(enchantment);
            var result = FIXTURE.EnchantmentApplier.ApplyEnchantments(
                enchantmentCalculatorContext,
                baseStats);

            Assert.Equal(1, result.Count);
            Assert.Equal(expectedValue, result[enchantment.StatDefinitionId]);
        }
        #endregion
    }
}
