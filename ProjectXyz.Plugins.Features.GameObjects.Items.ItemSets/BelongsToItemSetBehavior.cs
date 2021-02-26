using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.ItemSets
{
    public sealed class BelongsToItemSetBehavior :
        BaseBehavior,
        IBelongsToItemSetBehavior
    {
        public BelongsToItemSetBehavior(
            IIdentifier itemSetId,
            IIdentifier uniqueIdWithinSet,
            bool mustBeEquipped)
        {
            ItemSetId = itemSetId;
            UniqueIdWithinSet = uniqueIdWithinSet;
            MustBeEquipped = mustBeEquipped;
        }

        public IIdentifier ItemSetId { get; }

        public IIdentifier UniqueIdWithinSet { get; }

        public bool MustBeEquipped { get; }
    }
}
