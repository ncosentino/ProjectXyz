using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.ItemSets
{
    public interface IBelongsToItemSetBehavior : IBehavior
    {
        IIdentifier ItemSetId { get; }

        IIdentifier UniqueIdWithinSet { get; }

        bool MustBeEquipped { get; }
    }
}
