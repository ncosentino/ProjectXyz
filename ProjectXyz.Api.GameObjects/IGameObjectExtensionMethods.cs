using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Api.GameObjects
{
    public static class IGameObjectExtensionMethods
    {
        public static IEnumerable<TBehavior> Get<TBehavior>(this IGameObject gameObject)
            where TBehavior : IBehavior
        {
            return gameObject
                .Behaviors
                .Get<TBehavior>();
        }

        public static bool Has<TBehavior>(this IGameObject gameObject)
            where TBehavior : IBehavior
        {
            return gameObject
                .Behaviors
                .Get<TBehavior>()
                .Any();
        }

        public static TBehavior GetOnly<TBehavior>(this IGameObject gameObject)
            where TBehavior : IBehavior
        {
            return gameObject
                .Behaviors
                .Get<TBehavior>()
                .Single();
        }
    }
}