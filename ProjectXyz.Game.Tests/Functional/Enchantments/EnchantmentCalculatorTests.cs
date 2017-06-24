using System;
using System.Collections.Generic;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.States;
using ProjectXyz.Application.Enchantments.Api;
using ProjectXyz.Application.Enchantments.Core.Calculations;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;
using ProjectXyz.Game.Tests.Functional.TestingData;
using ProjectXyz.Plugins.Enchantments.Calculations.State;
using ProjectXyz.Plugins.States.Simple;
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
            yield return new object[] { TEST_DATA.Enchantments.Buffs.StatA, 5 };
            yield return new object[] { TEST_DATA.Enchantments.Debuffs.StatA, -5 };
            yield return new object[] { TEST_DATA.Enchantments.Buffs.StatB, 5 };
            yield return new object[] { TEST_DATA.Enchantments.Buffs.StatC, 10 };
            yield return new object[] { TEST_DATA.Enchantments.Debuffs.StatC, 5 };
            yield return new object[] { TEST_DATA.Enchantments.PreNullifyStatA, -1 };
            yield return new object[] { TEST_DATA.Enchantments.PostNullifyStatA, -1 };
            yield return new object[] { TEST_DATA.Enchantments.RecursiveStatA, 0 };
        }

        public static IEnumerable<object[]> GetMultipleEnchantmentsNoBaseStatsTheoryData()
        {
            yield return new object[] { TEST_DATA.Stats.DefinitionIds.StatA, TEST_DATA.Enchantments.Buffs.StatA.Yield().Append(TEST_DATA.Enchantments.Buffs.StatA), 10 };
            yield return new object[] { TEST_DATA.Stats.DefinitionIds.StatA, TEST_DATA.Enchantments.Debuffs.StatA.Yield().Append(TEST_DATA.Enchantments.Debuffs.StatA), -10 };
            yield return new object[] { TEST_DATA.Stats.DefinitionIds.StatA, TEST_DATA.Enchantments.Buffs.StatA.Yield().Append(TEST_DATA.Enchantments.Debuffs.StatA), 0 };
            yield return new object[] { TEST_DATA.Stats.DefinitionIds.StatA, TEST_DATA.Enchantments.Buffs.StatA.Yield().Append(TEST_DATA.Enchantments.PreNullifyStatA), 4 };
            yield return new object[] { TEST_DATA.Stats.DefinitionIds.StatA, TEST_DATA.Enchantments.PostNullifyStatA.Yield().Append(TEST_DATA.Enchantments.Buffs.StatA), -1 };
            yield return new object[] { TEST_DATA.Stats.DefinitionIds.StatB, TEST_DATA.Enchantments.Buffs.StatB.Yield().Append(TEST_DATA.Enchantments.Buffs.StatA), 5 };
        }

        public static IEnumerable<object[]> GetTimeOfDayEnchantmentsTheoryData()
        {
            yield return new object[] { TEST_DATA.Enchantments.DayTimeBuffs.StatABinary, 0, 0 };
            yield return new object[] { TEST_DATA.Enchantments.DayTimeBuffs.StatABinary, 0.5, 10 };
            yield return new object[] { TEST_DATA.Enchantments.DayTimeBuffs.StatABinary, 1, 10 };
            yield return new object[] { TEST_DATA.Enchantments.DayTimeBuffs.StatAPeak, 0, 0 };
            yield return new object[] { TEST_DATA.Enchantments.DayTimeBuffs.StatAPeak, 0.5, 5 };
            yield return new object[] { TEST_DATA.Enchantments.DayTimeBuffs.StatAPeak, 1, 10 };
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
        private void Calculate_SingleEnchantmentNoBaseStats_ExpectedResult(
            IEnchantment enchantment,
            double expectedResult)
        {
            var baseStats = new Dictionary<IIdentifier, double>();
            var enchantmentCalculatorContext = EnchantmentCalculatorContext
                .None
                .WithEnchantments(enchantment)
                .WithComponent(TEST_DATA.StatesPlugin.StateContextProvider);
            var result = _fixture.EnchantmentCalculator.Calculate(
                enchantmentCalculatorContext,
                baseStats,
                enchantment.StatDefinitionId);

            Assert.Equal(expectedResult, result);
        }

        [Theory,
         MemberData(nameof(GetMultipleEnchantmentsNoBaseStatsTheoryData))]
        private void Calculate_MultipleEnchantmentsNoBaseStats_ExpectedResult(
            IIdentifier statDefinitionId,
            IEnumerable<IEnchantment> enchantments,
            double expectedResult)
        {
            var baseStats = new Dictionary<IIdentifier, double>();
            var enchantmentCalculatorContext = EnchantmentCalculatorContext
                .None
                .WithEnchantments(enchantments)
                .WithComponent(TEST_DATA.StatesPlugin.StateContextProvider);
            var result = _fixture.EnchantmentCalculator.Calculate(
                enchantmentCalculatorContext,
                baseStats, 
                statDefinitionId);

            Assert.Equal(expectedResult, result);
        }

        [Theory,
         MemberData(nameof(GetTimeOfDayEnchantmentsTheoryData))]
        private void Calculate_TimeOfDayEnchantmentNoBaseStats_ExpectedResult(
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
                .WithComponent(stateContextProvider)
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
                .WithComponent(TEST_DATA.StatesPlugin.StateContextProvider);

            Action method = () => _fixture.EnchantmentCalculator.Calculate(
                enchantmentCalculatorContext,
                baseStats,
                TEST_DATA.Enchantments.BadExpression.StatDefinitionId);

            Assert.Throws<FormatException>(method);
        }
        #endregion
    }
}
