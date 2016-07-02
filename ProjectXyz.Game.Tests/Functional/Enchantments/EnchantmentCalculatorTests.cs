using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Stats.Calculations;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Application.Shared.Stats;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;
using ProjectXyz.Framework.Shared;
using ProjectXyz.Framework.Shared.Math;
using ProjectXyz.Game.Core.Enchantments;
using Xunit;

namespace ProjectXyz.Game.Tests.Functional.Enchantments
{
    public sealed class EnchantmentCalculatorTests
    {
        #region Constants
        private static readonly TestFixture FIXTURE = new TestFixture();
        #endregion

        #region Fields
        private readonly EnchantmentCalculator _enchantmentCalculator;
        #endregion

        #region Constructors
        public EnchantmentCalculatorTests()
        {
            var statDefinitionIdToTermMapping = FIXTURE.StatDefinitionIdToTermMapping;
            var statDefinitionIdToCalculationMapping = FIXTURE.StatDefinitionIdToCalculationMapping;

            var stringExpressionEvaluator = new StringExpressionEvaluatorWrapper(new DataTableExpressionEvaluator(new DataTable()), true);

            var statCalculationValueNodeFactory = new StatCalculationValueNodeFactory();
            var statCalculationExpressionNodeFactory = new StatCalculationExpressionNodeFactory(stringExpressionEvaluator);
            var statCalculationNodeFactory = new StatCalculationNodeFactoryWrapper(new IStatCalculationNodeFactory[]
            {
                statCalculationValueNodeFactory,
                statCalculationExpressionNodeFactory
            });

            var expressionStatDefinitionDependencyFinder = new ExpressionStatDefinitionDependencyFinder();

            var statCalculationNodeCreator = new StatCalculationNodeCreator(
                statCalculationNodeFactory,
                expressionStatDefinitionDependencyFinder,
                statDefinitionIdToTermMapping,
                statDefinitionIdToCalculationMapping);

            var statCalculator = new StatCalculator(statCalculationNodeCreator);

            var enchantmentExpressionInterceptorConverter = new EnchantmentExpressionInterceptorConverter();

            var stateValueInjector = new StateValueInjector(FIXTURE.StateIdToTermMapping);
            var enchantmentExpressionInterceptorFactory = new EnchantmentExpressionInterceptorFactory(
                stateValueInjector,
                statDefinitionIdToTermMapping);

            var enchantmentStatCalculator = new StatCalculatorWrapper(
                statCalculator,
                enchantmentExpressionInterceptorConverter);

            _enchantmentCalculator = new EnchantmentCalculator(
                enchantmentExpressionInterceptorFactory,
                enchantmentStatCalculator);
        }
        #endregion

