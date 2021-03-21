using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Default
{
    public sealed class GameObjectHierarchy : IGameObjectHierarchy
    {
        private readonly Dictionary<Type, IDiscoverableGameObjectsForBehavior> _mapping;

        public GameObjectHierarchy(IEnumerable<IDiscoverableGameObjectsForBehavior> gameObjectsForBehaviors)
        {
            _mapping = gameObjectsForBehaviors.ToDictionary(
                x => x.SupportedBehaviorType,
                x => x);
        }

        public IEnumerable<IGameObject> GetChildren(
            IGameObject parent,
            bool recursive)
        {
            var queue = new Queue<IGameObject>();
            queue.Enqueue(parent);

            var yielded = new HashSet<IGameObject>();

            while (queue.Count > 0)
            {
                var currentGameObject = queue.Dequeue();

                foreach (var behavior in currentGameObject.Behaviors)
                {
                    if (!_mapping.TryGetValue(
                        behavior.GetType(),
                        out var gameObjectsForBehavior))
                    {
                        continue;
                    }

                    foreach (var child in gameObjectsForBehavior.GetChildren(behavior))
                    {
                        if (yielded.Contains(child))
                        {
                            continue;
                        }

                        yielded.Add(child);

                        if (recursive)
                        {
                            queue.Enqueue(child);
                        }

                        yield return child;
                    }
                }
            }
        }
    }
}
