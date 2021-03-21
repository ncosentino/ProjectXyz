using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.TurnBased.Api;

namespace ProjectXyz.Plugins.Features.TurnBased
{
    public sealed class TurnBasedManager : ITurnBasedManager
    {
        private readonly IGameObjectHierarchy _gameObjectHierarchy;
        private readonly IReadOnlyMapGameObjectManager _mapGameObjectManager;
        private readonly List<IGameObject> _applicableGameObjects;

        public TurnBasedManager(
            IGameObjectHierarchy gameObjectHierarchy,
            IReadOnlyMapGameObjectManager mapGameObjectManager)
        {
            _gameObjectHierarchy = gameObjectHierarchy;
            _mapGameObjectManager = mapGameObjectManager;
            _applicableGameObjects = new List<IGameObject>();
            GlobalSync = true;
            SyncTurnsFromElapsedTime = true;
            ClearApplicableOnUpdate = true;
        }

        public bool SyncTurnsFromElapsedTime { get; set; }

        public bool ClearApplicableOnUpdate { get; set; }

        public bool GlobalSync { get; set; }

        public IReadOnlyCollection<IGameObject> ApplicableGameObjects => GlobalSync
            ? _mapGameObjectManager.GameObjects
            : _applicableGameObjects
                .Concat(_applicableGameObjects
                    .SelectMany(x => _gameObjectHierarchy.GetChildren(
                        x,
                        true)))
                .ToArray();

        public void SetApplicableObjects(IEnumerable<IGameObject> gameObjects)
        {
            _applicableGameObjects.Clear();
            _applicableGameObjects.AddRange(gameObjects);
        }
    }
}
