using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.ItemSets
{
    public sealed class ItemSetMatchingEquipSlotEnchantments : IItemSetMatchingEquipSlotEnchantments
    {
        public ItemSetMatchingEquipSlotEnchantments(
            IIdentifier equipSlotId,
            IEnumerable<IIdentifier> enchantmentDefinitionIds,
            int minimumRequiredItemsInSet)
        {
            EquipSlotId = equipSlotId;
            EnchantmentDefinitionIds = enchantmentDefinitionIds.ToArray();
            MinimumRequiredItemsInSet = minimumRequiredItemsInSet;
        }

        public IIdentifier EquipSlotId { get; }

        public IReadOnlyCollection<IIdentifier> EnchantmentDefinitionIds { get; }

        public int MinimumRequiredItemsInSet { get; }
    }
}
