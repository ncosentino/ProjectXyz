using System;
using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.States;
using ProjectXyz.Application.States.Core;
using ProjectXyz.Game.Tests.Functional.TestingData;
using ProjectXyz.Plugins.Enchantments.Calculations.State;
using ProjectXyz.Shared.Framework.Entities;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Calculations;
using Xunit;

namespace ProjectXyz.Game.Tests.Functional.Enchantments
{
    public sealed class EnchantmentCalculatorTests
    {
        #region Constants
        private static readonly TestData TEST_DATA = new TestData();
        #endregion

        #region Fields
        private readonly TestFixture _fixture;
        #endregion

        #region Constructors
        public EnchantmentCalculatorTests()
        {
            _fixture = new TestFixture(TEST_DATA);
        }
        #endregion

        #region Methods
        public static IEnumerable<object[]> GetSingleEnchantmentNoBaseStatsTheoryData()
        {
            yield return new object[] { "Addition A", TEST_DATA.Enchantments.Buffs.StatAAdditiveBaseStat, 5 };
            yield return new object[] { "Subtraction", TEST_DATA.Enchantments.Debuffs.StatAAdditiveBaseStat, -5 };
            yield return new object[] { "Addition B", TEST_DATA.Enchantments.Buffs.StatBAdditiveBaseStat, 5 };
            yield return new object[] { "Addition C", TEST_DATA.Enchantments.Buffs.StatCAdditiveBaseStat, 10 };
            yield return new object[] { "Subtraction C", TEST_DATA.Enchantments.Debuffs.StatCAdditiveBaseStat, 5 };
            yield return new object[] { "Pre-nullify", TEST_DATA.Enchantments.PreNullifyStatABaseStat, -1 };
            yield return new object[] { "Post-nullify", TEST_DATA.Enchantments.PostNullifyStatABaseStat, -1 };
            yield return new object[] { "Recursive", TEST_DATA.Enchantments.RecursiveStatABaseStat, 0 };
        }

        public static IEnumerable<object[]> GetMultipleEnchantmentsNoBaseStatsTheoryData()
        {
            yield return new object[] { "Two additions", TEST_DATA.Stats.DefinitionIds.StatA, TEST_DATA.Enchantments.Buffs.StatAAdditiveBaseStat.Yield().Append(TEST_DATA.Enchantments.Buffs.StatAAdditiveBaseStat), 10 };
            yield return new object[] { "Two subtractions", TEST_DATA.Stats.DefinitionIds.StatA, TEST_DATA.Enchantments.Debuffs.StatAAdditiveBaseStat.Yield().Append(TEST_DATA.Enchantments.Debuffs.StatAAdditiveBaseStat), -10 };
            yield return new object[] { "Add then subtract", TEST_DATA.Stats.DefinitionIds.StatA, TEST_DATA.Enchantments.Buffs.StatAAdditiveBaseStat.Yield().Append(TEST_DATA.Enchantments.Debuffs.StatAAdditiveBaseStat), 0 };
            yield return new object[] { "Pre-nullify then add", TEST_DATA.Stats.DefinitionIds.StatA, TEST_DATA.Enchantments.Buffs.StatAAdditiveBaseStat.Yield().Append(TEST_DATA.Enchantments.PreNullifyStatABaseStat), 4 };
            yield return new object[] { "Add then pre-nullify", TEST_DATA.Stats.DefinitionIds.StatA, TEST_DATA.Enchantments.PreNullifyStatABaseStat.Yield().Append(TEST_DATA.Enchantments.Buffs.StatAAdditiveBaseStat), 4 };
            yield return new object[] { "Post-nullify then add", TEST_DATA.Stats.DefinitionIds.StatA, TEST_DATA.Enchantments.PostNullifyStatABaseStat.Yield().Append(TEST_DATA.Enchantments.Buffs.StatAAdditiveBaseStat), -1 };
            yield return new object[] { "Add then post-nullify", TEST_DATA.Stats.DefinitionIds.StatA, TEST_DATA.Enchantments.Buffs.StatAAdditiveBaseStat.Yield().Append(TEST_DATA.Enchantments.PostNullifyStatABaseStat), -1 };
            yield return new object[] { "Addition for two different stats", TEST_DATA.Stats.DefinitionIds.StatB, TEST_DATA.Enchantments.Buffs.StatBAdditiveBaseStat.Yield().Append(TEST_DATA.Enchantments.Buffs.StatAAdditiveBaseStat), 5 };
            yield return new object[] { "Add then multiply", TEST_DATA.Stats.DefinitionIds.StatA, TEST_DATA.Enchantments.Buffs.StatAAdditiveBaseStat.Yield().Append(TEST_DATA.Enchantments.Buffs.StatAMultiplicativeBaseStat), 10 };
            yield return new object[] { "Multiply then add", TEST_DATA.Stats.DefinitionIds.StatA, TEST_DATA.Enchantments.Buffs.StatAMultiplicativeBaseStat.Yield().Append(TEST_DATA.Enchantments.Buffs.StatAAdditiveBaseStat), 5 };
        }

