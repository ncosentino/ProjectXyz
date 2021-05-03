using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Api.GameObjects.Behaviors
{
    public static class IEnumerableIBehaviorExtensions
    {
        public static TBehavior GetFirst<TBehavior>(this IEnumerable<IBehavior> behaviors)
            where TBehavior : IBehavior
        {
            var behavior = behaviors
                .TakeTypes<TBehavior>()
                .FirstOrDefault();
            if (behavior == null)
            {
                throw new InvalidOperationException(
                    $"Could not find a behavior of type '{typeof(TBehavior)}'.");
            }

            return behavior;
        }

        public static IEnumerable<TBehavior> Get<TBehavior>(this IEnumerable<IBehavior> behaviors)
            where TBehavior : IBehavior
        {
            return behaviors.TakeTypes<TBehavior>();
        }

        public static bool TryGetFirst<TBehavior>(
            this IEnumerable<IBehavior> behaviors,
            out TBehavior behavior)
            where TBehavior : IBehavior
        {
            behavior = behaviors
                .TakeTypes<TBehavior>()
                .FirstOrDefault();
            return behavior != null;
        }

        public static TBehavior GetOnly<TBehavior>(this IEnumerable<IBehavior> behaviors)
            where TBehavior : IBehavior
        {
            var matches = behaviors
                .TakeTypes<TBehavior>()
                .ToArray();
            if (matches.Length != 1)
            {
                var count = behaviors
                    .TakeTypes<TBehavior>()
                    .Count();
                throw new InvalidOperationException(
                    $"Enumeration found {count} behaviors " +
                    $"matching type '{typeof(TBehavior)}' when only one was " +
                    $"expected.");
            }

            return matches[0];
        }

        public static bool Has<TBehavior>(this IEnumerable<IBehavior> behaviors)
            where TBehavior : IBehavior
        {
            var behavior = behaviors
                .TakeTypes<TBehavior>()
                .FirstOrDefault();
            return behavior != null;
        }
    }
}