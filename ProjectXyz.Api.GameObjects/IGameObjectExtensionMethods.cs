using System;
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
            var match = gameObject
                .Behaviors
                .Get<TBehavior>()
                .SingleOrDefault();
            if (match == null)
            {
                var count = gameObject
                    .Behaviors
                    .Get<TBehavior>()
                    .Count();
                throw new InvalidOperationException(
                    $"Game object '{gameObject}' had {count} behaviors " +
                    $"matching type '{typeof(TBehavior)}' when only one was " +
                    $"expected.");
            }

            return match;
        }
    }
}