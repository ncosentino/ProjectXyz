using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.States;
using ProjectXyz.Game.Tests.Functional.TestingData;
using ProjectXyz.Plugins.Enchantments.Calculations.State;
using ProjectXyz.Shared.Framework.Entities;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations;
using ProjectXyz.Shared.States;
using Xunit;

namespace ProjectXyz.Game.Tests.Functional.GameObjects.Enchantments
{
    public sealed class EnchantmentCalculatorTests
    {
        private static readonly TestData _testData;
        private static readonly TestFixture _fixture;

        static EnchantmentCalculatorTests()
        {
            _testData = new TestData();
            _fixture = new TestFixture(_testData);
        }

        public static IEnumerable<object[]> GetSingleEnchantmentNoBaseStatsTheoryData()
        {
            yield return new object[] { "Addition A", _testData.Enchantments.Buffs.StatAAdditiveBaseStat, 5 };
            yield return new object[] { "Subtraction", _testData.Enchantments.Debuffs.StatAAdditiveBaseStat, -5 };
            yield return new object[] { "Addition B", _testData.Enchantments.Buffs.StatBAdditiveBaseStat, 5 };
            yield return new object[] { "Addition C", _testData.Enchantments.Buffs.StatCAdditiveBaseStat, 10 };
            yield return new object[] { "Subtraction C", _testData.Enchantments.Debuffs.StatCAdditiveBaseStat, 5 };
            yield return new object[] { "Pre-nullify", _testData.Enchantments.PreNullifyStatABaseStat, -1 };
            yield return new object[] { "Post-nullify", _testData.Enchantments.PostNullifyStatABaseStat, -1 };
            yield return new object[] { "Recursive", _testData.Enchantments.RecursiveStatABaseStat, 0 };
        }

        public static IEnumerable<object[]> GetMultipleEnchantmentsNoBaseStatsTheoryData()
        {
            yield return new object[] { "Two additions", _testData.Stats.DefinitionIds.StatA, _testData.Enchantments.Buffs.StatAAdditiveBaseStat.Yield().AppendSingle(_testData.Enchantments.Buffs.StatAAdditiveBaseStat), 10 };
            yield return new object[] { "Two subtractions", _testData.Stats.DefinitionIds.StatA, _testData.Enchantments.Debuffs.StatAAdditiveBaseStat.Yield().AppendSingle(_testData.Enchantments.Debuffs.StatAAdditiveBaseStat), -10 };
            yield return new object[] { "Add then subtract", _testData.Stats.DefinitionIds.StatA, _testData.Enchantments.Buffs.StatAAdditiveBaseStat.Yield().AppendSingle(_testData.Enchantments.Debuffs.StatAAdditiveBaseStat), 0 };
            yield return new object[] { "Pre-nullify then add", _testData.Stats.DefinitionIds.StatA, _testData.Enchantments.Buffs.StatAAdditiveBaseStat.Yield().AppendSingle(_testData.Enchantments.PreNullifyStatABaseStat), 4 };
            yield return new object[] { "Add then pre-nullify", _testData.Stats.DefinitionIds.StatA, _testData.Enchantments.PreNullifyStatABaseStat.Yield().AppendSingle(_testData.Enchantments.Buffs.StatAAdditiveBaseStat), 4 };
            yield return new object[] { "Post-nullify then add", _testData.Stats.DefinitionIds.StatA, _testData.Enchantments.PostNullifyStatABaseStat.Yield().AppendSingle(_testData.Enchantments.Buffs.StatAAdditiveBaseStat), -1 };
            yield return new object[] { "Add then post-nullify", _testData.Stats.DefinitionIds.StatA, _testData.Enchantments.Buffs.StatAAdditiveBaseStat.Yield().AppendSingle(_testData.Enchantments.PostNullifyStatABaseStat), -1 };
            yield return new object[] { "Addition for two different stats", _testData.Stats.DefinitionIds.StatB, _testData.Enchantments.Buffs.StatBAdditiveBaseStat.Yield().AppendSingle(_testData.Enchantments.Buffs.StatAAdditiveBaseStat), 5 };
            yield return new object[] { "Add then multiply", _testData.Stats.DefinitionIds.StatA, _testData.Enchantments.Buffs.StatAAdditiveBaseStat.Yield().AppendSingle(_testData.Enchantments.Buffs.StatAMultiplicativeBaseStat), 10 };
            yield return new object[] { "Multiply then add", _testData.Stats.DefinitionIds.StatA, _testData.Enchantments.Buffs.StatAMultiplicativeBaseStat.Yield().AppendSingle(_testData.Enchantments.Buffs.StatAAdditiveBaseStat), 5 };
        }

