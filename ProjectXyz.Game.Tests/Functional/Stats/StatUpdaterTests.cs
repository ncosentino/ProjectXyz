using System;
using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Application.Enchantments.Core.Calculations;
using ProjectXyz.Application.Stats.Core;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Game.Core.Stats;
using ProjectXyz.Game.Tests.Functional.TestingData;
using ProjectXyz.Plugins.Triggers.Elapsed;
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
        public static IEnumerable<object[]> GetTimeElapsedSingleEnchantmentTestData()
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

            var enchantmentCalculatorContextFactory = new EnchantmentCalculatorContextFactory();

            var statUpdater = new StatUpdater(
                new[] { TEST_DATA.StatesPlugin.StateContextProvider },
                mutableStatsProvider,
                _fixture.ActiveEnchantmentManager,
                _fixture.EnchantmentApplier,
                enchantmentCalculatorContextFactory);

            _fixture.ActiveEnchantmentManager.Add(enchantment);

            var elapsedTimeTriggerMechanic = new ElapsedTimeTriggerMechanic((_, x) => statUpdater.Update(x.Elapsed));
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
