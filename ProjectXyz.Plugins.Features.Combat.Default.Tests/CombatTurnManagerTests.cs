using System.Collections.Generic;
using System.Linq;

using Moq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Combat.Api;

using Xunit;

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
        private void GetSnapshot_7InitialProgress2Turns5After_5AfterAreLast5OfFirst()
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

            _combatTurnManager.Reset();
            var results1 = _combatTurnManager
                .GetSnapshot(_filterContext.Object, 7)
                .ToArray();
            _combatTurnManager.ProgressTurn(
                _filterContext.Object,
                2);
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
        }
    }
}