        public static IEnumerable<object[]> GetTimeOfDayEnchantmentsTheoryData()
        {
            yield return new object[] { "Over zero 0% active", _testData.Enchantments.DayTimeBuffs.StatAOverZero, 0, 0 };
            yield return new object[] { "Over zero 50% active", _testData.Enchantments.DayTimeBuffs.StatAOverZero, 0.5, 10 };
            yield return new object[] { "Over zero 100% active", _testData.Enchantments.DayTimeBuffs.StatAOverZero, 1, 10 };
            yield return new object[] { "Binary 0% active", _testData.Enchantments.DayTimeBuffs.StatABinary, 0, 0 };
            yield return new object[] { "Binary 50% active", _testData.Enchantments.DayTimeBuffs.StatABinary, 0.5, 0 };
            yield return new object[] { "Binary 100% active", _testData.Enchantments.DayTimeBuffs.StatABinary, 1, 10 };
            yield return new object[] { "Peak 0% active", _testData.Enchantments.DayTimeBuffs.StatAPeak, 0, 0 };
            yield return new object[] { "Peak 50% active", _testData.Enchantments.DayTimeBuffs.StatAPeak, 0.5, 5 };
            yield return new object[] { "Peak 100% active", _testData.Enchantments.DayTimeBuffs.StatAPeak, 1, 10 };
        }

        public static IEnumerable<object[]> GetSingleEnchantmentNoBaseStatsOverTimeTheoryData()
        {
            var doubleDuration = 2;
            var halfDuration = 0.5;

            yield return new object[] { "Additive zero turns", _testData.Enchantments.Buffs.StatAAdditiveBaseStat, 0, 5 };
            yield return new object[] { "Additive single turn", _testData.Enchantments.Buffs.StatAAdditiveBaseStat, 1, 5 };
            yield return new object[] { "Additve two turns", _testData.Enchantments.Buffs.StatAAdditiveBaseStat, doubleDuration, 5 };
            yield return new object[] { "Additive half turn", _testData.Enchantments.Buffs.StatAAdditiveBaseStat, halfDuration, 5 };
            yield return new object[] { "Base stat additive-over-time zero turns", _testData.Enchantments.BuffsOverTime.StatABaseStat, 0, 0 };
            yield return new object[] { "Base stat additive-over-time single turn", _testData.Enchantments.BuffsOverTime.StatABaseStat, 1, 10 };
            yield return new object[] { "Base stat additive-over-time two turns", _testData.Enchantments.BuffsOverTime.StatABaseStat, doubleDuration, 20 };
            yield return new object[] { "Base stat additive-over-time half turn", _testData.Enchantments.BuffsOverTime.StatABaseStat, halfDuration, 5 };
        }

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
            var result = _fixture
                .EnchantmentCalculator
                .Calculate(
                    enchantmentCalculatorContext,
                    baseStats,
                    enchantment.Behaviors.GetOnly<IHasStatDefinitionIdBehavior>().StatDefinitionId);

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
                    _testData.States.States.TimeOfDay,
                    new ConstantValueStateContext(new Dictionary<IIdentifier, double>()
                    {
                        { _testData.States.TimeOfDay.Day, percentActiveState }
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
                enchantment.Behaviors.GetOnly<IHasStatDefinitionIdBehavior>().StatDefinitionId);

            Assert.Equal(expectedResult, result);
        }

        [Theory,
         MemberData(nameof(GetSingleEnchantmentNoBaseStatsOverTimeTheoryData))]
        private void ApplyEnchantments_SingleEnchantmentNoBaseStatsOverTime(
            string _,
            IEnchantment enchantment,
            double elapsedTurns,
            double expectedValue)
        {
            var baseStats = new Dictionary<IIdentifier, double>();
            var enchantmentCalculatorContext = EnchantmentCalculatorContext
                .None
                .WithElapsedTurns(elapsedTurns)
                .WithEnchantments(enchantment)
                .WithComponent(new GenericComponent<IStateContextProvider>(_fixture.StateContextProvider));
            var result = _fixture.EnchantmentCalculator.Calculate(
                enchantmentCalculatorContext,
                baseStats,
                enchantment.Behaviors.GetOnly<IHasStatDefinitionIdBehavior>().StatDefinitionId);

            Assert.Equal(expectedValue, result);
        }

        [Fact]
        private void Calculate_BadExpression_ThrowsFormatException()
        {
            var baseStats = new Dictionary<IIdentifier, double>();
            var enchantmentCalculatorContext = EnchantmentCalculatorContext
                .None
                .WithEnchantments(_testData.Enchantments.BadExpression)
                .WithComponent(new GenericComponent<IStateContextProvider>(_fixture.StateContextProvider));

            Action method = () => _fixture.EnchantmentCalculator.Calculate(
                enchantmentCalculatorContext,
                baseStats,
                _testData.Enchantments.BadExpression.Behaviors.GetOnly<IHasStatDefinitionIdBehavior>().StatDefinitionId);

            Assert.Throws<FormatException>(method);
        }
    }
}
