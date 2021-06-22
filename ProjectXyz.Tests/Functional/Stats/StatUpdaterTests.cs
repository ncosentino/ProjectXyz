using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Autofac;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Game.Interface.Engine;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.ElapsedTime;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.Mapping;
using ProjectXyz.Plugins.Features.TurnBased;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Tests.Functional.TestingData;

using Xunit;

namespace ProjectXyz.Tests.Functional.Stats
{
    public sealed class StatUpdaterTests
    {
        private static readonly TestData _testData;
        private static readonly TestFixture _fixture;
        private static readonly IGameEngine _gameEngine;
        private static readonly ITurnBasedManager _turnBasedManager;
        private static readonly IMapGameObjectManager _mapGameObjectManager;
        private static readonly IManualTimeProvider _manualTimeProvider;
        private static readonly IHasEnchantmentsBehaviorFactory _hasEnchantmentsBehaviorFactory;
        private static readonly IHasMutableStatsBehaviorFactory _hasMutableStatsBehaviorFactory;

        static StatUpdaterTests()
        {
            _testData = new TestData();
            _fixture = new TestFixture(_testData);
            _gameEngine = _fixture.LifeTimeScope.Resolve<IGameEngine>();
            _turnBasedManager = _fixture.LifeTimeScope.Resolve<ITurnBasedManager>();
            _mapGameObjectManager = _fixture.LifeTimeScope.Resolve<IMapGameObjectManager>();
            _manualTimeProvider = _fixture.LifeTimeScope.Resolve<IManualTimeProvider>();
            _hasEnchantmentsBehaviorFactory = _fixture.LifeTimeScope.Resolve<IHasEnchantmentsBehaviorFactory>();
            _hasMutableStatsBehaviorFactory = _fixture.LifeTimeScope.Resolve<IHasMutableStatsBehaviorFactory>();
        }

        public static IEnumerable<object[]> GetGlobalTimeElapsedSingleEnchantmentTestData()
        {
            yield return new object[] { "Base stat addition zero turns", _testData.Enchantments.Buffs.StatAAdditiveBaseStat, 0, 5 };
            yield return new object[] { "Base stat addition one turn", _testData.Enchantments.Buffs.StatAAdditiveBaseStat, 1, 5 };
            yield return new object[] { "Base stat addition-over-time zero turns", _testData.Enchantments.BuffsOverTime.StatABaseStat, 0, 0 };
            yield return new object[] { "Base stat addition-over-time half turn", _testData.Enchantments.BuffsOverTime.StatABaseStat, 0.5, 5 };
            yield return new object[] { "Base stat addition-over-time one turn", _testData.Enchantments.BuffsOverTime.StatABaseStat, 1, 10 };
            yield return new object[] { "Base stat addition-over-time two turns", _testData.Enchantments.BuffsOverTime.StatABaseStat, 2, 20 };
            yield return new object[] { "Base stat interval-ignorant addition expires zero turns", _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant, 0, 5 };
            yield return new object[] { "Base stat interval-ignorant addition expires half turn", _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant, 0.5, 5 };
            yield return new object[] { "Base stat interval-ignorant addition expires one turn", _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant, 1, 5 };
            yield return new object[] { "Base stat interval-ignorant addition expires 10 turns", _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant, 10, 5 }; // 10 seconds over 1 update
            yield return new object[] { "Base stat interval-ignorant addition expires 15 turns", _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant, 15, 5 }; // 15 seconds over 1 update
            yield return new object[] { "Base stat addition expires zero turns", _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10Turns, 0, 0 };
            yield return new object[] { "Base stat addition expires half turn", _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10Turns, 0.5, 2.5 };
            yield return new object[] { "Base stat addition expires one turn", _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10Turns, 1, 5 };
            yield return new object[] { "Base stat addition expires 10 turns", _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10Turns, 10, 50 }; // 10 seconds over 1 update
            yield return new object[] { "Base stat addition expires 15 turns", _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10Turns, 15, 50 }; // 15 seconds over 1 update
            yield return new object[] { "On-demand addition expires one turn", _testData.Enchantments.BuffsThatExpire.StatAOnDemandAfter10TurnsIntervalIgnorant, 1, 0 };
        }