        #region Methods
        private static IEnumerable<object[]> GetSingleEnchantmentNoBaseStatsTheoryData()
        {
            yield return new object[] { FIXTURE.Enchantments.Buffs.StatA, 5 };
            yield return new object[] { FIXTURE.Enchantments.Debuffs.StatA, -5 };
            yield return new object[] { FIXTURE.Enchantments.Buffs.StatB, 5 };
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
            yield return new object[] { FIXTURE.Enchantments.DayTimeBuffs.StatABinary, 0.5, 5 };
            yield return new object[] { FIXTURE.Enchantments.DayTimeBuffs.StatABinary, 1, 5 };
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
            var result = _enchantmentCalculator.Calculate(
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
            var result = _enchantmentCalculator.Calculate(
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
            var result = _enchantmentCalculator.Calculate(
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

            Action method = () => _enchantmentCalculator.Calculate(
                StateContextProvider.Empty,
                baseStats,
                FIXTURE.Enchantments.BadExpression.AsArray(),
                FIXTURE.Enchantments.BadExpression.StatDefinitionId);

            Assert.Throws<FormatException>(method);
        }
        #endregion

        #region Classes
        private sealed class TestFixture
        {
            private static readonly CalculationPriorities CALC_PRIORITIES = new CalculationPriorities();
            private static readonly StatDefinitionIds STAT_DEFINITION_IDS = new StatDefinitionIds();
            private static readonly StateInfo STATES = new StateInfo();
            
            public ExampleEnchantments Enchantments { get; } = new ExampleEnchantments();

            public StatDefinitionIds Stats { get; } = STAT_DEFINITION_IDS;

            public StateInfo States { get; } = STATES;

            public IReadOnlyDictionary<IIdentifier, string> StatDefinitionIdToTermMapping { get; } = new Dictionary<IIdentifier, string>()
            {
                { STAT_DEFINITION_IDS.StatA, "STAT_A" },
                { STAT_DEFINITION_IDS.StatB, "STAT_B" },
            };

            public IReadOnlyDictionary<IIdentifier, string> StatDefinitionIdToCalculationMapping { get; } = new Dictionary<IIdentifier, string>()
            {

            };

            public IReadOnlyDictionary<IIdentifier, IReadOnlyDictionary<IIdentifier, string>> StateIdToTermMapping = new Dictionary<IIdentifier, IReadOnlyDictionary<IIdentifier, string>>()
            {
                {
                    STATES.States.TimeOfDay,
                    new Dictionary<IIdentifier, string>()
                    {
                        { STATES.TimeOfDay.Day, "TOD_DAY" },
                        { STATES.TimeOfDay.Night, "TOD_NIGHT" },
                    }
                },
            };

            public sealed class ExampleEnchantments
            {
                
                public IEnchantment PreNullifyStatA { get; } = new ExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "-1", CALC_PRIORITIES.Lowest);

                public IEnchantment PostNullifyStatA { get; } = new ExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "-1", CALC_PRIORITIES.Highest);

                public IEnchantment RecursiveStatA { get; } = new ExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A", CALC_PRIORITIES.Highest);

                public IEnchantment BadExpression { get; } = new ExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "Can't actually be evaluated", CALC_PRIORITIES.Highest);

                public BuffEnchantments Buffs { get; } = new BuffEnchantments();

                public DebuffEnchantments Debuffs { get; } = new DebuffEnchantments();

                public DayTimeBuffEnchantments DayTimeBuffs { get; } = new DayTimeBuffEnchantments();

                public sealed class BuffEnchantments
                {
                    public IEnchantment StatA { get; } = new ExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A + 5", CALC_PRIORITIES.Middle);

                    public IEnchantment StatB { get; } = new ExpressionEnchantment(STAT_DEFINITION_IDS.StatB, "STAT_B + 5", CALC_PRIORITIES.Middle);
                }

                public sealed class DebuffEnchantments
                {
                    public IEnchantment StatA { get; } = new ExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A - 5", CALC_PRIORITIES.Middle);
                }

                public sealed class DayTimeBuffEnchantments
                {
                    public IEnchantment StatAPeak { get; } = new ExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A + 10 * TOD_DAY", CALC_PRIORITIES.Middle);

                    public IEnchantment StatABinary { get; } = new ExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A + 10 * (IIF TOD_DAY > 0, 1, 0)", CALC_PRIORITIES.Middle);
                }
            }

            public sealed class StatDefinitionIds
            {
                public IIdentifier StatA { get; } = new StringIdentifier("Stat A");

                public IIdentifier StatB { get; } = new StringIdentifier("Stat B");
            }

            public sealed class StateInfo
            {
                public StateTypeIds States { get; } = new StateTypeIds();

                public TimeOfDayIds TimeOfDay { get; } = new TimeOfDayIds();

                public sealed class StateTypeIds
                {
                    public IIdentifier TimeOfDay { get; } = new StringIdentifier("Time of Day");
                }

                public sealed class TimeOfDayIds
                {
                    public IIdentifier Day { get; } = new StringIdentifier("Day");

                    public IIdentifier Night { get; } = new StringIdentifier("Night");
                }
            }

            private class CalculationPriorities
            {
                public ICalculationPriority Lowest { get; } = new CalculationPriority<int>(int.MinValue);

                public ICalculationPriority Middle { get; } = new CalculationPriority<int>(0);

                public ICalculationPriority Highest { get; } = new CalculationPriority<int>(int.MaxValue);
            }
        }
        #endregion
    }
}
