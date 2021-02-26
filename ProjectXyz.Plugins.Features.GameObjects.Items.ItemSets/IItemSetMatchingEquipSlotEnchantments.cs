using System.Collections.Generic;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.ItemSets
{
    public interface IItemSetMatchingEquipSlotEnchantments
    {
        IIdentifier EquipSlotId { get; }

        IReadOnlyCollection<IIdentifier> EnchantmentDefinitionIds { get; }

        int MinimumRequiredItemsInSet { get; }
    }
}
