using System.Collections.Generic;
using System.Linq;
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
                .Get<TBehavior>()
                .Any();
        }

        public static TBehavior GetOnly<TBehavior>(this IHasBehaviors hasBehaviors)
            where TBehavior : IBehavior
        {
            return hasBehaviors
                .Behaviors
                .GetOnly<TBehavior>();
        }
    }
}