        public static IEnumerable<object[]> GetGlobalTurnsElapsedSingleEnchantmentTestData()
        {
            yield return new object[] { "Base stat addition zero turns", _testData.Enchantments.Buffs.StatAAdditiveBaseStat, 0, 0 };
            yield return new object[] { "Base stat addition one turn", _testData.Enchantments.Buffs.StatAAdditiveBaseStat, 1, 5 };
            yield return new object[] { "Base stat addition-over-time zero turns", _testData.Enchantments.BuffsOverTime.StatABaseStat, 0, 0 };
            yield return new object[] { "Base stat addition-over-time one turn", _testData.Enchantments.BuffsOverTime.StatABaseStat, 1, 10 };
            yield return new object[] { "Base stat addition-over-time two turns", _testData.Enchantments.BuffsOverTime.StatABaseStat, 2, 20 };
            yield return new object[] { "Base stat interval-ignorant addition expires zero turns", _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant, 0, 0 };
            yield return new object[] { "Base stat interval-ignorant addition expires one turn", _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant, 1, 5 };
            yield return new object[] { "Base stat interval-ignorant addition expires 10 turns", _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant, 10, 50 }; // 10 explicit turns
            yield return new object[] { "Base stat interval-ignorant addition expires 15 turns", _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant, 15, 50 }; // 15 explicit turns
            yield return new object[] { "On-demand addition expires one turn", _testData.Enchantments.BuffsThatExpire.StatAOnDemandAfter10TurnsIntervalIgnorant, 1, 0 };
        }

        [Theory,
         MemberData(nameof(GetGlobalTimeElapsedSingleEnchantmentTestData))]
        private async Task GlobalTimeElapsed_SingleBaseStatEnchantment_ExpectedBaseStat(
            string _,
            IGameObject enchantment,
            double elapsedTurns,
            double expectedValue)
        {
            // Setup
            var baseTime = DateTime.UtcNow;
            _manualTimeProvider.SetTimeUtc(baseTime);
            await _gameEngine.UpdateAsync();

            var hasMutableStats = _hasMutableStatsBehaviorFactory.Create();
            var hasEnchantments = _hasEnchantmentsBehaviorFactory.Create();
            var gameObject = new TestGameObject(new IBehavior[]
            {
                hasEnchantments,
                hasMutableStats,
            });

            hasEnchantments.AddEnchantments(enchantment);
            
            // FIXME: do a proper conversion here
            var elapsedSeconds = elapsedTurns;

            UsingCleanTurnBasedmanager(() =>
            UsingCleanMapGameObjectManager(async () =>
            {
                _turnBasedManager.GlobalSync = true;
                _turnBasedManager.SyncTurnsFromElapsedTime = true;

                _mapGameObjectManager.MarkForAddition(gameObject);
                _mapGameObjectManager.Synchronize();

                // Execute
                _manualTimeProvider.SetTimeUtc(baseTime.AddSeconds(elapsedSeconds));
                await _gameEngine.UpdateAsync();
            }));

            // Assert
            Assert.Equal(
                expectedValue,
                hasMutableStats.BaseStats.GetValueOrDefault(enchantment.Behaviors.GetOnly<IHasStatDefinitionIdBehavior>().StatDefinitionId));
        }

