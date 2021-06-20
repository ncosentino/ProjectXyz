using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.Logging;
using ProjectXyz.Api.Systems;
using ProjectXyz.Game.Core.Systems;
using ProjectXyz.Game.Interface.Engine;

namespace ProjectXyz.Game.Core.Engine
{
    public sealed class GameEngine :
        IAsyncGameEngine,
        IGameEngine
    {
        private readonly ILogger _logger;
        private readonly IReadOnlyCollection<ISystem> _systems;
        private readonly IReadOnlyCollection<ISystemUpdateComponentCreator> _systemUpdateComponentCreators;
        private readonly object _updateLoopProtectionLock;

        private bool _updateLoopProtection;

        public GameEngine(
            IEnumerable<IDiscoverableSystem> systems,
            IEnumerable<IDiscoverableSystemUpdateComponentCreator> systemUpdateComponentCreators,
            ILogger logger)
        {
            _updateLoopProtectionLock = new object();

            _logger = logger;
            _systems = systems
                .OrderBy(x => x.Priority ?? int.MaxValue)
                .ToArray();

            _logger.Debug($"Registered systems for '{this}':");
            foreach (var system in _systems)
            {
                _logger.Debug($"\t{system}");
            }

            _systemUpdateComponentCreators = systemUpdateComponentCreators
                .OrderBy(x => x.Priority ?? int.MaxValue)
                .ToArray();

            _logger.Debug($"Registered system update component creators for '{this}':");
            foreach (var systemUpdateComponentCreator in _systemUpdateComponentCreators)
            {
                _logger.Debug($"\t{systemUpdateComponentCreator}");
            }
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            await Task.Factory.StartNew(
                GameLoop,
                new StartArgs(cancellationToken),
                cancellationToken);
        }

        public async Task UpdateAsync()
        {
            lock (_updateLoopProtectionLock)
            {
                if (_updateLoopProtection)
                {
                    return;
                }

                _updateLoopProtection = true;
            }

            try
            {
                await UpdateAsync(CancellationToken.None).ConfigureAwait(false);
            }
            finally 
            {
                _updateLoopProtection = false;
            }
        }

        private async Task UpdateAsync(CancellationToken cancellationToken)
        {
            var systemUpdateComponents = new List<IComponent>();
            foreach (var systemUpdateComponentCreator in _systemUpdateComponentCreators)
            {
                var nextComponents = systemUpdateComponentCreator.CreateNext(systemUpdateComponents);
                systemUpdateComponents.AddRange(nextComponents);
            }
            
            var systemUpdateContext = new SystemUpdateContext(systemUpdateComponents);

            foreach (var system in _systems)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                await system
                    .UpdateAsync(systemUpdateContext)
                    .ConfigureAwait(false);
            }
        }

        private async Task GameLoop(object args)
        {
            _logger.Debug($"Game loop started for '{this}'.");

            var startArgs = (StartArgs)args;
            var cancellationToken = startArgs.CancellationToken;

            while (!cancellationToken.IsCancellationRequested)
            {
                await UpdateAsync(cancellationToken).ConfigureAwait(false);
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