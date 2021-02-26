using System.Collections.Generic;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.ItemSets
{
    public interface IItemSetDefinition
    {
        bool CountEnchantmentsAreAdditive { get; }
        
        IReadOnlyDictionary<int, IReadOnlyCollection<IIdentifier>> EnchantmentsForCountsOfSet { get; }
        
        IReadOnlyCollection<IItemSetMatchingEquipSlotEnchantments> EnchantmentsForEquipSlots { get; }
        
        IReadOnlyCollection<IItemSetMatchingItemEnchantments> EnchantmentsForMatchingItems { get; }
        
        IIdentifier Id { get; }
    }
}
