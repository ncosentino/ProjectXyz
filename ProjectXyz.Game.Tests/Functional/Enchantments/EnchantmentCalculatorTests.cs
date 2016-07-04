using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Shared.Stats;
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
        #endregion

        #region Tests
        [Theory,
         MemberData("GetSingleEnchantmentNoBaseStatsTheoryData")]
        private void Calculate_SingleEnchantmentNoBaseStats_ExpectedResult(
            IEnchantment enchantment,
            double expectedResult)
        {
            var baseStats = StatCollection.Empty;
            var result = FIXTURE.EnchantmentCalculator.Calculate(
                StateContextProvider.Empty,
                baseStats,
                enchantment.AsArray(),
                enchantment.StatDefinitionId);

            Assert.Equal(expectedResult, result);
        }

        [Theory,
         MemberData("GetMultipleEnchantmentsNoBaseStatsTheoryData")]
        private void Calculate_MultipleEnchantmentsNoBaseStats_ExpectedResult(
            IIdentifier statDefinitionId,
            IEnumerable<IEnchantment> enchantments,
            double expectedResult)
        {
            var baseStats = StatCollection.Empty;
            var result = FIXTURE.EnchantmentCalculator.Calculate(
                StateContextProvider.Empty,
                baseStats,
                enchantments.ToArray(),
                statDefinitionId);

            Assert.Equal(expectedResult, result);
        }

        [Theory,
         MemberData("GetTimeOfDayEnchantmentsTheoryData")]
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

            var baseStats = StatCollection.Empty;
            var result = FIXTURE.EnchantmentCalculator.Calculate(
                stateContextProvider,
                baseStats,
                enchantment.AsArray(),
                enchantment.StatDefinitionId);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        private void Calculate_BadExpression_ThrowsFormatException()
        {
            var baseStats = StatCollection.Empty;

            Action method = () => FIXTURE.EnchantmentCalculator.Calculate(
                StateContextProvider.Empty,
                baseStats,
                FIXTURE.Enchantments.BadExpression.AsArray(),
                FIXTURE.Enchantments.BadExpression.StatDefinitionId);

            Assert.Throws<FormatException>(method);
        }
        #endregion
    }
}
