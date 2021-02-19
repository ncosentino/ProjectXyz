using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Api.GameObjects
{
    public static class IHasBehaviorsExtensionMethods
    {
        public static IEnumerable<TBehavior> Get<TBehavior>(this IHasBehaviors hasBehaviors)
            where TBehavior : IBehavior
        {
            return hasBehaviors
                .Behaviors
                .Get<TBehavior>();
        }

        public static bool Has<TBehavior>(this IHasBehaviors hasBehaviors)
            where TBehavior : IBehavior
        {
            return hasBehaviors
                .Behaviors
                .Has<TBehavior>();
        }

        public static TBehavior GetOnly<TBehavior>(this IHasBehaviors hasBehaviors)
            where TBehavior : IBehavior
        {
            return hasBehaviors
                .Behaviors
                .GetOnly<TBehavior>();
        }

        public static TBehavior GetFirst<TBehavior>(this IHasBehaviors hasBehaviors)
            where TBehavior : IBehavior
        {
            return hasBehaviors
                .Behaviors
                .GetFirst<TBehavior>();
        }

        public static bool TryGetFirst<TBehavior>(
            this IHasBehaviors hasBehaviors,
            out TBehavior behavior)
            where TBehavior : IBehavior
        {
            return hasBehaviors
                .Behaviors
                .TryGetFirst(out behavior);
        }
    }
}