using System;

using NexusLabs.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.TurnBased.Default
{
    public sealed class ActionInfo : IActionInfo
    {
        private readonly Lazy<IFrozenHashSet<IGameObject>> _lazyAllGameObjectsResolved;

        public ActionInfo(
            IGameObject actor,
            IFrozenCollection<IGameObject> applicableGameObjects,
            Lazy<IFrozenHashSet<IGameObject>> lazyAllGameObjectsResolved,
            double elapsedActions)
        {
            Actor = actor;
            ApplicableGameObjects = applicableGameObjects;
            _lazyAllGameObjectsResolved = lazyAllGameObjectsResolved;
            ElapsedActions = elapsedActions;
        }

        public IGameObject Actor { get; }

        public IFrozenCollection<IGameObject> ApplicableGameObjects { get; }

        public double ElapsedActions { get; }

        public IFrozenHashSet<IGameObject> AllGameObjects => _lazyAllGameObjectsResolved.Value;
    }
}
