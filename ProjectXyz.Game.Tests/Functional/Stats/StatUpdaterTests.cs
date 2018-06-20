using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.States;
using ProjectXyz.Application.Stats.Core;
using ProjectXyz.Game.Tests.Functional.TestingData;
using ProjectXyz.Plugins.Triggers.Elapsed;
using Xunit;
using ProjectXyz.Game.Interface.Enchantments;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Shared.Framework.Entities;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Calculations;

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
            yield return new object[] { "Base stat addition zero interval", TEST_DATA.Enchantments.Buffs.StatAAdditiveBaseStat, TEST_DATA.ZeroInterval, 5 };
            yield return new object[] { "Base stat addition-over-time zero interval", TEST_DATA.Enchantments.BuffsOverTime.StatABaseStat, TEST_DATA.ZeroInterval, 0 };
            yield return new object[] { "Base stat addition-over-time half interval", TEST_DATA.Enchantments.BuffsOverTime.StatABaseStat, TEST_DATA.UnitInterval.Divide(2), 5 };
            yield return new object[] { "Base stat addition-over-time one interval", TEST_DATA.Enchantments.BuffsOverTime.StatABaseStat, TEST_DATA.UnitInterval, 10 };
            yield return new object[] { "Base stat addition-over-time two intervals", TEST_DATA.Enchantments.BuffsOverTime.StatABaseStat, TEST_DATA.UnitInterval.Multiply(2), 20 };
            yield return new object[] { "Base stat addition expires zero interval", TEST_DATA.Enchantments.BuffsThatExpire.StatABaseStat, TEST_DATA.ZeroInterval, 5 };
            yield return new object[] { "Base stat addition expires half interval", TEST_DATA.Enchantments.BuffsThatExpire.StatABaseStat, TEST_DATA.UnitInterval.Divide(2), 5 };
            yield return new object[] { "Base stat addition expires one interval", TEST_DATA.Enchantments.BuffsThatExpire.StatABaseStat, TEST_DATA.UnitInterval, 5 };
            yield return new object[] { "On-demand addition expires one interval", TEST_DATA.Enchantments.BuffsThatExpire.StatAOnDemand, TEST_DATA.UnitInterval, 0 };
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
            var activeEnchantmentManager = _fixture
                .ActiveEnchantmentManagerFactory
                .Create();
            var mutableStatsProvider = new MutableStatsProvider();
            var statManager = _fixture.StatManagerFactory.Create(mutableStatsProvider);
            var hasMutableStats = new HasMutableStatsBehavior(statManager);
            var hasEnchantments = new HasEnchantmentsBehavior(activeEnchantmentManager);

            var enchantmentCalculatorContextFactory = new EnchantmentCalculatorContextFactory(new[]
            {
                new GenericComponent<IStateContextProvider>(_fixture.StateContextProvider)
            });

            var statUpdater = new StatUpdater(
                _fixture.EnchantmentApplier,
                enchantmentCalculatorContextFactory);

            activeEnchantmentManager.Add(enchantment);

            var elapsedTimeTriggerMechanic = new ElapsedTimeTriggerMechanic((x, y) => statUpdater.Update(
                hasMutableStats.BaseStats,
                hasEnchantments.Enchantments,
                hasMutableStats.MutateStats,
                y.Elapsed));
            _fixture.TriggerMechanicRegistrar.RegisterTrigger(elapsedTimeTriggerMechanic);

            // Execute
            _fixture.ElapsedTimeTriggerSourceMechanic.Update(elapsed);

            // Assert
            Assert.Equal(
                expectedValue,
                mutableStatsProvider.Stats.GetValueOrDefault(enchantment.StatDefinitionId));
        }
        #endregion
    }
}