        [Theory,
         MemberData(nameof(GetGlobalTurnsElapsedSingleEnchantmentTestData))]
        private void GlobalTurnsElapsed_SingleBaseStatEnchantment_ExpectedBaseStat(
            string _,
            IGameObject enchantment,
            double elapsedTurns,
            double expectedValue)
        {
            // Setup
            var hasMutableStats = _hasMutableStatsBehaviorFactory.Create();
            var hasEnchantments = _hasEnchantmentsBehaviorFactory.Create();
            var gameObject = new TestGameObject(new IBehavior[]
            {
                hasEnchantments,
                hasMutableStats,
            });

            hasEnchantments.AddEnchantments(enchantment);
            
            UsingCleanTurnBasedmanager(() =>
            UsingCleanMapGameObjectManager(async () =>
            {
                _turnBasedManager.GlobalSync = true;
                _turnBasedManager.SyncTurnsFromElapsedTime = false; // no time syncing

                _mapGameObjectManager.MarkForAddition(gameObject);
                _mapGameObjectManager.Synchronize();

                // Execute
                for (int i = 0; i < elapsedTurns; i++)
                {
                    await _gameEngine.UpdateAsync();
                }
            }));

            // Assert
            Assert.Equal(
                expectedValue,
                hasMutableStats.BaseStats.GetValueOrDefault(enchantment.Behaviors.GetOnly<IHasStatDefinitionIdBehavior>().StatDefinitionId));
        }

        [Fact]
        private void SpecificTurnsElapsed_MatchFromMap_ExpectedBaseStat()
        {
            // Setup
            var hasMutableStats = _hasMutableStatsBehaviorFactory.Create();
            var hasEnchantments = _hasEnchantmentsBehaviorFactory.Create();
            var gameObject = new TestGameObject(new IBehavior[]
            {
                hasEnchantments,
                hasMutableStats,
            });

            var enchantment = _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant;
            hasEnchantments.AddEnchantments(enchantment);
            
            UsingCleanTurnBasedmanager(() =>
            UsingCleanMapGameObjectManager(async () =>
            {
                _turnBasedManager.GlobalSync = false;
                _turnBasedManager.SyncTurnsFromElapsedTime = false; // no time syncing
                _turnBasedManager.ClearApplicableOnUpdate = false;
                _turnBasedManager.SetApplicableObjects(new[]
                {
                    gameObject,
                });

                _mapGameObjectManager.MarkForAddition(gameObject);
                _mapGameObjectManager.Synchronize();

                // Execute
                await _gameEngine.UpdateAsync();
            }));

            // Assert
            Assert.Equal(
                5,
                hasMutableStats.BaseStats.GetValueOrDefault(enchantment.Behaviors.GetOnly<IHasStatDefinitionIdBehavior>().StatDefinitionId));
        }

        [Fact]
        private void SpecificTurnsElapsed_NoMatchFromMap_ExpectedBaseStat()
        {
            // Setup
            var hasMutableStats = _hasMutableStatsBehaviorFactory.Create();
            var hasEnchantments = _hasEnchantmentsBehaviorFactory.Create();
            var gameObject = new TestGameObject(new IBehavior[]
            {
                hasEnchantments,
                hasMutableStats,
            });

            var enchantment = _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant;
            hasEnchantments.AddEnchantments(enchantment);
            
            UsingCleanTurnBasedmanager(() =>
            UsingCleanMapGameObjectManager(async () =>
            {
                _turnBasedManager.GlobalSync = false;
                _turnBasedManager.SyncTurnsFromElapsedTime = false; // no time syncing
                _turnBasedManager.ClearApplicableOnUpdate = false;
                _turnBasedManager.SetApplicableObjects(new IGameObject[]
                {
                });

                _mapGameObjectManager.MarkForAddition(gameObject);
                _mapGameObjectManager.Synchronize();

                // Execute
                await _gameEngine.UpdateAsync();
            }));

            // Assert
            Assert.Equal(
                0,
                hasMutableStats.BaseStats.GetValueOrDefault(enchantment.Behaviors.GetOnly<IHasStatDefinitionIdBehavior>().StatDefinitionId));
        }