        public static IEnumerable<object[]> GetTimeOfDayEnchantmentsTheoryData()
        {
            yield return new object[] { "Over zero 0% active", TEST_DATA.Enchantments.DayTimeBuffs.StatAOverZero, 0, 0 };
            yield return new object[] { "Over zero 50% active", TEST_DATA.Enchantments.DayTimeBuffs.StatAOverZero, 0.5, 10 };
            yield return new object[] { "Over zero 100% active", TEST_DATA.Enchantments.DayTimeBuffs.StatAOverZero, 1, 10 };
            yield return new object[] { "Binary 0% active", TEST_DATA.Enchantments.DayTimeBuffs.StatABinary, 0, 0 };
            yield return new object[] { "Binary 50% active", TEST_DATA.Enchantments.DayTimeBuffs.StatABinary, 0.5, 0 };
            yield return new object[] { "Binary 100% active", TEST_DATA.Enchantments.DayTimeBuffs.StatABinary, 1, 10 };
            yield return new object[] { "Peak 0% active", TEST_DATA.Enchantments.DayTimeBuffs.StatAPeak, 0, 0 };
            yield return new object[] { "Peak 50% active", TEST_DATA.Enchantments.DayTimeBuffs.StatAPeak, 0.5, 5 };
            yield return new object[] { "Peak 100% active", TEST_DATA.Enchantments.DayTimeBuffs.StatAPeak, 1, 10 };
        }

        public static IEnumerable<object[]> GetSingleEnchantmentNoBaseStatsOverTimeTheoryData()
        {
            var doubleDuration = TEST_DATA.UnitInterval.Multiply(2);
            var halfDuration = TEST_DATA.UnitInterval.Divide(2);

            yield return new object[] { "Additive zero interval", TEST_DATA.Enchantments.Buffs.StatAAdditiveBaseStat, TEST_DATA.ZeroInterval, 5 };
            yield return new object[] { "Additive unit interval", TEST_DATA.Enchantments.Buffs.StatAAdditiveBaseStat, TEST_DATA.UnitInterval, 5 };
            yield return new object[] { "Additve two unit intervals", TEST_DATA.Enchantments.Buffs.StatAAdditiveBaseStat, doubleDuration, 5 };
            yield return new object[] { "Additive half unit interval", TEST_DATA.Enchantments.Buffs.StatAAdditiveBaseStat, halfDuration, 5 };
            yield return new object[] { "Base stat additive-over-time zero interval", TEST_DATA.Enchantments.BuffsOverTime.StatABaseStat, TEST_DATA.ZeroInterval, 0 };
            yield return new object[] { "Base stat additive-over-time unit interval", TEST_DATA.Enchantments.BuffsOverTime.StatABaseStat, TEST_DATA.UnitInterval, 10 };
            yield return new object[] { "Base stat additive-over-time two unit intervals", TEST_DATA.Enchantments.BuffsOverTime.StatABaseStat, doubleDuration, 20 };
            yield return new object[] { "Base stat additive-over-time half interval", TEST_DATA.Enchantments.BuffsOverTime.StatABaseStat, halfDuration, 5 };
        }
        #endregion

