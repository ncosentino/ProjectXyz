using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class ItemContainerBehavior :
        BaseBehavior,
        IItemContainerBehavior
    {
        private readonly List<IGameObject> _items;

        public ItemContainerBehavior(IIdentifier containerId)
        {
            ContainerId = containerId;
            _items = new List<IGameObject>();
        }

        public IIdentifier ContainerId { get; }

        public IReadOnlyCollection<IGameObject> Items => _items;

        public bool CanAddItem(IGameObject gameObject) => true;

        public bool TryAddItem(IGameObject gameObject)
        {
            _items.Add(gameObject);
            return true;
        }

        public bool CanRemoveItem(IGameObject gameObject) => _items.Contains(gameObject);

        public bool TryRemoveItem(IGameObject gameObject) => _items.Remove(gameObject);
    }
}
