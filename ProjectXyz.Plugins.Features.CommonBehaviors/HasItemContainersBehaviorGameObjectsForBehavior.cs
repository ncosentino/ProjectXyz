using System;
using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class HasItemContainersBehaviorGameObjectsForBehavior : IDiscoverableGameObjectsForBehavior
    {
        public Type SupportedBehaviorType { get; } = typeof(HasItemContainersBehavior);

        public IEnumerable<IGameObject> GetChildren(IBehavior behavior)
        {
            var hasItemContainersBehavior = (IReadOnlyHasItemContainersBehavior)behavior;
            foreach (var itemContainerBehavior in hasItemContainersBehavior.ItemContainers)
            {
                foreach (var item in itemContainerBehavior.Items)
                {
                    yield return item;
                }
            }
        }
    }
}
