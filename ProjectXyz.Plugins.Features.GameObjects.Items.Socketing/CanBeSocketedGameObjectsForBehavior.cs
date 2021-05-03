using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Socketing
{
    public sealed class CanBeSocketedGameObjectsForBehavior : IDiscoverableGameObjectsForBehavior
    {
        public Type SupportedBehaviorType { get; } = typeof(CanBeSocketedBehavior);

        public IEnumerable<IGameObject> GetChildren(IBehavior behavior)
        {
            var canBeSocketedBehavior = (ICanBeSocketedBehavior)behavior;
            foreach (var item in canBeSocketedBehavior
                .OccupiedSockets
                .Select(x => x.Owner)
                .Cast<IGameObject>())
            {
                yield return item;
            }
        }
    }
}