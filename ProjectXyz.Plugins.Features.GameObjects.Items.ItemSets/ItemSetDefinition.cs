using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.ItemSets
{
    public sealed class ItemSetDefinition : IItemSetDefinition
    {
        public ItemSetDefinition(
            IIdentifier id,
            IReadOnlyDictionary<int, IReadOnlyCollection<IIdentifier>> enchantmentsForCountsOfSet,
            bool countEnchantmentsAreAdditive,
            IEnumerable<IItemSetMatchingEquipSlotEnchantments> enchantmentsForEquipSlots,
            IEnumerable<IItemSetMatchingItemEnchantments> enchantmentsForMatchingItems)
        {
            Id = id;
            EnchantmentsForCountsOfSet = enchantmentsForCountsOfSet;
            CountEnchantmentsAreAdditive = countEnchantmentsAreAdditive;
            EnchantmentsForEquipSlots = enchantmentsForEquipSlots.ToArray();
            EnchantmentsForMatchingItems = enchantmentsForMatchingItems.ToArray();
        }

        public IIdentifier Id { get; }

        public IReadOnlyDictionary<int, IReadOnlyCollection<IIdentifier>> EnchantmentsForCountsOfSet { get; }

        public bool CountEnchantmentsAreAdditive { get; }

        public IReadOnlyCollection<IItemSetMatchingEquipSlotEnchantments> EnchantmentsForEquipSlots { get; }

        public IReadOnlyCollection<IItemSetMatchingItemEnchantments> EnchantmentsForMatchingItems { get; }
    }
}
