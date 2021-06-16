using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class MapGameObjectManager : IMapGameObjectManager
    {
        private readonly HashSet<IGameObject> _gameObjects;
        private readonly HashSet<IGameObject> _gameObjectsToRemove;
        private readonly HashSet<IGameObject> _gameObjectsToAdd;
        private readonly object _skipSynchronizationLock;

        private bool _skipSynchronization;

        public MapGameObjectManager()
        {
            _gameObjects = new HashSet<IGameObject>();
            _gameObjectsToRemove = new HashSet<IGameObject>();
            _gameObjectsToAdd = new HashSet<IGameObject>();
            _skipSynchronizationLock = new object();
        }

        public event EventHandler<GameObjectsSynchronizedEventArgs> Synchronized;

        public IReadOnlyCollection<IGameObject> GameObjects => _gameObjects;

        public bool IsSynchronizing { get; private set; }

        public void MarkForRemoval(params IGameObject[] gameObjects) =>
            MarkForRemoval((IEnumerable<IGameObject>)gameObjects);

        public void MarkForRemoval(IEnumerable<IGameObject> gameObjects)
        {
            PreventSynchronizationDuring(() =>
            {
                foreach (var gameObject in gameObjects)
                {
                    _gameObjectsToRemove.Add(gameObject);
                }
            });
        }

        public void MarkForAddition(params IGameObject[] gameObjects) =>
            MarkForAddition((IEnumerable<IGameObject>)gameObjects);

        public void MarkForAddition(IEnumerable<IGameObject> gameObjects)
        {
            PreventSynchronizationDuring(() =>
            {
                foreach (var gameObject in gameObjects)
                {
                    _gameObjectsToAdd.Add(gameObject);
                }
            });
        }

        public void ClearGameObjectsImmediate()
        {
            _gameObjectsToAdd.Clear();
            _gameObjectsToRemove.Clear();
            var copy = GameObjects.ToArray();
            MarkForRemoval(copy);
            Synchronize();
        }

        public void Synchronize()
        {
            if (_skipSynchronization)
            {
                lock (_skipSynchronizationLock)
                {
                    if (_skipSynchronization)
                    {
                        return;
                    }
                }
            }

            IsSynchronizing = true;
            try
            {
                var requiredSync = false;

                var addedGameObjects = new HashSet<IGameObject>();
                foreach (var gameObject in _gameObjectsToAdd.Where(x => !_gameObjectsToRemove.Contains(x)))
                {
                    // in some situations we're "adding" objects to the map
                    // even though they're already present just to ensure
                    // they stick around, but there's no actual net change
                    if (_gameObjects.Contains(gameObject))
                    {
                        continue;
                    }

                    _gameObjects.Add(gameObject);
                    addedGameObjects.Add(gameObject);
                    requiredSync = true;
                }

                _gameObjectsToAdd.Clear();

                var removedGameObjects = new HashSet<IGameObject>();
                foreach (var gameObject in _gameObjectsToRemove)
                {
                    if (!_gameObjects.Contains(gameObject))
                    {
                        continue;
                    }

                    _gameObjects.Remove(gameObject);
                    removedGameObjects.Add(gameObject);
                    requiredSync = true;
                }

                _gameObjectsToRemove.Clear();

                if (requiredSync)
                {
                    Synchronized?.Invoke(
                        this,
                        new GameObjectsSynchronizedEventArgs(
                            addedGameObjects,
                            removedGameObjects,
                            _gameObjects));
                }
            }
            finally
            {
                IsSynchronizing = false;
            }
        }

        private void PreventSynchronizationDuring(Action callback)
        {
            var saveState = _skipSynchronization;
            lock (_skipSynchronizationLock)
            {
                _skipSynchronization = true;
            }

            try
            {
                callback();
            }
            finally
            {
                lock (_skipSynchronizationLock)
                {
                    _skipSynchronization = saveState;
                }
            }
        }
    }
}
