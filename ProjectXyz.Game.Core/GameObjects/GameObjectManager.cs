using System.Collections.Generic;
using ProjectXyz.Game.Interface.GameObjects;

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

        public IReadOnlyCollection<IGameObject> GameObjects => _gameObjects;

        public void MarkForRemoval(IGameObject gameObject)
        {
            _gameObjectsToRemove.Add(gameObject);
        }

        public void MarkForAddition(IGameObject gameObject)
        {
            _gameObjectsToAdd.Add(gameObject);
        }

        public void Synchronize()
        {
            foreach (var gameObject in _gameObjectsToAdd)
            {
                _gameObjects.Add(gameObject);
            }

            _gameObjectsToAdd.Clear();

            foreach (var gameObject in _gameObjectsToRemove)
            {
                _gameObjects.Remove(gameObject);
            }

            _gameObjectsToRemove.Clear();
        }
    }
}
