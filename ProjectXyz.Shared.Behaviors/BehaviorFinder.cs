using System;
using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Shared.Behaviors
{
    public sealed class BehaviorFinder : IBehaviorFinder
    {
        public bool TryFind<TBehavior>(
            IHasBehaviors hasBehaviors,
            out Tuple<TBehavior> behaviors)
        {
            var b1 = default(TBehavior);
            foreach (var behavior in hasBehaviors.Behaviors)
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

            behaviors = Tuple.Create(b1);
            return b1 != null;
        }

        public bool TryFind<TBehavior1, TBehavior2>(
            IHasBehaviors hasBehaviors,
            out Tuple<TBehavior1, TBehavior2> behaviors)
        {
            var b1 = default(TBehavior1);
            var b2 = default(TBehavior2);
            foreach (var behavior in hasBehaviors.Behaviors)
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

            behaviors = Tuple.Create(b1, b2);
            return b1 != null && b2 != null;
        }

        public bool TryFind<TBehavior1, TBehavior2, TBehavior3>(
            IHasBehaviors hasBehaviors,
            out Tuple<TBehavior1, TBehavior2, TBehavior3> behaviors)
        {
            var b1 = default(TBehavior1);
            var b2 = default(TBehavior2);
            var b3 = default(TBehavior3);
            foreach (var behavior in hasBehaviors.Behaviors)
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

            behaviors = Tuple.Create(b1, b2, b3);
            return b1 != null && b2 != null && b3 != null;
        }
    }
}