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

        public MapGameObjectManager()
        {
            _gameObjects = new HashSet<IGameObject>();
            _gameObjectsToRemove = new HashSet<IGameObject>();
            _gameObjectsToAdd = new HashSet<IGameObject>();
        }

        public event EventHandler<GameObjectsSynchronizedEventArgs> Synchronized;

        public IReadOnlyCollection<IGameObject> GameObjects => _gameObjects;

        public void MarkForRemoval(params IGameObject[] gameObjects) =>
            MarkForRemoval((IEnumerable<IGameObject>)gameObjects);

        public void MarkForRemoval(IEnumerable<IGameObject> gameObjects)
        {
            lock (_gameObjectsToRemove)
            {
                foreach (var gameObject in gameObjects)
                {
                    _gameObjectsToRemove.Add(gameObject);
                }
            }
        }

        public void MarkForAddition(params IGameObject[] gameObjects) =>
            MarkForAddition((IEnumerable<IGameObject>)gameObjects);

        public void MarkForAddition(IEnumerable<IGameObject> gameObjects)
        {
            lock (_gameObjectsToAdd)
            {
                foreach (var gameObject in gameObjects)
                {
                    _gameObjectsToAdd.Add(gameObject);
                }
            }
        }

        public void Synchronize()
        {
            var requiredSync = false;

            var addedGameObjects = new HashSet<IGameObject>();
            lock (_gameObjectsToAdd)
            lock (_gameObjectsToRemove)
            {
                lock (_gameObjects)
                {
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
                }

                _gameObjectsToAdd.Clear();
            }

            var removedGameObjects = new HashSet<IGameObject>();
            lock (_gameObjectsToRemove)
            {
                lock (_gameObjects)
                {
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
                }

                _gameObjectsToRemove.Clear();
            }

            if (requiredSync)
            {
                Synchronized?.Invoke(
                    this,
                    new GameObjectsSynchronizedEventArgs(
                        addedGameObjects,
                        removedGameObjects));
            }
        }
    }
}
