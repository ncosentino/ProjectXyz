using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments
{
    public sealed class EnchantmentsChangedEventArgs : EventArgs
    {
        public EnchantmentsChangedEventArgs(
            IEnumerable<IGameObject> addedEnchantments,
            IEnumerable<IGameObject> removedEnchantments)
        {
            AddedEnchantments = addedEnchantments.AsFrozenCollection();
            RemovedEnchantments = removedEnchantments.AsFrozenCollection();
        }

        public IFrozenCollection<IGameObject> AddedEnchantments { get; }

        public IFrozenCollection<IGameObject> RemovedEnchantments { get; }
    }
}