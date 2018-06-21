using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Api.Systems;
using ProjectXyz.Game.Core.Systems;
using ProjectXyz.Game.Interface.Engine;

namespace ProjectXyz.Game.Core.Engine
{
    public sealed class GameEngine : IAsyncGameEngine
    {
        private readonly ILogger _logger;
        private readonly IGameObjectManager _gameObjectManager;
        private readonly IReadOnlyCollection<ISystem> _systems;
        private readonly IReadOnlyCollection<ISystemUpdateComponentCreator> _systemUpdateComponentCreators;

        public GameEngine(
            IGameObjectManager gameObjectManager,
            IEnumerable<ISystem> systems,
            IEnumerable<ISystemUpdateComponentCreator> systemUpdateComponentCreators,
            ILogger logger)
        {
            _gameObjectManager = gameObjectManager;
            _logger = logger;
            _systems = systems.ToArray();

            _logger.Debug($"Registered systems for '{this}':");
            foreach (var system in _systems)
            {
                _logger.Debug($"\t{system}");
            }

            _systemUpdateComponentCreators = systemUpdateComponentCreators.ToArray();
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            await Task.Factory.StartNew(
                GameLoop,
                new StartArgs(cancellationToken),
                cancellationToken);
        }

        private void GameLoop(object args)
        {
            _logger.Debug($"Game loop started for '{this}'.");

            var startArgs = (StartArgs)args;
            var cancellationToken = startArgs.CancellationToken;

            while (!cancellationToken.IsCancellationRequested)
            {
                var systemUpdateComponents = _systemUpdateComponentCreators.Select(x => x.CreateNext());
                var systemUpdateContext = new SystemUpdateContext(systemUpdateComponents);

                foreach (var system in _systems)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

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