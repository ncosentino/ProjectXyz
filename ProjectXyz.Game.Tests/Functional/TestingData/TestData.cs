using System;
using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Application.Enchantments.Core.Calculations;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Shared;
using ProjectXyz.Game.Tests.Functional.TestingData.States;
using ProjectXyz.Game.Tests.Functional.TestingData.Stats;
using ProjectXyz.Plugins.Core;
using ProjectXyz.Plugins.DomainConversion.EnchantmentsAndTriggers;
using ProjectXyz.Plugins.Triggers.Elapsed.Duration;

namespace ProjectXyz.Game.Tests.Functional.TestingData
{
    public sealed class TestData
    {
        #region Constants
        private static readonly EnchantmentFactory ENCHANTMENT_FACTORY = new EnchantmentFactory();
        private static readonly CalculationPriorities CALC_PRIORITIES = new CalculationPriorities();
        private static readonly StatInfo.StatDefinitionIds STAT_DEFINITION_IDS = new StatInfo.StatDefinitionIds();
        private static readonly IInterval UNIT_INTERVAL = new Interval<double>(1);
        #endregion

        #region Properties
        public StatesPlugin StatesPlugin { get; } = new StatesPlugin(PluginArgs.None);

        public StatsPlugin StatsPlugin { get; } = new StatsPlugin(PluginArgs.None);

        public StatInfo Stats { get; } = new StatInfo();

        public StateInfo States { get; } = new StateInfo();

        public IInterval UnitInterval { get; } = UNIT_INTERVAL;

        public IInterval ZeroInterval { get; } = UNIT_INTERVAL.Subtract(UNIT_INTERVAL);
        
        public ExampleEnchantments Enchantments { get; } = new ExampleEnchantments();

        public IReadOnlyDictionary<IIdentifier, string> StatDefinitionIdToCalculationMapping { get; } = new Dictionary<IIdentifier, string>()
        {

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



        private class CalculationPriorities
        {
            public ICalculationPriority Lowest { get; } = new CalculationPriority<int>(int.MinValue);

            public ICalculationPriority Middle { get; } = new CalculationPriority<int>(0);

            public ICalculationPriority Highest { get; } = new CalculationPriority<int>(int.MaxValue);
        }
        #endregion
    }
}