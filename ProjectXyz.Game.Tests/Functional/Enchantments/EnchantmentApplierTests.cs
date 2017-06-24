using System.Collections.Generic;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Application.Enchantments.Api;
using ProjectXyz.Application.Enchantments.Core.Calculations;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Game.Tests.Functional.TestingData;
using Xunit;

namespace ProjectXyz.Game.Tests.Functional.Enchantments
{
    public sealed class EnchantmentApplierTests
    {
        #region Constants
        private static readonly TestData TEST_DATA = new TestData();
        #endregion

        #region Fields
        private readonly TestFixture _fixture;
        #endregion

        #region Constructors
        public EnchantmentApplierTests()
        {
            _fixture = new TestFixture(TEST_DATA);
        }
        #endregion

        #region Methods
        public static IEnumerable<object[]> GetSingleEnchantmentNoBaseStatsTheoryData()
        {
            yield return new object[] { TEST_DATA.Enchantments.Buffs.StatA, 5 };
            yield return new object[] { TEST_DATA.Enchantments.Debuffs.StatA, -5 };
            yield return new object[] { TEST_DATA.Enchantments.Buffs.StatB, 5 };
            yield return new object[] { TEST_DATA.Enchantments.Buffs.StatC, 10 };
            yield return new object[] { TEST_DATA.Enchantments.Debuffs.StatC, 5 };
            yield return new object[] { TEST_DATA.Enchantments.PreNullifyStatA, -1 };
            yield return new object[] { TEST_DATA.Enchantments.PostNullifyStatA, -1 };
            yield return new object[] { TEST_DATA.Enchantments.RecursiveStatA, 0 };
        }

        public static IEnumerable<object[]> GetSingleEnchantmentNoBaseStatsOverTimeTheoryData()
        {
            var doubleDuration = TEST_DATA.UnitInterval.Multiply(2);
            var halfDuration = TEST_DATA.UnitInterval.Divide(2);

            yield return new object[] { TEST_DATA.Enchantments.Buffs.StatA, TEST_DATA.UnitInterval, 5 };
            yield return new object[] { TEST_DATA.Enchantments.Buffs.StatA, doubleDuration, 5 };
            yield return new object[] { TEST_DATA.Enchantments.Buffs.StatA, halfDuration, 5 };
            yield return new object[] { TEST_DATA.Enchantments.BuffsOverTime.StatA, TEST_DATA.UnitInterval, 10 };
            yield return new object[] { TEST_DATA.Enchantments.BuffsOverTime.StatA, doubleDuration, 20 };
            yield return new object[] { TEST_DATA.Enchantments.BuffsOverTime.StatA, halfDuration, 5 };
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
            var enchantmentCalculatorContext = EnchantmentCalculatorContext
                .None
                .WithEnchantments(enchantment)
                .WithComponent(TEST_DATA.StatesPlugin.StateContextProvider);
            var result = _fixture.EnchantmentApplier.ApplyEnchantments(
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
            var enchantmentCalculatorContext = EnchantmentCalculatorContext
                .None
                .WithElapsed(elapsed)
                .WithEnchantments(enchantment)
                .WithComponent(TEST_DATA.StatesPlugin.StateContextProvider);
            var result = _fixture.EnchantmentApplier.ApplyEnchantments(
                enchantmentCalculatorContext,
                baseStats);

            Assert.Equal(1, result.Count);
            Assert.Equal(expectedValue, result[enchantment.StatDefinitionId]);
        }
        #endregion
    }
}