        #region Tests
        [Theory,
         MemberData(nameof(GetSingleEnchantmentNoBaseStatsTheoryData))]
        private void Calculate_SingleEnchantmentNoBaseStats(
            string _,
            IEnchantment enchantment,
            double expectedResult)
        {
            var baseStats = new Dictionary<IIdentifier, double>();
            var enchantmentCalculatorContext = EnchantmentCalculatorContext
                .None
                .WithEnchantments(enchantment)
                .WithComponent(new GenericComponent<IStateContextProvider>(_fixture.StateContextProvider));
            var result = _fixture.EnchantmentCalculator.Calculate(
                enchantmentCalculatorContext,
                baseStats,
                enchantment.StatDefinitionId);

            Assert.Equal(expectedResult, result);
        }

        [Theory,
         MemberData(nameof(GetMultipleEnchantmentsNoBaseStatsTheoryData))]
        private void Calculate_MultipleEnchantmentsNoBaseStats(
            string _,
            IIdentifier statDefinitionId,
            IEnumerable<IEnchantment> enchantments,
            double expectedResult)
        {
            var baseStats = new Dictionary<IIdentifier, double>();
            var enchantmentCalculatorContext = EnchantmentCalculatorContext
                .None
                .WithEnchantments(enchantments)
                .WithComponent(new GenericComponent<IStateContextProvider>(_fixture.StateContextProvider));
            var result = _fixture.EnchantmentCalculator.Calculate(
                enchantmentCalculatorContext,
                baseStats, 
                statDefinitionId);

            Assert.Equal(expectedResult, result);
        }

        [Theory,
         MemberData(nameof(GetTimeOfDayEnchantmentsTheoryData))]
        private void Calculate_TimeOfDayEnchantmentNoBaseStats(
            string _,
            IEnchantment enchantment,
            double percentActiveState,
            double expectedResult)
        {
            var stateContextProvider = new StateContextProvider(new Dictionary<IIdentifier, IStateContext>()
            {
                {
                    TEST_DATA.States.States.TimeOfDay,
                    new ConstantValueStateContext(new Dictionary<IIdentifier, double>()
                    {
                        { TEST_DATA.States.TimeOfDay.Day, percentActiveState }
                    })
                },
            });

            var enchantmentCalculatorContext = EnchantmentCalculatorContext
                .None
                .WithComponent(new GenericComponent<IStateContextProvider>(stateContextProvider))
                .WithEnchantments(enchantment);

            var baseStats = new Dictionary<IIdentifier, double>();
            var result = _fixture.EnchantmentCalculator.Calculate(
                enchantmentCalculatorContext,
                baseStats,
                enchantment.StatDefinitionId);

            Assert.Equal(expectedResult, result);
        }

        [Theory,
         MemberData(nameof(GetSingleEnchantmentNoBaseStatsOverTimeTheoryData))]
        private void ApplyEnchantments_SingleEnchantmentNoBaseStatsOverTime(
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
            var result = _fixture.EnchantmentCalculator.Calculate(
                enchantmentCalculatorContext,
                baseStats,
                enchantment.StatDefinitionId);

            Assert.Equal(expectedValue, result);
        }

        [Fact]
        private void Calculate_BadExpression_ThrowsFormatException()
        {
            var baseStats = new Dictionary<IIdentifier, double>();
            var enchantmentCalculatorContext = EnchantmentCalculatorContext
                .None
                .WithEnchantments(TEST_DATA.Enchantments.BadExpression)
                .WithComponent(new GenericComponent<IStateContextProvider>(_fixture.StateContextProvider));

            Action method = () => _fixture.EnchantmentCalculator.Calculate(
                enchantmentCalculatorContext,
                baseStats,
                TEST_DATA.Enchantments.BadExpression.StatDefinitionId);

            Assert.Throws<FormatException>(method);
        }
        #endregion
    }
}
