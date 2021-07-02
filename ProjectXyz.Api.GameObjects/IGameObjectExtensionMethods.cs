using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Api.GameObjects
{
    public static class IGameObjectExtensionMethods
    {
        public static IEnumerable<TBehavior> Get<TBehavior>(this IGameObject gameObject)
            where TBehavior : IBehavior
        {
            Contract.RequiresNotNull(
                gameObject,
                () => $"Cannot call extension method '{nameof(Get)}' on a null object.");

            return gameObject
                .Behaviors
                .Get<TBehavior>();
        }

        public static bool Has<TBehavior>(this IGameObject gameObject)
            where TBehavior : IBehavior
        {
            Contract.RequiresNotNull(
                gameObject,
                () => $"Cannot call extension method '{nameof(Has)}' on a null object.");

            return gameObject
                .Behaviors
                .Get<TBehavior>()
                .Any();
        }

        public static TBehavior GetOnly<TBehavior>(this IGameObject gameObject)
            where TBehavior : IBehavior
        {
            Contract.RequiresNotNull(
                gameObject,
                () => $"Cannot call extension method '{nameof(GetOnly)}' on a null object.");

            var match = gameObject
                .Behaviors
                .Get<TBehavior>()
                .SingleOrDefault();
            Contract.RequiresNotNull(
                match,
                () => 
                {
                    var count = gameObject
                        .Behaviors
                        .Get<TBehavior>()
                        .Count();
                    return $"Game object '{gameObject}' had {count} behaviors " +
                        $"matching type '{typeof(TBehavior)}' when only one was " +
                        $"expected.";
                });

            return match;
        }

        public static TBehavior GetFirst<TBehavior>(this IGameObject gameObject)
            where TBehavior : IBehavior
        {
            Contract.RequiresNotNull(
                gameObject,
                () => $"Cannot call extension method '{nameof(GetFirst)}' on a null object.");

            return gameObject
                .Behaviors
                .GetFirst<TBehavior>();
        }

        public static bool TryGetFirst<TBehavior>(
            this IGameObject gameObject,
            out TBehavior behavior)
            where TBehavior : IBehavior
        {
            Contract.RequiresNotNull(
                gameObject,
                () => $"Cannot call extension method '{nameof(TryGetFirst)}' on a null object.");

            return gameObject
                .Behaviors
                .TryGetFirst(out behavior);
        }
    }
}