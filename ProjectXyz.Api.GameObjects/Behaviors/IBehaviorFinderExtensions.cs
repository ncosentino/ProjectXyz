using System;

namespace ProjectXyz.Api.GameObjects.Behaviors
{
    public static class IBehaviorFinderExtensions
    {
        public static bool TryFind<TBehavior>(
            this IBehaviorFinder behaviorFinder,
            IGameObject gameObject,
            out Tuple<TBehavior> matchingBehaviors) => behaviorFinder.TryFind(
                gameObject.Behaviors,
                out matchingBehaviors);

        public static bool TryFind<TBehavior1, TBehavior2>(
            this IBehaviorFinder behaviorFinder,
            IGameObject gameObject,
            out Tuple<TBehavior1, TBehavior2> matchingBehaviors) => behaviorFinder.TryFind(
                gameObject.Behaviors,
                out matchingBehaviors);

        public static bool TryFind<TBehavior1, TBehavior2, TBehavior3>(
            this IBehaviorFinder behaviorFinder,
            IGameObject gameObject,
            out Tuple<TBehavior1, TBehavior2, TBehavior3> matchingBehaviors) => behaviorFinder.TryFind(
                gameObject.Behaviors,
                out matchingBehaviors);
    }
}