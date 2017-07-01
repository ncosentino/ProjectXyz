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
            yield return new object[] { TEST_DATA.Enchantments.Buffs.StatA, TEST_DATA.ZeroInterval, 5 };
            yield return new object[] { TEST_DATA.Enchantments.BuffsOverTime.StatA, TEST_DATA.UnitInterval.Divide(2), 5 };
            yield return new object[] { TEST_DATA.Enchantments.BuffsOverTime.StatA, TEST_DATA.UnitInterval, 10 };
            yield return new object[] { TEST_DATA.Enchantments.BuffsOverTime.StatA, TEST_DATA.UnitInterval.Multiply(2), 20 };
        }
        #endregion

        #region Tests
        [Theory,
         MemberData(nameof(GetTimeElapsedSingleEnchantmentTestData))]
        private void TimeElapsed_SingleEnchantment_ExpectedStatValue(
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

            var elapsedTimeTriggerMechanic = new ElapsedTimeTriggerMechanic((_, x) => statUpdater.Update(
                hasMutableStats.Stats,
                hasEnchantments.Enchantments,
                hasMutableStats.MutateStats,
                x.Elapsed));
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
