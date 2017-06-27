using System;
using ProjectXyz.Game.Interface.Behaviors;

namespace ProjectXyz.Game.Core.Behaviors
{
    public sealed class BehaviorFinder : IBehaviorFinder
    {
        public bool TryFind<TBehavior>(
            IHasBehaviors hasBehaviors,
            out Tuple<TBehavior> behaviours)
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

            behaviours = Tuple.Create(b1);
            return b1 != null;
        }

        public bool TryFind<TBehavior1, TBehavior2>(
            IHasBehaviors hasBehaviors,
            out Tuple<TBehavior1, TBehavior2> behaviours)
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

            behaviours = Tuple.Create(b1, b2);
            return b1 != null && b2 != null;
        }
    }
}