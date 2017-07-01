using System;
using System.Collections.Generic;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Game.Interface.Behaviors;
using ProjectXyz.Game.Interface.Stats;
using ProjectXyz.Game.Interface.Systems;

namespace ProjectXyz.Game.Core.Systems
{
    public sealed class StatUpdaterSystem : ISystem
    {
        private readonly IBehaviorFinder _behaviorFinder;
        private readonly IStatUpdater _statUpdater;

        public StatUpdaterSystem(
            IBehaviorFinder behaviorFinder,
            IStatUpdater statUpdater)
        {
            _behaviorFinder = behaviorFinder;
            _statUpdater = statUpdater;
        }

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IHasBehaviors> hasBehaviors)
        {
            var elapsed = systemUpdateContext
                .GetFirst<IComponent<IElapsedTime>>()
                .Value
                .Interval;

            foreach (var hasBehavior in hasBehaviors)
            {
                Tuple<IHasEnchantments, IHasMutableStats> behaviours;
                if (!_behaviorFinder.TryFind(hasBehavior, out behaviours))
                {
                    continue;
                }

                _statUpdater.Update(
                    behaviours.Item2.Stats,
                    behaviours.Item1.Enchantments,
                    behaviours.Item2.MutateStats,
                    elapsed);
            }
        }
    }
}