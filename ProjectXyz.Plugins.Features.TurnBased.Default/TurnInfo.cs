﻿using System;

using NexusLabs.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.TurnBased.Default
{
    public sealed class TurnInfo : ITurnInfo
    {
        private readonly Lazy<IFrozenHashSet<IGameObject>> _lazyAllGameObjectsResolved;

        public TurnInfo(
            IGameObject actor,
            IFrozenCollection<IGameObject> applicableGameObjects,
            Lazy<IFrozenHashSet<IGameObject>> lazyAllGameObjectsResolved,
            double elapsedTurns)
        {
            Actor = actor;
            ApplicableGameObjects = applicableGameObjects;
            _lazyAllGameObjectsResolved = lazyAllGameObjectsResolved;
            ElapsedTurns = elapsedTurns;
        }

        public IGameObject Actor { get; }

        public IFrozenCollection<IGameObject> ApplicableGameObjects { get; }

        public IFrozenHashSet<IGameObject> AllGameObjects => _lazyAllGameObjectsResolved.Value;

        public double ElapsedTurns { get; }
    }
}
