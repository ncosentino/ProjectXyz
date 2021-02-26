using System.Collections.Generic;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.ItemSets
{
    public interface IItemSetMatchingItemEnchantments
    {
        IIdentifier ItemTemplateId { get; }

        IReadOnlyCollection<IIdentifier> EnchantmentDefinitionIds { get; }

        int MinimumRequiredItemsInSet { get; }
    }
}
