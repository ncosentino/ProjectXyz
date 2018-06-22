using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Collections;
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

        public event EventHandler<ItemsChangedEventArgs> ItemsChanged;

        public IIdentifier ContainerId { get; }

        public IReadOnlyCollection<IGameObject> Items => _items;

        public bool CanAddItem(IGameObject gameObject) => true;

        public bool TryAddItem(IGameObject gameObject)
        {
            _items.Add(gameObject);
            ItemsChanged?.Invoke(
                this,
                new ItemsChangedEventArgs(
                    gameObject.Yield(),
                    Enumerable.Empty<IGameObject>()));
            return true;
        }

        public bool CanRemoveItem(IGameObject gameObject) => _items.Contains(gameObject);

        public bool TryRemoveItem(IGameObject gameObject)
        {
            bool removed = _items.Remove(gameObject);
            if (removed)
            {
                ItemsChanged?.Invoke(
                    this,
                    new ItemsChangedEventArgs(
                        Enumerable.Empty<IGameObject>(),
                        gameObject.Yield()));
            }

            return removed;
        }
    }
}
