using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Systems;
using ProjectXyz.Game.Core.Systems;
using ProjectXyz.Game.Interface.Engine;

namespace ProjectXyz.Game.Core.Engine
{
    public sealed class GameEngine : IGameEngine
    {
        private readonly IGameObjectManager _gameObjectManager;
        private readonly IReadOnlyCollection<ISystem> _systems;
        private readonly IReadOnlyCollection<ISystemUpdateComponentCreator> _systemUpdateComponentCreators;

        public GameEngine(
            IGameObjectManager gameObjectManager,
            IEnumerable<ISystem> systems,
            IEnumerable<ISystemUpdateComponentCreator> systemUpdateComponentCreators)
        {
            _gameObjectManager = gameObjectManager;
            _systems = systems.ToArray();
            _systemUpdateComponentCreators = systemUpdateComponentCreators.ToArray();
        }

        public async Task Start(CancellationToken cancellationToken)
        {
            await Task.Factory.StartNew(
                GameLoop,
                new StartArgs(cancellationToken),
                cancellationToken);
        }

        private void GameLoop(object args)
        {
            var startArgs = (StartArgs)args;
            var cancellationToken = startArgs.CancellationToken;

            while (!cancellationToken.IsCancellationRequested)
            {
                var systemUpdateComponents = _systemUpdateComponentCreators.Select(x => x.CreateNext());
                var systemUpdateContext = new SystemUpdateContext(systemUpdateComponents);

                foreach (var system in _systems)
                {
                    system.Update(
                        systemUpdateContext,
                        _gameObjectManager.GameObjects);
                }
            }
        }

        private sealed class StartArgs
        {
            public StartArgs(CancellationToken cancellationToken)
            {
                CancellationToken = cancellationToken;
            }

            public CancellationToken CancellationToken { get; }
        }
    }
}