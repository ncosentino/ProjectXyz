using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors
{
    public sealed class MergeStatsActorBehaviorsInterceptor : IDiscoverableActorBehaviorsInterceptor
    {
        private readonly IHasMutableStatsBehaviorFactory _hasMutableStatsBehaviorFactory;

        public MergeStatsActorBehaviorsInterceptor(IHasMutableStatsBehaviorFactory hasMutableStatsBehaviorFactory)
        {
            _hasMutableStatsBehaviorFactory = hasMutableStatsBehaviorFactory;
        }

        public int Priority { get; } = 0;

        public IEnumerable<IBehavior> Intercept(IReadOnlyCollection<IBehavior> behaviors)
        {
            var hasStatsBehaviors = behaviors
                .Get<IHasStatsBehavior>()
                .ToArray();
            if (hasStatsBehaviors.Length < 2)
            {
                return behaviors;
            }

            // FIXME: we probably want to respect whether or not the stats
            // that existed were mutable or not. currently this will blow all
            // the stats away and put a mutable behavior in place.

            var mutableStatsBehavior = _hasMutableStatsBehaviorFactory.Create();
            mutableStatsBehavior.MutateStats(stats =>
            {
                foreach (var hasStatsBehavior in hasStatsBehaviors)
                {
                    foreach (var stat in hasStatsBehavior.BaseStats)
                    {
                        stats[stat.Key] = stat.Value;
                    }
                }
            });

            return behaviors
                .Where(x => !(x is IHasStatsBehavior))
                .AppendSingle(mutableStatsBehavior);
        }
    }
}
