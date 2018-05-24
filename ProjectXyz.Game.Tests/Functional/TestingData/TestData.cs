using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Application.Enchantments.Core.Calculations;
using ProjectXyz.Game.Tests.Functional.TestingData.Enchantments;
using ProjectXyz.Game.Tests.Functional.TestingData.States;
using ProjectXyz.Game.Tests.Functional.TestingData.Stats;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Enchantments;
using ProjectXyz.Plugins.Triggers.Elapsed.Duration;
using ProjectXyz.Plugins.Triggers.Enchantments.Expiration;
using ProjectXyz.Shared.Framework;

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

            public IEnchantment PreNullifyStatABaseStat { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "-1", CALC_PRIORITIES.Lowest, new AppliesToBaseStat());

            public IEnchantment PostNullifyStatABaseStat { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "-1", CALC_PRIORITIES.Highest, new AppliesToBaseStat());

            public IEnchantment RecursiveStatABaseStat { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A", CALC_PRIORITIES.Highest, new AppliesToBaseStat());

            public IEnchantment BadExpression { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "Can't actually be evaluated", CALC_PRIORITIES.Highest);

            public BuffEnchantments Buffs { get; } = new BuffEnchantments();

            public DebuffEnchantments Debuffs { get; } = new DebuffEnchantments();

            public DayTimeBuffEnchantments DayTimeBuffs { get; } = new DayTimeBuffEnchantments();

            public BuffOverTimeEnchantments BuffsOverTime { get; } = new BuffOverTimeEnchantments();

            public BuffsThatExpireEnchantments BuffsThatExpire { get; } = new BuffsThatExpireEnchantments();

            public sealed class BuffEnchantments
            {
                public IEnchantment StatAAdditiveBaseStat { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A + 5", CALC_PRIORITIES.Middle, new AppliesToBaseStat());

                public IEnchantment StatAAdditiveOnDemand { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A + 5", CALC_PRIORITIES.Middle);

                public IEnchantment StatAMultiplicativeBaseStat { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A * 2", CALC_PRIORITIES.Middle, new AppliesToBaseStat());

                public IEnchantment StatBAdditiveBaseStat { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatB, "STAT_B + 5", CALC_PRIORITIES.Middle, new AppliesToBaseStat());

                public IEnchantment StatCAdditiveBaseStat { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatC, "STAT_C + 100", CALC_PRIORITIES.Middle, new AppliesToBaseStat());
            }

            public sealed class DebuffEnchantments
            {
                public IEnchantment StatAAdditiveBaseStat { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A - 5", CALC_PRIORITIES.Middle, new AppliesToBaseStat());

                public IEnchantment StatAMultiplicative { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A * 0.5", CALC_PRIORITIES.Middle);

                public IEnchantment StatCAdditiveBaseStat { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatC, "STAT_C - 100", CALC_PRIORITIES.Middle, new AppliesToBaseStat());
            }

            public sealed class DayTimeBuffEnchantments
            {
                public IEnchantment StatAPeak { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A + 10 * TOD_DAY", CALC_PRIORITIES.Middle);

                public IEnchantment StatABinary { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A + 10 * if(TOD_DAY >= 1, 1, 0)", CALC_PRIORITIES.Middle);

                public IEnchantment StatAOverZero { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A + 10 * if(TOD_DAY > 0, 1, 0)", CALC_PRIORITIES.Middle);
            }

            public sealed class BuffOverTimeEnchantments
            {
                public IEnchantment StatABaseStat { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A + (10 * INTERVAL)", CALC_PRIORITIES.Middle, new AppliesToBaseStat());

                public IEnchantment StatAOnDemand { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A + (10 * INTERVAL)", CALC_PRIORITIES.Middle);
            }

            public sealed class BuffsThatExpireEnchantments
            {
                public IEnchantment StatABaseStat { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(
                    STAT_DEFINITION_IDS.StatA,
                    "STAT_A + 5",
                    CALC_PRIORITIES.Middle,
                    new AppliesToBaseStat(),
                    new ExpiryTriggerComponent(new DurationTriggerComponent(new Interval<double>(10))));

                public IEnchantment StatAOnDemand { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(
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