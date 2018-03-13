using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Api.Behaviors
{
    public interface IBehaviorCollection : IReadOnlyCollection<IBehavior>
    {
        IEnumerable<TBehavior> Get<TBehavior>()
            where TBehavior : IBehavior;
    }

    public static class IBehaviorCollectionExtensions
    {
        public static TBehavior GetFirst<TBehavior>(this IBehaviorCollection behaviors)
            where TBehavior : IBehavior
        {
            var behavior = behaviors
                .Get<TBehavior>()
                .FirstOrDefault();
            if (behavior == null)
            {
                throw new InvalidOperationException($"Could not find a behavior of type '{typeof(TBehavior)}'.");
            }

            return behavior;
        }

        public static bool TryGetFirst<TBehavior>(
            this IBehaviorCollection behaviors,
            out TBehavior behavior)
            where TBehavior : IBehavior
        {
            behavior = behaviors
                .Get<TBehavior>()
                .FirstOrDefault();
            return behavior != null;
        }

        public static bool Has<TBehavior>(this IBehaviorCollection behaviors)
            where TBehavior : IBehavior
        {
            var behavior = behaviors
                .Get<TBehavior>()
                .FirstOrDefault();
            return behavior != null;
        }
    }
}