        [Fact]
        private void SpecificTurnsElapsed_ItemInPlayerInventory_ExpectedBaseStat()
        {
            // Setup
            var actorFactory = _fixture.LifeTimeScope.Resolve<IActorFactory>();
            var actor = actorFactory.Create(
                new IdentifierBehavior(new StringIdentifier("actor")),
                new[]
                {
                    new ItemContainerBehavior(new StringIdentifier("inventory")),
                });

            var item = new TestGameObject(new IBehavior[]
            {
                _hasEnchantmentsBehaviorFactory.Create(),
                _hasMutableStatsBehaviorFactory.Create(),
            });
            Assert.True(
                actor
                    .GetOnly<IItemContainerBehavior>()
                    .TryAddItem(item),
                $"Could not add '{item}' to item container of '{actor}'.");

            var enchantment = _testData.Enchantments.BuffsThatExpire.StatABaseStatAfter10TurnsIntervalIgnorant;
            item.GetOnly<IHasEnchantmentsBehavior>().AddEnchantments(enchantment);

            UsingCleanTurnBasedmanager(() =>
            UsingCleanMapGameObjectManager(async () =>
            {
                _turnBasedManager.GlobalSync = false;
                _turnBasedManager.SyncTurnsFromElapsedTime = false; // no time syncing
                _turnBasedManager.ClearApplicableOnUpdate = false;
                _turnBasedManager.SetApplicableObjects(new IGameObject[]
                {
                    actor
                });

                _mapGameObjectManager.MarkForAddition(actor);
                _mapGameObjectManager.Synchronize();

                // Execute
                await _gameEngine.UpdateAsync();
            }));

            // Assert
            Assert.Equal(
                5,
                item.GetOnly<IHasMutableStatsBehavior>().BaseStats.GetValueOrDefault(enchantment.Behaviors.GetOnly<IHasStatDefinitionIdBehavior>().StatDefinitionId));
        }

        private void UsingCleanTurnBasedmanager(Action callback)
        {
            ResetTurnBasedManager();
            try
            {
                callback();
            }
            finally
            {
                ResetTurnBasedManager();
            }
        }

        private void ResetTurnBasedManager()
        {
            _turnBasedManager.GlobalSync = true;
            _turnBasedManager.SyncTurnsFromElapsedTime = true;
            _turnBasedManager.ClearApplicableOnUpdate = true;
            _turnBasedManager.SetApplicableObjects(new IGameObject[] { });
        }

        private void UsingCleanMapGameObjectManager(Func<Task> callback)
        {
            _mapGameObjectManager.MarkForRemoval(_mapGameObjectManager.GameObjects);
            _mapGameObjectManager.Synchronize();
            try
            {
                callback();
            }
            finally
            {
                _mapGameObjectManager.MarkForRemoval(_mapGameObjectManager.GameObjects);
                _mapGameObjectManager.Synchronize();
            }
        }

        public sealed class TestGameObject : IGameObject
        {
            public TestGameObject(IEnumerable<IBehavior> behaviors)
            {
                Behaviors = behaviors.ToArray();
            }

            public IReadOnlyCollection<IBehavior> Behaviors { get; }
        }

        public interface IManualTimeProvider : IRealTimeProvider
        {
            void SetTimeUtc(DateTime dateTime);
        }

        public sealed class ManualTimeProvider : IManualTimeProvider
        {
            private DateTime _dateTime;

            public DateTime GetTimeUtc() => _dateTime;

            public void SetTimeUtc(DateTime dateTime)
            {
                _dateTime = dateTime;
            }
        }

        public sealed class TestModule : Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                base.Load(builder);

                var trace = new System.Diagnostics.StackTrace();
                if (trace.ToString().IndexOf(nameof(StatUpdaterTests), StringComparison.Ordinal) == -1)
                {
                    return;
                }

                builder
                    .RegisterType<ManualTimeProvider>()
                    .AsImplementedInterfaces()
                    .SingleInstance();
            }
        }
    }
}
