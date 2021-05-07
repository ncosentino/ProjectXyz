using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class StackableItemBehavior :
        BaseBehavior,
        IStackableItemBehavior
    {
        public StackableItemBehavior(
            IIdentifier stackId,
            int stackLimit)
            : this(stackId, stackLimit, 0)
        {
        }

        public StackableItemBehavior(
            IIdentifier stackId,
            int stackLimit,
            int count)
        {
            StackId = stackId;
            StackLimit = stackLimit;
            Count = count;
        }

        public IIdentifier StackId { get; }

        public int Count { get; set; }

        public int StackLimit { get; }
    }
}
