using System;
using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.Systems;
using ProjectXyz.Framework.Entities.Extensions;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Stats;

namespace ProjectXyz.Plugins.Features.BaseStatEnchantments.Systems
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
                Tuple<IHasEnchantmentsBehavior, IHasMutableStatsBehavior> behaviours;
                if (!_behaviorFinder.TryFind(hasBehavior, out behaviours))
                {
                    continue;
                }

                _statUpdater.Update(
                    behaviours.Item2.BaseStats,
                    behaviours.Item1.Enchantments,
                    behaviours.Item2.MutateStats,
                    elapsed);
            }
        }
    }
}