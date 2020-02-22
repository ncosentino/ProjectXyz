using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.States;
using ProjectXyz.Game.Tests.Functional.TestingData;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.ElapsedTime;
using ProjectXyz.Plugins.Stats;
using ProjectXyz.Shared.Framework.Entities;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Calculations;
using Xunit;

namespace ProjectXyz.Game.Tests.Functional.Stats
{
    public sealed class StatUpdaterTests
    {
        private static readonly TestData _testData;
        private static readonly TestFixture _fixture;

        static StatUpdaterTests()
        {
            _testData = new TestData();
            _fixture = new TestFixture(_testData);
        }

        public static IEnumerable<object[]> GetTimeElapsedSingleEnchantmentTestData()
        {
            yield return new object[] { "Base stat addition zero interval", _testData.Enchantments.Buffs.StatAAdditiveBaseStat, _testData.ZeroInterval, 5 };
            yield return new object[] { "Base stat addition-over-time zero interval", _testData.Enchantments.BuffsOverTime.StatABaseStat, _testData.ZeroInterval, 0 };
            yield return new object[] { "Base stat addition-over-time half interval", _testData.Enchantments.BuffsOverTime.StatABaseStat, _testData.UnitInterval.Divide(2), 5 };
            yield return new object[] { "Base stat addition-over-time one interval", _testData.Enchantments.BuffsOverTime.StatABaseStat, _testData.UnitInterval, 10 };
            yield return new object[] { "Base stat addition-over-time two intervals", _testData.Enchantments.BuffsOverTime.StatABaseStat, _testData.UnitInterval.Multiply(2), 20 };
            yield return new object[] { "Base stat addition expires zero interval", _testData.Enchantments.BuffsThatExpire.StatABaseStat, _testData.ZeroInterval, 5 };
            yield return new object[] { "Base stat addition expires half interval", _testData.Enchantments.BuffsThatExpire.StatABaseStat, _testData.UnitInterval.Divide(2), 5 };
            yield return new object[] { "Base stat addition expires one interval", _testData.Enchantments.BuffsThatExpire.StatABaseStat, _testData.UnitInterval, 5 };
            yield return new object[] { "On-demand addition expires one interval", _testData.Enchantments.BuffsThatExpire.StatAOnDemand, _testData.UnitInterval, 0 };
        }

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
    }
}
