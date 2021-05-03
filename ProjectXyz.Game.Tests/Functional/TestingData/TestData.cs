using System.Collections.Generic;

using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Tests.Functional.TestingData.Enchantments;
using ProjectXyz.Game.Tests.Functional.TestingData.States;
using ProjectXyz.Game.Tests.Functional.TestingData.Stats;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Enchantments;
using ProjectXyz.Plugins.Features.Behaviors.Default;
using ProjectXyz.Plugins.Features.ExpiringEnchantments;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations;
using ProjectXyz.Plugins.Features.TurnBased.Duration;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Game.Tests.Functional.TestingData
{
    public sealed class TestData
    {
        #region Constants
        private static readonly ExpressionEnchantmentFactory ENCHANTMENT_FACTORY = new ExpressionEnchantmentFactory(new EnchantmentFactory(new GameObjectFactory()));
        private static readonly ICalculationPriorityFactory CALCULATION_PRIORITY_FACTORY = new CalculationPriorityFactory();
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

            public IGameObject PreNullifyStatABaseStat { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "-1", CALC_PRIORITIES.Lowest, new AppliesToBaseStat());

            public IGameObject PostNullifyStatABaseStat { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "-1", CALC_PRIORITIES.Highest, new AppliesToBaseStat());

            public IGameObject RecursiveStatABaseStat { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A", CALC_PRIORITIES.Highest, new AppliesToBaseStat());

            public IGameObject BadExpression { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "Can't actually be evaluated", CALC_PRIORITIES.Highest);

            public BuffEnchantments Buffs { get; } = new BuffEnchantments();

            public DebuffEnchantments Debuffs { get; } = new DebuffEnchantments();

            public DayTimeBuffEnchantments DayTimeBuffs { get; } = new DayTimeBuffEnchantments();

            public BuffOverTimeEnchantments BuffsOverTime { get; } = new BuffOverTimeEnchantments();

            public BuffsThatExpireEnchantments BuffsThatExpire { get; } = new BuffsThatExpireEnchantments();

            public sealed class BuffEnchantments
            {
                public IGameObject StatAAdditiveBaseStat { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A + 5", CALC_PRIORITIES.Middle, new AppliesToBaseStat());

                public IGameObject StatAAdditiveOnDemand { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A + 5", CALC_PRIORITIES.Middle);

                public IGameObject StatAMultiplicativeBaseStat { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A * 2", CALC_PRIORITIES.Middle, new AppliesToBaseStat());

                public IGameObject StatBAdditiveBaseStat { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatB, "STAT_B + 5", CALC_PRIORITIES.Middle, new AppliesToBaseStat());

                public IGameObject StatCAdditiveBaseStat { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatC, "STAT_C + 100", CALC_PRIORITIES.Middle, new AppliesToBaseStat());
            }

            public sealed class DebuffEnchantments
            {
                public IGameObject StatAAdditiveBaseStat { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A - 5", CALC_PRIORITIES.Middle, new AppliesToBaseStat());

                public IGameObject StatAMultiplicative { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A * 0.5", CALC_PRIORITIES.Middle);

                public IGameObject StatCAdditiveBaseStat { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatC, "STAT_C - 100", CALC_PRIORITIES.Middle, new AppliesToBaseStat());
            }

            public sealed class DayTimeBuffEnchantments
            {
                public IGameObject StatAPeak { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A + 10 * TOD_DAY", CALC_PRIORITIES.Middle);

                public IGameObject StatABinary { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A + 10 * if(TOD_DAY >= 1, 1, 0)", CALC_PRIORITIES.Middle);

                public IGameObject StatAOverZero { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(STAT_DEFINITION_IDS.StatA, "STAT_A + 10 * if(TOD_DAY > 0, 1, 0)", CALC_PRIORITIES.Middle);
            }

            public sealed class BuffOverTimeEnchantments
            {
                public IGameObject StatABaseStat { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(
                    STAT_DEFINITION_IDS.StatA,
                    "STAT_A + (10 * INTERVAL)",
                    CALC_PRIORITIES.Middle,
                    new AppliesToBaseStat());

                public IGameObject StatAOnDemand { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(
                    STAT_DEFINITION_IDS.StatA,
                    "STAT_A + (10 * INTERVAL)",
                    CALC_PRIORITIES.Middle);
            }

            public sealed class BuffsThatExpireEnchantments
            {
                public IGameObject StatABaseStatAfter10TurnsIntervalIgnorant { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(
                    STAT_DEFINITION_IDS.StatA,
                    "STAT_A + 5",
                    CALC_PRIORITIES.Middle,
                    new AppliesToBaseStat(),
                    new ExpiryTriggerBehavior(new DurationTriggerBehavior(10)));

                public IGameObject StatABaseStatAfter10Turns { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(
                    STAT_DEFINITION_IDS.StatA,
                    "STAT_A + 5 * MIN(INTERVAL, 10)", // need to cap this at expiration limit
                    CALC_PRIORITIES.Middle,
                    new AppliesToBaseStat(),
                    new ExpiryTriggerBehavior(new DurationTriggerBehavior(10)));

                public IGameObject StatAOnDemandAfter10TurnsIntervalIgnorant { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(
                    STAT_DEFINITION_IDS.StatA,
                    "STAT_A + 5",
                    CALC_PRIORITIES.Middle,
                    new ExpiryTriggerBehavior(new DurationTriggerBehavior(10)));

                //public IGameObject StatAOnDemandAfter10Turns { get; } = ENCHANTMENT_FACTORY.CreateExpressionEnchantment(
                //    STAT_DEFINITION_IDS.StatA,
                //    "STAT_A + 5 * MIN(INTERVAL, 10)", // need to cap this at expiration limit
                //    CALC_PRIORITIES.Middle,
                //    new ExpiryTriggerBehavior(new DurationTriggerBehavior(10)));
            }
        }



        private class CalculationPriorities
        {
            public ICalculationPriority Lowest { get; } = CALCULATION_PRIORITY_FACTORY.Create<int>(int.MinValue);

            public ICalculationPriority Middle { get; } = CALCULATION_PRIORITY_FACTORY.Create<int>(0);

            public ICalculationPriority Highest { get; } = CALCULATION_PRIORITY_FACTORY.Create<int>(int.MaxValue);
        }
        #endregion
    }
}