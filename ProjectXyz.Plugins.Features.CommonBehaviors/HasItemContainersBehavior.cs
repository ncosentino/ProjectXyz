using System;
using System.Collections.Generic;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class HasItemContainersBehavior :
        BaseBehavior,
        IHasItemContainersBehavior
    {
        private readonly List<IItemContainerBehavior> _containers;

        public HasItemContainersBehavior()
        {
            _containers = new List<IItemContainerBehavior>();
        }

        public IReadOnlyCollection<IItemContainerBehavior> ItemContainers => _containers;

        public void AddItemContainer(IItemContainerBehavior itemContainerBehavior)
            => _containers.Add(itemContainerBehavior);

        public bool RemoveItemContainer(IItemContainerBehavior itemContainerBehavior)
            => _containers.Remove(itemContainerBehavior);
    }
}
