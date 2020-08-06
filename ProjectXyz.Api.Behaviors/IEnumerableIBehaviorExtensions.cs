using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Api.Behaviors
{
    public static class IEnumerableIBehaviorExtensions
    {
        public static TBehavior GetFirst<TBehavior>(this IEnumerable<IBehavior> behaviors)
            where TBehavior : IBehavior
        {
            var behavior = behaviors
                .Where(x => x is TBehavior)
                .Cast<TBehavior>()
                .FirstOrDefault();
            if (behavior == null)
            {
                throw new InvalidOperationException($"Could not find a behavior of type '{typeof(TBehavior)}'.");
            }

            return behavior;
        }

        public static bool TryGetFirst<TBehavior>(
            this IEnumerable<IBehavior> behaviors,
            out TBehavior behavior)
            where TBehavior : IBehavior
        {
            behavior = behaviors
                .Where(x => x is TBehavior)
                .Cast<TBehavior>()
                .FirstOrDefault();
            return behavior != null;
        }

        public static TBehavior GetOnly<TBehavior>(this IEnumerable<IBehavior> behaviors)
            where TBehavior : IBehavior
        {
            return behaviors
                .Where(x => x is TBehavior)
                .Cast<TBehavior>()
                .Single();
        }

        public static bool Has<TBehavior>(this IEnumerable<IBehavior> behaviors)
            where TBehavior : IBehavior
        {
            var behavior = behaviors
                .Where(x => x is TBehavior)
                .Cast<TBehavior>()
                .FirstOrDefault();
            return behavior != null;
        }
    }
}