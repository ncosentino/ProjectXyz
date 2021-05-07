using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IReadOnlyStackableItemBehavior : IBehavior
    {
        IIdentifier StackId { get; }

        int Count { get; }

        int StackLimit { get; }
    }
}