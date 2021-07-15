using System;
using System.Linq;

using NexusLabs.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.TurnBased.Default
{
    public sealed class TurnInfoFactory : ITurnInfoFactory
    {
        private readonly IGameObjectHierarchy _gameObjectHierarchy;

        public TurnInfoFactory(IGameObjectHierarchy gameObjectHierarchy)
        {
            _gameObjectHierarchy = gameObjectHierarchy;
        }

        public ITurnInfo Create(
            IGameObject actor,
            IFrozenCollection<IGameObject> applicableGameObjects,
            double elapsedTurns)
        {
            var turnInfo = new TurnInfo(
                actor,
                applicableGameObjects,
                new Lazy<IFrozenHashSet<IGameObject>>(() => new FrozenHashSet<IGameObject>(applicableGameObjects
                    .Concat(applicableGameObjects.SelectMany(x => _gameObjectHierarchy.GetChildren(
                        x,
                        true))))),
                elapsedTurns);
            return turnInfo;
        }
    }
}
