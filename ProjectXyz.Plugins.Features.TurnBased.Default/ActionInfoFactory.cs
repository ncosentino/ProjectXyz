using System;
using System.Linq;

using NexusLabs.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.TurnBased.Default
{
    public sealed class ActionInfoFactory : IActionInfoFactory
    {
        private readonly IGameObjectHierarchy _gameObjectHierarchy;

        public ActionInfoFactory(IGameObjectHierarchy gameObjectHierarchy)
        {
            _gameObjectHierarchy = gameObjectHierarchy;
        }

        public IActionInfo Create(
            IGameObject actor,
            IFrozenCollection<IGameObject> applicableGameObjects,
            double elapsedActions)
        {
            var actionInfo = new ActionInfo(
                actor,
                applicableGameObjects,
                new Lazy<IFrozenHashSet<IGameObject>>(() => new FrozenHashSet<IGameObject>(applicableGameObjects
                    .Concat(applicableGameObjects.SelectMany(x => _gameObjectHierarchy.GetChildren(
                        x,
                        true))))),
                elapsedActions);
            return actionInfo;
        }
    }
}
