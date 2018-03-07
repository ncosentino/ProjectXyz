using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.States;
using ProjectXyz.Application.Enchantments.Core.Calculations;
using ProjectXyz.Framework.Entities.Shared;
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
            yield return new object[] { "", TEST_DATA.Enchantments.Buffs.StatAAdditiveBaseStat, 5 };
            yield return new object[] { "", TEST_DATA.Enchantments.Debuffs.StatAAdditiveBaseStat, -5 };
            yield return new object[] { "", TEST_DATA.Enchantments.Buffs.StatBAdditiveBaseStat, 5 };
            yield return new object[] { "", TEST_DATA.Enchantments.Buffs.StatCAdditiveBaseStat, 10 };
            yield return new object[] { "", TEST_DATA.Enchantments.Debuffs.StatCAdditiveBaseStat, 5 };
            yield return new object[] { "", TEST_DATA.Enchantments.PreNullifyStatABaseStat, -1 };
            yield return new object[] { "", TEST_DATA.Enchantments.PostNullifyStatABaseStat, -1 };
            yield return new object[] { "", TEST_DATA.Enchantments.RecursiveStatABaseStat, 0 };
        }

        public static IEnumerable<object[]> GetSingleEnchantmentNoBaseStatsOverTimeTheoryData()
        {
            var doubleDuration = TEST_DATA.UnitInterval.Multiply(2);
            var halfDuration = TEST_DATA.UnitInterval.Divide(2);

            yield return new object[] { "", TEST_DATA.Enchantments.Buffs.StatAAdditiveBaseStat, TEST_DATA.UnitInterval, 5 };
            yield return new object[] { "", TEST_DATA.Enchantments.Buffs.StatAAdditiveBaseStat, doubleDuration, 5 };
            yield return new object[] { "", TEST_DATA.Enchantments.Buffs.StatAAdditiveBaseStat, halfDuration, 5 };
            yield return new object[] { "", TEST_DATA.Enchantments.BuffsOverTime.StatABaseStat, TEST_DATA.UnitInterval, 10 };
            yield return new object[] { "", TEST_DATA.Enchantments.BuffsOverTime.StatABaseStat, doubleDuration, 20 };
            yield return new object[] { "", TEST_DATA.Enchantments.BuffsOverTime.StatABaseStat, halfDuration, 5 };
        }
        #endregion

        #region Tests
        [Theory,
         MemberData(nameof(GetSingleEnchantmentNoBaseStatsTheoryData))]
        private void ApplyEnchantments_SingleEnchantmentNoBaseStats_SingleStatExpectedValue(
            string _,
            IEnchantment enchantment,
            double expectedValue)
        {
            var baseStats = new Dictionary<IIdentifier, double>();
            var enchantmentCalculatorContext = EnchantmentCalculatorContext
                .None
                .WithEnchantments(enchantment)
                .WithComponent(new GenericComponent<IStateContextProvider>(_fixture.StateContextProvider));
            var result = _fixture.EnchantmentApplier.ApplyEnchantments(
                enchantmentCalculatorContext,
                baseStats);

            Assert.Equal(1, result.Count);
            Assert.Equal(expectedValue, result[enchantment.StatDefinitionId]);
        }

        [Theory,
         MemberData(nameof(GetSingleEnchantmentNoBaseStatsOverTimeTheoryData))]
        private void ApplyEnchantments_SingleEnchantmentNoBaseStatsOverTime_SingleStatExpectedValue(
            string _,
            IEnchantment enchantment,
            IInterval elapsed,
            double expectedValue)
        {
            var baseStats = new Dictionary<IIdentifier, double>();
            var enchantmentCalculatorContext = EnchantmentCalculatorContext
                .None
                .WithElapsed(elapsed)
                .WithEnchantments(enchantment)
                .WithComponent(new GenericComponent<IStateContextProvider>(_fixture.StateContextProvider));
            var result = _fixture.EnchantmentApplier.ApplyEnchantments(
                enchantmentCalculatorContext,
                baseStats);

            Assert.Equal(1, result.Count);
            Assert.Equal(expectedValue, result[enchantment.StatDefinitionId]);
        }
        #endregion
    }
}
