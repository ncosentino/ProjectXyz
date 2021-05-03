using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.ItemSets
{
    public interface IItemSetEnchantmentBehavior : IBehavior
    {
        IIdentifier ItemSetId { get; }

        IIdentifier EnchantmentDefinitionId { get; }
    }
}
