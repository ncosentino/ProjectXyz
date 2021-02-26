using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.ItemSets
{
    public sealed class ItemSetEnchantmentBehavior :
        BaseBehavior,
        IItemSetEnchantmentBehavior
    {
        public ItemSetEnchantmentBehavior(
            IIdentifier itemSetId,
            IIdentifier enchantmentDefinitionId)
        {
            ItemSetId = itemSetId;
            EnchantmentDefinitionId = enchantmentDefinitionId;
        }

        public IIdentifier ItemSetId { get; }

        public IIdentifier EnchantmentDefinitionId { get; }
    }
}
