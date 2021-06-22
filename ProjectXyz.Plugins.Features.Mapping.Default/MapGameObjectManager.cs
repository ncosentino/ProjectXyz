﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class MapGameObjectManager : IMapGameObjectManager
    {
        private readonly ConcurrentDictionary<IGameObject, bool> _gameObjects;
        private readonly ConcurrentDictionary<IGameObject, bool> _gameObjectsToRemove;
        private readonly ConcurrentDictionary<IGameObject, bool> _gameObjectsToAdd;
        private readonly object _skipSynchronizationLock;

        private bool _skipSynchronization;

        public MapGameObjectManager()
        {
            _gameObjects = new ConcurrentDictionary<IGameObject, bool>();
            _gameObjectsToRemove = new ConcurrentDictionary<IGameObject, bool>();
            _gameObjectsToAdd = new ConcurrentDictionary<IGameObject, bool>();
            _skipSynchronizationLock = new object();
            GameObjects = new IGameObject[0];
        }

        public event EventHandler<GameObjectsSynchronizedEventArgs> Synchronized;

        public IReadOnlyCollection<IGameObject> GameObjects { get; private set; }

        public bool IsSynchronizing { get; private set; }

        public void MarkForRemoval(params IGameObject[] gameObjects) =>
            MarkForRemoval((IEnumerable<IGameObject>)gameObjects);

        public void MarkForRemoval(IEnumerable<IGameObject> gameObjects)
        {
            PreventSynchronizationDuring(() =>
            {
                foreach (var gameObject in gameObjects)
                {
                    _gameObjectsToAdd.TryAdd(gameObject, true);
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
                    _gameObjectsToAdd.TryAdd(gameObject, true);
                }
            });
        }

        public void ClearGameObjectsImmediate()
        {
            PreventSynchronizationDuring(() =>
            {
                _gameObjectsToAdd.Clear();
                _gameObjectsToRemove.Clear();
                MarkForRemoval(GameObjects);
            });

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
                foreach (var gameObject in _gameObjectsToAdd.Keys.Where(x => !_gameObjectsToRemove.ContainsKey(x)))
                {
                    // in some situations we're "adding" objects to the map
                    // even though they're already present just to ensure
                    // they stick around, but there's no actual net change
                    if (_gameObjects.ContainsKey(gameObject))
                    {
                        continue;
                    }

                    _gameObjects.TryAdd(gameObject, true);
                    addedGameObjects.Add(gameObject);
                    requiredSync = true;
                }

                _gameObjectsToAdd.Clear();

                var removedGameObjects = new HashSet<IGameObject>();
                foreach (var gameObject in _gameObjectsToRemove.Keys)
                {
                    if (!_gameObjects.ContainsKey(gameObject))
                    {
                        continue;
                    }

                    _gameObjects.TryRemove(gameObject, out _);
                    removedGameObjects.Add(gameObject);
                    requiredSync = true;
                }

                _gameObjectsToRemove.Clear();

                if (requiredSync)
                {
                    GameObjects = _gameObjects.Keys.ToArray();
                    Synchronized?.Invoke(
                        this,
                        new GameObjectsSynchronizedEventArgs(
                            addedGameObjects,
                            removedGameObjects,
                            GameObjects));
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
