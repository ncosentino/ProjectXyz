using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Items;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation
{
    public sealed class ItemDefinitionIdentifierBehavior :
        BaseBehavior,
        IItemDefinitionIdentifierBehavior
    {
        public ItemDefinitionIdentifierBehavior(IIdentifier itemDefinitionId)
        {
            ItemDefinitionId = itemDefinitionId;
        }

        public IIdentifier ItemDefinitionId { get; }
    }
}