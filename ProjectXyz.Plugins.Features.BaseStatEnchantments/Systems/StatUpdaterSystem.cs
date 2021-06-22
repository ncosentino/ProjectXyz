﻿using System;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.BaseStatEnchantments.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.TurnBased;

namespace ProjectXyz.Plugins.Features.BaseStatEnchantments.Systems
{
    public sealed class StatUpdaterSystem : IDiscoverableSystem
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

        public int? Priority => null;

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
            var turnInfo = systemUpdateContext
                .GetFirst<IComponent<ITurnInfo>>()
                .Value;

            foreach (var gameObject in turnInfo.ApplicableGameObjects)
            {
                if (!_behaviorFinder.TryFind(gameObject, out Tuple<IHasReadOnlyEnchantmentsBehavior, IHasMutableStatsBehavior> behaviours))
                {
                    continue;
                }

                _statUpdater.Update(
                    behaviours.Item2.BaseStats,
                    behaviours.Item1.Enchantments,
                    behaviours.Item2.MutateStats,
                    turnInfo.ElapsedTurns);
            }
        }
    }
}