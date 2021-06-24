using System.Collections.Generic;
using System.Linq;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping;

namespace ProjectXyz.Plugins.Features.TurnBased.Default
{
    public sealed class TurnBasedManager : ITurnBasedManager
    {
        private readonly IGameObjectHierarchy _gameObjectHierarchy;
        private readonly IReadOnlyMapGameObjectManager _mapGameObjectManager;
        
        // don't mark as read-only, despite all of the best practices we try to
        // do. this collection will get re-assigned as to not modify it while
        // enumerating it. safer this way.
        private List<IGameObject> _applicableGameObjects;

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

        public IReadOnlyCollection<IGameObject> GetAllGameObjects() => _mapGameObjectManager.GameObjects;

        public IReadOnlyCollection<IGameObject> GetApplicableGameObjects()
        {
            if (GlobalSync)
            {
                return GetAllGameObjects();
            }

            // FIXME: why is it that we get the child objects only when we
            // have applicable objects set? why aren't we doing this for
            // the other map objects normally? also another important note
            // here that might not be obvious, but getting the children
            // likely should be done on a per update (not cached) basis
            // because the children could change between update frames...
            // we could change the API to let applicable objects caller
            // do this for us, but if we need the overall map objects to
            // provide children as well, we probably need that on a
            // per-update basis.
            var applicableAndChildren = _applicableGameObjects
                .Concat(_applicableGameObjects
                    .SelectMany(x => _gameObjectHierarchy.GetChildren(
                        x,
                        true)))
                .ToArray();
            return applicableAndChildren;
        }

        public void SetApplicableObjects(IEnumerable<IGameObject> gameObjects)
        {
            Contract.RequiresNotNull(
                gameObjects,
                $"{nameof(gameObjects)} cannot be null. Use an empty enumerable.");

            var applicableGameObjects = new List<IGameObject>();
            foreach (var gameObject in gameObjects)
            {
                Contract.RequiresNotNull(
                    gameObject,
                    $"One of the game objects in the provided argument " +
                    $"'{nameof(gameObjects)}' was null.");
                applicableGameObjects.Add(gameObject);
            }

            // re-assign entire list so we don't risk enumeration over changing collections
            _applicableGameObjects = applicableGameObjects;
        }
    }
}
