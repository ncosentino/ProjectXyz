using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.ItemSets
{
    public sealed class ItemSetMatchingItemEnchantments : IItemSetMatchingItemEnchantments
    {
        public ItemSetMatchingItemEnchantments(
            IIdentifier itemTemplateId,
            IEnumerable<IIdentifier> enchantmentDefinitionIds,
            int minimumRequiredItemsInSet)
        {
            ItemTemplateId = itemTemplateId;
            EnchantmentDefinitionIds = enchantmentDefinitionIds.ToArray();
            MinimumRequiredItemsInSet = minimumRequiredItemsInSet;
        }

        public IIdentifier ItemTemplateId { get; }

        public IReadOnlyCollection<IIdentifier> EnchantmentDefinitionIds { get; }

        public int MinimumRequiredItemsInSet { get; }
    }
}
