﻿using System.Collections.Generic;
using System.Linq;

using Moq;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Combat.Api;

using Xunit;
using System.Threading.Tasks;
using System;

namespace ProjectXyz.Plugins.Features.Combat.Default.Tests
{
    public sealed class CombatTurnManagerTests
    {
        private readonly MockRepository _mockRepository;
        private readonly CombatTurnManager _combatTurnManager;
        private readonly Mock<ICombatCalculations> _combatCalculations;
        private readonly Mock<ICombatGameObjectProvider> _combatGameObjectManager;
        private readonly Mock<IFilterContext> _filterContext;

        public CombatTurnManagerTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _combatCalculations = _mockRepository.Create<ICombatCalculations>();
            _combatGameObjectManager = _mockRepository.Create<ICombatGameObjectProvider>();
            _filterContext = _mockRepository.Create<IFilterContext>();
            _combatTurnManager = new CombatTurnManager(
                _combatCalculations.Object,
                _combatGameObjectManager.Object);
        }

        [Fact]
        private void GetSnapshot_NoActors_Empty()
        {
            _combatGameObjectManager
                .Setup(x => x.GetGameObjects())
                .Returns(Array.Empty<IGameObject>());

            var results = _combatTurnManager
                .GetSnapshot(_filterContext.Object, 1)
                .ToArray();

            Assert.Empty(results);
            _mockRepository.VerifyAll();
        }

        [Fact]
        private async Task GetSnapshot_AfterStartCombatSnapshotLength1_UsesCache()
        {
            var gameObjectA1 = _mockRepository.Create<IGameObject>();
            var gameObjectB1 = _mockRepository.Create<IGameObject>();
            var gameObjectB2 = _mockRepository.Create<IGameObject>();

            _combatGameObjectManager
                .Setup(x => x.GetGameObjects())
                .Returns(new[]
                {
                    gameObjectA1.Object,
                    gameObjectB1.Object,
                    gameObjectB2.Object,
                });

            var speedMapping = new Dictionary<IGameObject, double>()
            {
                [gameObjectA1.Object] = 10,
                [gameObjectB1.Object] = 8,
                [gameObjectB2.Object] = 4,
            };

            _combatCalculations
                .Setup(x => x.CalculateActorIncrementValue)
                .Returns(new CombatCalculation<double>((context, actors, actor) => speedMapping[actor]));
            _combatCalculations
                .Setup(x => x.CalculateActorRequiredTargetValuePerTurn)
                .Returns(new CombatCalculation<double>((context, actors, actor) => 100));

            await _combatTurnManager
                .StartCombatAsync(_filterContext.Object)
                .ConfigureAwait(false);
            var results = _combatTurnManager
                .GetSnapshot(_filterContext.Object, 1)
                .ToArray();

            Assert.Equal(gameObjectA1.Object, Assert.Single(results));
            _mockRepository.VerifyAll();

            // FIXME: this is a pretty crap way to ensure we used a cached value but it works for now?
            _combatGameObjectManager.Verify(x => x.GetGameObjects(), Times.Once);
        }

        [Fact]
        private async Task GetSnapshot_After1TurnProgressedSnapshotLength1_UsesCache()
        {
            var gameObjectA1 = _mockRepository.Create<IGameObject>();
            var gameObjectB1 = _mockRepository.Create<IGameObject>();
            var gameObjectB2 = _mockRepository.Create<IGameObject>();

            _combatGameObjectManager
                .Setup(x => x.GetGameObjects())
                .Returns(new[]
                {
                    gameObjectA1.Object,
                    gameObjectB1.Object,
                    gameObjectB2.Object,
                });

            var speedMapping = new Dictionary<IGameObject, double>()
            {
                [gameObjectA1.Object] = 10,
                [gameObjectB1.Object] = 8,
                [gameObjectB2.Object] = 4,
            };

            _combatCalculations
                .Setup(x => x.CalculateActorIncrementValue)
                .Returns(new CombatCalculation<double>((context, actors, actor) => speedMapping[actor]));
            _combatCalculations
                .Setup(x => x.CalculateActorRequiredTargetValuePerTurn)
                .Returns(new CombatCalculation<double>((context, actors, actor) => 100));

            await _combatTurnManager
                .StartCombatAsync(_filterContext.Object)
                .ConfigureAwait(false);
            await _combatTurnManager
                .ProgressTurnAsync(_filterContext.Object, 1)
                .ConfigureAwait(false);
            var results = _combatTurnManager
                .GetSnapshot(_filterContext.Object, 1)
                .ToArray();

            Assert.Equal(gameObjectB1.Object, Assert.Single(results));
            _mockRepository.VerifyAll();

            // FIXME: this is a pretty crap way to ensure we used a cached value but it works for now?
            _combatGameObjectManager.Verify(x => x.GetGameObjects(), Times.Exactly(3));
        }

