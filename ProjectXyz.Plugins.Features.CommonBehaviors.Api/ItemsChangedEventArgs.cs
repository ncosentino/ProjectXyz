using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public sealed class ItemsChangedEventArgs : EventArgs
    {
        public ItemsChangedEventArgs(
            IEnumerable<IGameObject> addedItems,
            IEnumerable<IGameObject> removedItems)
        {
            AddedItems = addedItems.ToReadOnlyCollection();
            RemovedItems = removedItems.ToReadOnlyCollection();
        }

        public IReadOnlyCollection<IGameObject> AddedItems { get; }

        public IReadOnlyCollection<IGameObject> RemovedItems { get; }
    }
}