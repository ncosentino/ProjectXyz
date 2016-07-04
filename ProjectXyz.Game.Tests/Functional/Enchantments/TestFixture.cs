using System.Collections.Generic;
using Jace;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Stats.Calculations;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Shared;
using ProjectXyz.Framework.Shared.Math;
using ProjectXyz.Game.Core.Enchantments;

namespace ProjectXyz.Game.Tests.Functional.Enchantments
{
    public sealed class TestFixture
    {
        #region Constants
        private static readonly CalculationPriorities CALC_PRIORITIES = new CalculationPriorities();
        private static readonly StatDefinitionIds STAT_DEFINITION_IDS = new StatDefinitionIds();
        private static readonly StateInfo STATES = new StateInfo();
        #endregion

        #region Constructors
        public TestFixture()
        {
            var statDefinitionIdToTermMapping = StatDefinitionIdToTermMapping;
            var statDefinitionIdToCalculationMapping = StatDefinitionIdToCalculationMapping;

            var jaceCalculationEngine = new CalculationEngine();
            var stringExpressionEvaluator = new StringExpressionEvaluatorWrapper(new GenericExpressionEvaluator(jaceCalculationEngine.Calculate), true);

            var statCalculationValueNodeFactory = new StatCalculationValueNodeFactory();
            var statCalculationExpressionNodeFactory = new StatCalculationExpressionNodeFactory(stringExpressionEvaluator);
            var statCalculationNodeFactory = new StatCalculationNodeFactoryWrapper(new IStatCalculationNodeFactory[]
            {
                statCalculationValueNodeFactory,
                statCalculationExpressionNodeFactory
            });

            var expressionStatDefinitionDependencyFinder = new ExpressionStatDefinitionDependencyFinder();

            var statBoundsExpressionInterceptor = StatBoundsExpressionInterceptor;

            var statCalculationNodeCreator = new StatCalculationNodeCreator(
                statCalculationNodeFactory,
                expressionStatDefinitionDependencyFinder,
                statBoundsExpressionInterceptor,
                statDefinitionIdToTermMapping,
                statDefinitionIdToCalculationMapping);

            var statCalculator = new StatCalculator(statCalculationNodeCreator);

            var enchantmentExpressionInterceptorConverter = new EnchantmentExpressionInterceptorConverter();

            var stateValueInjector = new StateValueInjector(StateIdToTermMapping);
            var enchantmentExpressionInterceptorFactory = new EnchantmentExpressionInterceptorFactory(
                stateValueInjector,
                statDefinitionIdToTermMapping);

            var enchantmentStatCalculator = new StatCalculatorWrapper(
                statCalculator,
                enchantmentExpressionInterceptorConverter);

            EnchantmentCalculator = new EnchantmentCalculator(
                enchantmentExpressionInterceptorFactory,
                enchantmentStatCalculator);
            
            EnchantmentApplier = new EnchantmentApplier(EnchantmentCalculator);
        }
        #endregion

        #region Properties
        public IEnchantmentCalculator EnchantmentCalculator { get; }

        public IEnchantmentApplier EnchantmentApplier { get; }

        public ExampleEnchantments Enchantments { get; } = new ExampleEnchantments();

        public StatDefinitionIds Stats { get; } = STAT_DEFINITION_IDS;

        public StateInfo States { get; } = STATES;

        public IReadOnlyDictionary<IIdentifier, string> StatDefinitionIdToTermMapping { get; } = new Dictionary<IIdentifier, string>()
        {
            { STAT_DEFINITION_IDS.StatA, "STAT_A" },
            { STAT_DEFINITION_IDS.StatB, "STAT_B" },
            { STAT_DEFINITION_IDS.StatC, "STAT_C" },
        };

        public IReadOnlyDictionary<IIdentifier, string> StatDefinitionIdToCalculationMapping { get; } = new Dictionary<IIdentifier, string>()
        {

        };

        public IStatExpressionInterceptor StatBoundsExpressionInterceptor { get; } = new StatBoundsExpressionInterceptor(new Dictionary<IIdentifier, IStatBounds>()
        {
            { STAT_DEFINITION_IDS.StatC, new StatBounds("5", "10") },
        });

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
        #endregion

        #region Classes
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

                public IEnchantment StatC { get; } = new ExpressionEnchantment(STAT_DEFINITION_IDS.StatC, "STAT_C + 100", CALC_PRIORITIES.Middle);
            }

            public sealed class DebuffEnchantments
            {
                public IEnchantment StatA { get; } = new ExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A - 5", CALC_PRIORITIES.Middle);

                public IEnchantment StatC { get; } = new ExpressionEnchantment(STAT_DEFINITION_IDS.StatC, "STAT_C - 100", CALC_PRIORITIES.Middle);
            }

            public sealed class DayTimeBuffEnchantments
            {
                public IEnchantment StatAPeak { get; } = new ExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A + 10 * TOD_DAY", CALC_PRIORITIES.Middle);

                public IEnchantment StatABinary { get; } = new ExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A + 10 * if(TOD_DAY > 0, 1, 0)", CALC_PRIORITIES.Middle);
            }
        }

        public sealed class StatDefinitionIds
        {
            public IIdentifier StatA { get; } = new StringIdentifier("Stat A");
            public IIdentifier StatB { get; } = new StringIdentifier("Stat B");
            public IIdentifier StatC { get; } = new StringIdentifier("Stat C");
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
        #endregion
    }
}