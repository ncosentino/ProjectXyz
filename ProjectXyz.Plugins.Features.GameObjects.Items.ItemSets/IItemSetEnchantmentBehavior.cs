
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.ItemSets
{
    public interface IItemSetEnchantmentBehavior : IBehavior
    {
        IIdentifier ItemSetId { get; }

        IIdentifier EnchantmentDefinitionId { get; }
    }
}
