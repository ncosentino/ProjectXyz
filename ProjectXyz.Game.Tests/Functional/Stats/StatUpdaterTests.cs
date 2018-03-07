using System;
using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.States;
using ProjectXyz.Application.Enchantments.Core.Calculations;
using ProjectXyz.Application.Stats.Core;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Game.Core.Behaviors;
using ProjectXyz.Game.Core.Stats;
using ProjectXyz.Game.Tests.Functional.TestingData;
using ProjectXyz.Plugins.Triggers.Elapsed;
using Xunit;
using ProjectXyz.Game.Interface.Enchantments;

namespace ProjectXyz.Game.Tests.Functional.Stats
{
    public sealed class StatUpdaterTests
    {
        #region Constants
        private static readonly TestData TEST_DATA = new TestData();
        #endregion

        #region Fields
        private readonly TestFixture _fixture;
        #endregion

        #region Constructors
        public StatUpdaterTests()
        {
            _fixture = new TestFixture(TEST_DATA);
        }
        #endregion

        #region Methods
        public static IEnumerable<object[]> GetTimeElapsedSingleEnchantmentTestData()
        {
            yield return new object[] { "Addition zero interval", TEST_DATA.Enchantments.Buffs.StatAAdditive, TEST_DATA.ZeroInterval, 5 };
            yield return new object[] { "Addition-over-time zero interval", TEST_DATA.Enchantments.BuffsOverTime.StatA, TEST_DATA.ZeroInterval, 0 };
            yield return new object[] { "Addition-over-time half interval", TEST_DATA.Enchantments.BuffsOverTime.StatA, TEST_DATA.UnitInterval.Divide(2), 5 };
            yield return new object[] { "Addition-over-time one interval", TEST_DATA.Enchantments.BuffsOverTime.StatA, TEST_DATA.UnitInterval, 10 };
            yield return new object[] { "Addition-over-time two intervals", TEST_DATA.Enchantments.BuffsOverTime.StatA, TEST_DATA.UnitInterval.Multiply(2), 20 };
            yield return new object[] { "Addition expires zero interval", TEST_DATA.Enchantments.BuffsThatExpire.StatA, TEST_DATA.ZeroInterval, 5 };
            yield return new object[] { "Addition expires half interval", TEST_DATA.Enchantments.BuffsThatExpire.StatA, TEST_DATA.UnitInterval.Divide(2), 5 };

            // FIXME: this buff says it's supposed to expire. it does. but the 
            // BASE stat has been modified so the buff goes away but the effect
            // of the buff is permanent
            yield return new object[] { "Addition expires one interval", TEST_DATA.Enchantments.BuffsThatExpire.StatA, TEST_DATA.UnitInterval, 0 };
        }
        #endregion

        #region Tests
        [Theory,
         MemberData(nameof(GetTimeElapsedSingleEnchantmentTestData))]
        private void TimeElapsed_SingleEnchantment(
            string _,
            IEnchantment enchantment,
            IInterval elapsed,
            double expectedValue)
        {
            // Setup
            var mutableStatsProvider = new MutableStatsProvider();
            var hasMutableStats = new HasMutableStats(mutableStatsProvider);
            var hasEnchantments = new HasEnchantments(_fixture.ActiveEnchantmentManager);

            var enchantmentCalculatorContextFactory = new EnchantmentCalculatorContextFactory(new[]
            {
                new GenericComponent<IStateContextProvider>(_fixture.StateContextProvider)
            });

            var statUpdater = new StatUpdater(
                _fixture.EnchantmentApplier,
                enchantmentCalculatorContextFactory);

            _fixture.ActiveEnchantmentManager.Add(enchantment);

            var elapsedTimeTriggerMechanic = new ElapsedTimeTriggerMechanic((x, y) => statUpdater.Update(
                hasMutableStats.Stats,
                hasEnchantments.Enchantments,
                hasMutableStats.MutateStats,
                y.Elapsed));
            _fixture.TriggerMechanicRegistrar.RegisterTrigger(elapsedTimeTriggerMechanic);

            // Execute
            _fixture.ElapsedTimeTriggerSourceMechanic.Update(elapsed);

            // Assert
            Assert.Equal(
                expectedValue,
                mutableStatsProvider.Stats[enchantment.StatDefinitionId]);
        }
        #endregion
    }
}