        [Fact]
        private async Task GetSnapshot_7InitialProgress2Turns5After_5AfterAreLast5OfFirst()
        {
            var gameObjectA1 = _mockRepository.Create<IGameObject>();
            var gameObjectB1 = _mockRepository.Create<IGameObject>();
            var gameObjectB2 = _mockRepository.Create<IGameObject>();

            _combatGameObjectManager
                .Setup(x => x.GetGameObjects())
                .Returns(new[]
                {
                    gameObjectA1.Object,
                    gameObjectB1.Object,
                    gameObjectB2.Object,
                });

            var speedMapping = new Dictionary<IGameObject, double>()
            {
                [gameObjectA1.Object] = 10,
                [gameObjectB1.Object] = 8,
                [gameObjectB2.Object] = 4,
            };

            _combatCalculations
                .Setup(x => x.CalculateActorIncrementValue)
                .Returns(new CombatCalculation<double>((context, actors, actor) => speedMapping[actor]));
            _combatCalculations
                .Setup(x => x.CalculateActorRequiredTargetValuePerTurn)
                .Returns(new CombatCalculation<double>((context, actors, actor) => 100));

            await _combatTurnManager
                .StartCombatAsync(_filterContext.Object)
                .ConfigureAwait(false);
            var results1 = _combatTurnManager
                .GetSnapshot(_filterContext.Object, 7)
                .ToArray();
            await _combatTurnManager
                .ProgressTurnAsync(
                    _filterContext.Object,
                    2)
                .ConfigureAwait(false);
            var results2 = _combatTurnManager
                .GetSnapshot(_filterContext.Object, 5)
                .ToArray();

            Assert.Equal(7, results1.Length);
            Assert.Equal(5, results2.Length);

            Assert.Equal(gameObjectA1.Object, results1[0]);
            Assert.Equal(gameObjectB1.Object, results1[1]);
            Assert.Equal(gameObjectA1.Object, results1[2]);
            Assert.Equal(gameObjectB1.Object, results1[3]);
            Assert.Equal(gameObjectB2.Object, results1[4]);
            Assert.Equal(gameObjectA1.Object, results1[5]);
            Assert.Equal(gameObjectB1.Object, results1[6]);
            
            Assert.Equal<IGameObject>(
                results1.Skip(2),
                results2);
            _mockRepository.VerifyAll();
        }

        [InlineData(6, new[] { 0, 1, 0, 1, 2, 0 }, 1)]
        [InlineData(5, new[] { 0, 1, 0, 1, 2 }, 0)]
        [InlineData(4, new[] { 0, 1, 0, 1 }, 2)]
        [InlineData(3, new[] { 0, 1, 0 }, 1)]
        [InlineData(2, new[] { 0, 1 }, 0)]
        [InlineData(1, new[] { 0 }, 1)]
        [Theory]
        private async Task ProgressTurn_VariableTurns_ExpectedEventArgs(
            int numberOfTurns,
            int[] expectedProgressedActorIndices,
            int expectedNextActorIndex)
        {
            var gameObjectA1 = _mockRepository.Create<IGameObject>(); // actor 0
            var gameObjectB1 = _mockRepository.Create<IGameObject>(); // actor 1
            var gameObjectB2 = _mockRepository.Create<IGameObject>(); // actor 2

            var possibleActors = new[]
            {
                gameObjectA1.Object,
                gameObjectB1.Object,
                gameObjectB2.Object,
            };

            _combatGameObjectManager
                .Setup(x => x.GetGameObjects())
                .Returns(new[]
                {
                    gameObjectA1.Object,
                    gameObjectB1.Object,
                    gameObjectB2.Object,
                });

            var speedMapping = new Dictionary<IGameObject, double>()
            {
                [gameObjectA1.Object] = 10,
                [gameObjectB1.Object] = 8,
                [gameObjectB2.Object] = 4,
            };

            _combatCalculations
                .Setup(x => x.CalculateActorIncrementValue)
                .Returns(new CombatCalculation<double>((context, actors, actor) => speedMapping[actor]));
            _combatCalculations
                .Setup(x => x.CalculateActorRequiredTargetValuePerTurn)
                .Returns(new CombatCalculation<double>((context, actors, actor) => 100));

            var turnProgressedCount = 0;
            _combatTurnManager.TurnProgressed += (s, e) =>
            {
                Assert.Equal(numberOfTurns, e.ActorTurnProgression.Count);
                Assert.Equal(expectedProgressedActorIndices.Select(x => possibleActors[x]), e.ActorTurnProgression);
                Assert.Equal(possibleActors[expectedNextActorIndex], e.ActorWithNextTurn);
                turnProgressedCount++;
            };

            await _combatTurnManager
                .ProgressTurnAsync(
                    _filterContext.Object,
                    numberOfTurns)
                .ConfigureAwait(false);

            Assert.Equal(1, turnProgressedCount);
            _mockRepository.VerifyAll();
        }

        [Fact]
        private async Task CombatStarted_3ActorsVariableSpeed_ExpectedEventArgs()
        {
            var gameObjectA1 = _mockRepository.Create<IGameObject>();
            var gameObjectB1 = _mockRepository.Create<IGameObject>();
            var gameObjectB2 = _mockRepository.Create<IGameObject>();

            _combatGameObjectManager
                .Setup(x => x.GetGameObjects())
                .Returns(new[]
                {
                    gameObjectA1.Object,
                    gameObjectB1.Object,
                    gameObjectB2.Object,
                });

            var speedMapping = new Dictionary<IGameObject, double>()
            {
                [gameObjectA1.Object] = 10,
                [gameObjectB1.Object] = 8,
                [gameObjectB2.Object] = 4,
            };

            _combatCalculations
                .Setup(x => x.CalculateActorIncrementValue)
                .Returns(new CombatCalculation<double>((context, actors, actor) => speedMapping[actor]));
            _combatCalculations
                .Setup(x => x.CalculateActorRequiredTargetValuePerTurn)
                .Returns(new CombatCalculation<double>((context, actors, actor) => 100));

            var combatStartedCount = 0;
            _combatTurnManager.CombatStarted += (s, e) =>
            {
                var firstActor = Assert.Single(e.ActorOrder);
                Assert.Equal(gameObjectA1.Object, firstActor);
                combatStartedCount++;
            };

            await _combatTurnManager
                .StartCombatAsync(_filterContext.Object)
                .ConfigureAwait(false);

            Assert.Equal(1, combatStartedCount);
            _mockRepository.VerifyAll();
        }
    }
}
