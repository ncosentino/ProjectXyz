using System;

namespace ProjectXyz.Api.Behaviors
{
    public interface IBehaviorFinder
    {
        bool TryFind<TBehavior>(
            IHasBehaviors hasBehaviors,
            out Tuple<TBehavior> behaviours);

        bool TryFind<TBehavior1, TBehavior2>(
            IHasBehaviors hasBehaviors,
            out Tuple<TBehavior1, TBehavior2> behaviours);

        bool TryFind<TBehavior1, TBehavior2, TBehavior3>(
            IHasBehaviors hasBehaviors,
            out Tuple<TBehavior1, TBehavior2, TBehavior3> behaviors);
    }
}