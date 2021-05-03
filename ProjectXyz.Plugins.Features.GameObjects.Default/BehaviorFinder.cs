using System;
using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.Behaviors.Default
{
    public sealed class BehaviorFinder : IBehaviorFinder
    {
        public bool TryFind<TBehavior>(
            IEnumerable<IBehavior> behaviors,
            out Tuple<TBehavior> matchingBehaviors)
        {
            var b1 = default(TBehavior);
            foreach (var behavior in behaviors)
            {
                if (behavior is TBehavior && b1 == null)
                {
                    b1 = (TBehavior)behavior;
                }

                if (b1 != null)
                {
                    break;
                }
            }

            matchingBehaviors = Tuple.Create(b1);
            return b1 != null;
        }

        public bool TryFind<TBehavior1, TBehavior2>(
            IEnumerable<IBehavior> behaviors,
            out Tuple<TBehavior1, TBehavior2> matchingBehaviors)
        {
            var b1 = default(TBehavior1);
            var b2 = default(TBehavior2);
            foreach (var behavior in behaviors)
            {
                if (behavior is TBehavior1 && b1 == null)
                {
                    b1 = (TBehavior1)behavior;
                }
                else if (behavior is TBehavior2 && b2 == null)
                {
                    b2 = (TBehavior2)behavior;
                }

                if (b1 != null && b2 != null)
                {
                    break;
                }
            }

            matchingBehaviors = Tuple.Create(b1, b2);
            return b1 != null && b2 != null;
        }

        public bool TryFind<TBehavior1, TBehavior2, TBehavior3>(
            IEnumerable<IBehavior> behaviors,
            out Tuple<TBehavior1, TBehavior2, TBehavior3> matchingBehaviors)
        {
            var b1 = default(TBehavior1);
            var b2 = default(TBehavior2);
            var b3 = default(TBehavior3);
            foreach (var behavior in behaviors)
            {
                if (behavior is TBehavior1 && b1 == null)
                {
                    b1 = (TBehavior1)behavior;
                }
                else if (behavior is TBehavior2 && b2 == null)
                {
                    b2 = (TBehavior2)behavior;
                }
                else if (behavior is TBehavior3 && b3 == null)
                {
                    b3 = (TBehavior3)behavior;
                }

                if (b1 != null && b2 != null && b3 != null)
                {
                    break;
                }
            }

            matchingBehaviors = Tuple.Create(b1, b2, b3);
            return b1 != null && b2 != null && b3 != null;
        }
    }
}