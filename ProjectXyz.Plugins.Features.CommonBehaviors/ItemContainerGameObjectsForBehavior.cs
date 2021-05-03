using System;
using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class ItemContainerGameObjectsForBehavior : IDiscoverableGameObjectsForBehavior
    {
        public Type SupportedBehaviorType { get; } = typeof(ItemContainerBehavior);

        public IEnumerable<IGameObject> GetChildren(IBehavior behavior)
        {
            var itemContainerBehavior = (IReadOnlyItemContainerBehavior)behavior;
            foreach (var item in itemContainerBehavior.Items)
            {
                yield return item;
            }
        }
    }
}
