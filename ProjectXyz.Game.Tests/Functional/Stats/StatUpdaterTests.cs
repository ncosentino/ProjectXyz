using System;
using System.Collections.Generic;
using ProjectXyz.Application.Core.Stats;
using ProjectXyz.Application.Core.Triggering.Triggers.Elapsed;
using ProjectXyz.Application.Enchantments.Api;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Game.Core.Stats;
using Xunit;

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
        private static IEnumerable<object[]> GetTimeElapsedSingleEnchantmentTestData()
        {
            yield return new object[] { TEST_DATA.Enchantments.Buffs.StatA, TEST_DATA.ZeroInterval, 5 };
            yield return new object[] { TEST_DATA.Enchantments.BuffsOverTime.StatA, TEST_DATA.UnitInterval.Divide(2), 5 };
            yield return new object[] { TEST_DATA.Enchantments.BuffsOverTime.StatA, TEST_DATA.UnitInterval, 10 };
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

            var statUpdater = new StatUpdater(
                ComponentCollection.Empty,
                mutableStatsProvider,
                _fixture.ActiveEnchantmentManager,
                _fixture.EnchantmentApplier);

            _fixture.ActiveEnchantmentManager.Add(enchantment);

            var elapsedTimeTriggerMechanic = new ElapsedTimeTriggerMechanic(statUpdater.Update);
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
