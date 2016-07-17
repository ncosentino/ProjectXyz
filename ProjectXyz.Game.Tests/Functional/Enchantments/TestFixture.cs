using System;
using System.Collections.Generic;
using System.Linq;
using Jace;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Enchantments.Calculations;
using ProjectXyz.Application.Core.Enchantments.Expiration;
using ProjectXyz.Application.Core.Stats.Calculations;
using ProjectXyz.Application.Core.Triggering;
using ProjectXyz.Application.Core.Triggering.Triggers.Duration;
using ProjectXyz.Application.Core.Triggering.Triggers.Elapsed;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Application.Interface.Enchantments.Expiration;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Application.Interface.Triggering;
using ProjectXyz.Application.Interface.Triggering.Triggers.Elapsed;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;
using ProjectXyz.Framework.Shared;
using ProjectXyz.Framework.Shared.Math;
using ProjectXyz.Game.Core.Enchantments;

namespace ProjectXyz.Game.Tests.Functional.Enchantments
{
    public sealed class TestFixture
    {
        #region Constants
        private static readonly EnchantmentFactory ENCHANTMENT_FACTORY = new EnchantmentFactory();
        private static readonly CalculationPriorities CALC_PRIORITIES = new CalculationPriorities();
        private static readonly StatDefinitionIds STAT_DEFINITION_IDS = new StatDefinitionIds();
        private static readonly StateInfo STATES = new StateInfo();
        private static readonly IInterval UNIT_INTERVAL = new Interval<double>(1);

        private static readonly IReadOnlyCollection<Func<IEnchantmentCalculatorContext, KeyValuePair<string, double>>> CONTEXT_TO_VALUE_MAPPING  = new Func<IEnchantmentCalculatorContext, KeyValuePair<string, double>>[]
        {
            context => new KeyValuePair<string, double>("INTERVAL", context.Elapsed.Divide(UNIT_INTERVAL)),
        };
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
            var stateEnchantmentExpressionInterceptorFactory = new StateExpressionInterceptorFactory(
                stateValueInjector,
                statDefinitionIdToTermMapping);

            var valueMappingExpressionInterceptorFactory = new ValueMappingExpressionInterceptorFactory();

            var enchantmentStatCalculator = new StatCalculatorWrapper(
                statCalculator,
                enchantmentExpressionInterceptorConverter);

            var contextToTermValueMappingConverter = new ContextToTermValueMappingConverter(CONTEXT_TO_VALUE_MAPPING);

            var contextToInterceptorsConverter = new ContextToInterceptorsConverter(
                stateEnchantmentExpressionInterceptorFactory,
                valueMappingExpressionInterceptorFactory,
                contextToTermValueMappingConverter);

            EnchantmentCalculator = new EnchantmentCalculator(
                enchantmentStatCalculator,
                contextToInterceptorsConverter);

            EnchantmentApplier = new EnchantmentApplier(EnchantmentCalculator);

            ElapsedTimeTriggerSourceMechanic = new ElapsedTimeTriggerSourceMechanic();

            var registrar = new TriggerMechanicRegistrar(ElapsedTimeTriggerSourceMechanic.AsArray());

            var durationTriggerMechanicFactory = new DurationTriggerMechanicFactory();
            var expiryTriggerMechanicFactory = new ExpiryTriggerMechanicFactory(durationTriggerMechanicFactory);
            ActiveEnchantmentManager = new ActiveEnchantmentManager(
                expiryTriggerMechanicFactory,
                registrar);
        }
        #endregion

        #region Properties
        public ElapsedTimeTriggerSourceMechanic ElapsedTimeTriggerSourceMechanic { get; }

        public IActiveEnchantmentManager ActiveEnchantmentManager { get; }

        public IInterval UnitInterval { get; } = UNIT_INTERVAL;

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

            public IEnchantment PreNullifyStatA { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "-1", CALC_PRIORITIES.Lowest);

            public IEnchantment PostNullifyStatA { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "-1", CALC_PRIORITIES.Highest);

            public IEnchantment RecursiveStatA { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A", CALC_PRIORITIES.Highest);

            public IEnchantment BadExpression { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "Can't actually be evaluated", CALC_PRIORITIES.Highest);

            public BuffEnchantments Buffs { get; } = new BuffEnchantments();

            public DebuffEnchantments Debuffs { get; } = new DebuffEnchantments();

            public DayTimeBuffEnchantments DayTimeBuffs { get; } = new DayTimeBuffEnchantments();

            public BuffOverTimeEnchantments BuffsOverTime { get; } = new BuffOverTimeEnchantments();

            public BuffsThatExpireEnchantments BuffsThatExpire { get; } = new BuffsThatExpireEnchantments();

            public sealed class BuffEnchantments
            {
                public IEnchantment StatA { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A + 5", CALC_PRIORITIES.Middle);

                public IEnchantment StatB { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatB, "STAT_B + 5", CALC_PRIORITIES.Middle);

                public IEnchantment StatC { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatC, "STAT_C + 100", CALC_PRIORITIES.Middle);
            }

            public sealed class DebuffEnchantments
            {
                public IEnchantment StatA { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A - 5", CALC_PRIORITIES.Middle);

                public IEnchantment StatC { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatC, "STAT_C - 100", CALC_PRIORITIES.Middle);
            }

            public sealed class DayTimeBuffEnchantments
            {
                public IEnchantment StatAPeak { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A + 10 * TOD_DAY", CALC_PRIORITIES.Middle);

                public IEnchantment StatABinary { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A + 10 * if(TOD_DAY > 0, 1, 0)", CALC_PRIORITIES.Middle);
            }

            public sealed class BuffOverTimeEnchantments
            {
                public IEnchantment StatA { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A + (10 * INTERVAL)", CALC_PRIORITIES.Middle);
            }

            public sealed class BuffsThatExpireEnchantments
            {
                public IEnchantment StatA { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(
                    STAT_DEFINITION_IDS.StatA, 
                    "STAT_A + 5", 
                    CALC_PRIORITIES.Middle,
                    new ExpiryTriggerComponent(new DurationTriggerComponent(new Interval<double>(10))));
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

            public ElapsedTimeInfo ElapsedTime { get; } = new ElapsedTimeInfo();

            public sealed class StateTypeIds
            {
                public IIdentifier TimeOfDay { get; } = new StringIdentifier("Time of Day");

                public IIdentifier ElapsedTime { get; } = new StringIdentifier("Elapsed Time");
            }

            public sealed class TimeOfDayIds
            {
                public IIdentifier Day { get; } = new StringIdentifier("Day");

                public IIdentifier Night { get; } = new StringIdentifier("Night");
            }

            public sealed class ElapsedTimeInfo
            {
                public IIdentifier ElapsedTimeUnits { get; } = new StringIdentifier("Elapsed Time Units");
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

    public sealed class EnchantmentFactory
    {
        public IEnchantment CreateExpressionEnchantment(
            IIdentifier statDefinitionId,
            string expression,
            ICalculationPriority calculationPriority,
            params IExpiryComponent[] expiry)
        {
            IEnumerable<IComponent> components = new EnchantmentExpressionComponent(
                calculationPriority,
                expression)
                .Yield();

            if (expiry != null)
            {
                components = components.Concat(expiry);
            }

            return new Enchantment(
                statDefinitionId,
                components);
        }
    }
}