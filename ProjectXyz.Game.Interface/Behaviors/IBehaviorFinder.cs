using System;

namespace ProjectXyz.Game.Interface.Behaviors
{
    public interface IBehaviorFinder
    {
        bool TryFind<TBehavior>(
            IHasBehaviors hasBehaviors,
            out Tuple<TBehavior> behaviours);

        bool TryFind<TBehavior1, TBehavior2>(
            IHasBehaviors hasBehaviors,
            out Tuple<TBehavior1, TBehavior2> behaviours);
    }
}