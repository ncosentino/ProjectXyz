using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Enchantments.Calculations;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;
using Xunit;

namespace ProjectXyz.Game.Tests.Functional.Enchantments
{
    public sealed class EnchantmentCalculatorTests
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

        private static IEnumerable<object[]> GetMultipleEnchantmentsNoBaseStatsTheoryData()
        {
            yield return new object[] { FIXTURE.Stats.StatA, FIXTURE.Enchantments.Buffs.StatA.Yield().Append(FIXTURE.Enchantments.Buffs.StatA), 10 };
            yield return new object[] { FIXTURE.Stats.StatA, FIXTURE.Enchantments.Debuffs.StatA.Yield().Append(FIXTURE.Enchantments.Debuffs.StatA), -10 };
            yield return new object[] { FIXTURE.Stats.StatA, FIXTURE.Enchantments.Buffs.StatA.Yield().Append(FIXTURE.Enchantments.Debuffs.StatA), 0 };
            yield return new object[] { FIXTURE.Stats.StatA, FIXTURE.Enchantments.Buffs.StatA.Yield().Append(FIXTURE.Enchantments.PreNullifyStatA), 4 };
            yield return new object[] { FIXTURE.Stats.StatA, FIXTURE.Enchantments.PostNullifyStatA.Yield().Append(FIXTURE.Enchantments.Buffs.StatA), -1 };
            yield return new object[] { FIXTURE.Stats.StatB, FIXTURE.Enchantments.Buffs.StatB.Yield().Append(FIXTURE.Enchantments.Buffs.StatA), 5 };
        }

        private static IEnumerable<object[]> GetTimeOfDayEnchantmentsTheoryData()
        {
            yield return new object[] { FIXTURE.Enchantments.DayTimeBuffs.StatABinary, 0, 0 };
            yield return new object[] { FIXTURE.Enchantments.DayTimeBuffs.StatABinary, 0.5, 10 };
            yield return new object[] { FIXTURE.Enchantments.DayTimeBuffs.StatABinary, 1, 10 };
            yield return new object[] { FIXTURE.Enchantments.DayTimeBuffs.StatAPeak, 0, 0 };
            yield return new object[] { FIXTURE.Enchantments.DayTimeBuffs.StatAPeak, 0.5, 5 };
            yield return new object[] { FIXTURE.Enchantments.DayTimeBuffs.StatAPeak, 1, 10 };
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
        private void Calculate_SingleEnchantmentNoBaseStats_ExpectedResult(
            IEnchantment enchantment,
            double expectedResult)
        {
            var baseStats = new Dictionary<IIdentifier, double>();
            var enchantmentCalculatorContext = EnchantmentCalculatorContext.None.WithEnchantments(enchantment);
            var result = FIXTURE.EnchantmentCalculator.Calculate(
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
            var enchantmentCalculatorContext = EnchantmentCalculatorContext.None.WithEnchantments(enchantments);
            var result = FIXTURE.EnchantmentCalculator.Calculate(
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
                    FIXTURE.States.States.TimeOfDay,
                    new ConstantValueStateContext(new Dictionary<IIdentifier, double>()
                    {
                        { FIXTURE.States.TimeOfDay.Day, percentActiveState }
                    })
                },
            });

            var enchantmentCalculatorContext = EnchantmentCalculatorContext.None
                .WithStateContextProvider(stateContextProvider)
                .WithEnchantments(enchantment);

            var baseStats = new Dictionary<IIdentifier, double>();
            var result = FIXTURE.EnchantmentCalculator.Calculate(
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
            var enchantmentCalculatorContext = EnchantmentCalculatorContext.None
                .WithElapsed(elapsed)
                .WithEnchantments(enchantment);
            var result = FIXTURE.EnchantmentCalculator.Calculate(
                enchantmentCalculatorContext,
                baseStats,
                enchantment.StatDefinitionId);

            Assert.Equal(expectedValue, result);
        }

        [Fact]
        private void Calculate_BadExpression_ThrowsFormatException()
        {
            var baseStats = new Dictionary<IIdentifier, double>();
            var enchantmentCalculatorContext = EnchantmentCalculatorContext.None.WithEnchantments(FIXTURE.Enchantments.BadExpression);

            Action method = () => FIXTURE.EnchantmentCalculator.Calculate(
                enchantmentCalculatorContext,
                baseStats,
                FIXTURE.Enchantments.BadExpression.StatDefinitionId);

            Assert.Throws<FormatException>(method);
        }
        #endregion
    }
}
