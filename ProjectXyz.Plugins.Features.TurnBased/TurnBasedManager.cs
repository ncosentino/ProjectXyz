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

        private IReadOnlyCollection<IGameObject> _mapObjects;

        public TurnBasedManager(
            IGameObjectHierarchy gameObjectHierarchy,
            IReadOnlyMapGameObjectManager mapGameObjectManager)
        {
            _gameObjectHierarchy = gameObjectHierarchy;
            _mapGameObjectManager = mapGameObjectManager;
            _applicableGameObjects = new List<IGameObject>();
            _mapObjects = new List<IGameObject>();

            GlobalSync = true;
            SyncTurnsFromElapsedTime = true;
            ClearApplicableOnUpdate = true;

            _mapGameObjectManager.Synchronized += MapGameObjectManager_Synchronized;
        }


        public bool SyncTurnsFromElapsedTime { get; set; }

        public bool ClearApplicableOnUpdate { get; set; }

        public bool GlobalSync { get; set; }

        public IReadOnlyCollection<IGameObject> GetAllGameObjects() => _mapObjects;

        public IReadOnlyCollection<IGameObject> GetApplicableGameObjects()
        {
            if (GlobalSync)
            {
                return _mapObjects;
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
            _applicableGameObjects.Clear();
            _applicableGameObjects.AddRange(gameObjects);
        }

        private void MapGameObjectManager_Synchronized(
            object sender, 
            GameObjectsSynchronizedEventArgs e)
        {
            // NOTE: we don't need to re-copy this since the event args have
            // an immutable collection already
            _mapObjects = e.FullSet;
        }
    }
}
