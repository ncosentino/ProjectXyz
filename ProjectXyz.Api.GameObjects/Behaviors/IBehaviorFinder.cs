using System;
using System.Collections.Generic;

namespace ProjectXyz.Api.GameObjects.Behaviors
{
    public interface IBehaviorFinder
    {
        bool TryFind<TBehavior>(
            IEnumerable<IBehavior> behaviors,
            out Tuple<TBehavior> matchingBehaviors);

        bool TryFind<TBehavior1, TBehavior2>(
            IEnumerable<IBehavior> behaviors,
            out Tuple<TBehavior1, TBehavior2> matchingBehaviors);

        bool TryFind<TBehavior1, TBehavior2, TBehavior3>(
            IEnumerable<IBehavior> behaviors,
            out Tuple<TBehavior1, TBehavior2, TBehavior3> matchingBehaviors);
    }
}