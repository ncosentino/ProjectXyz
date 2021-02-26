using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.ItemSets
{
    public interface IBelongsToItemSetBehavior : IBehavior
    {
        IIdentifier ItemSetId { get; }

        IIdentifier UniqueIdWithinSet { get; }

        bool MustBeEquipped { get; }
    }
}
