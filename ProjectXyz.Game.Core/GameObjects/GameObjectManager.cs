using System;
using System.Collections.Generic;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Game.Core.GameObjects
{
    public sealed class GameObjectManager : IMutableGameObjectManager
    {
        private readonly HashSet<IGameObject> _gameObjects;
        private readonly HashSet<IGameObject> _gameObjectsToRemove;
        private readonly HashSet<IGameObject> _gameObjectsToAdd;

        public GameObjectManager()
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
            foreach (var gameObject in gameObjects)
            {
                _gameObjectsToRemove.Add(gameObject);
            }
        }

        public void MarkForAddition(params IGameObject[] gameObjects) =>
            MarkForAddition((IEnumerable<IGameObject>)gameObjects);

        public void MarkForAddition(IEnumerable<IGameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                _gameObjectsToAdd.Add(gameObject);
            }
        }

        public void Synchronize()
        {
            var requiredSync = false;
            foreach (var gameObject in _gameObjectsToAdd)
            {
                _gameObjects.Add(gameObject);
                requiredSync = true;
            }

            foreach (var gameObject in _gameObjectsToRemove)
            {
                _gameObjects.Remove(gameObject);
                requiredSync = true;
            }

            if (requiredSync)
            {
                Synchronized?.Invoke(
                    this,
                    new GameObjectsSynchronizedEventArgs(
                        _gameObjectsToAdd,
                        _gameObjectsToRemove));
            }

            _gameObjectsToAdd.Clear();
            _gameObjectsToRemove.Clear();
        }
    }
}
