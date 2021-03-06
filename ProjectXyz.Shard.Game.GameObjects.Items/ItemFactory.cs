﻿using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items
{
    public sealed class ItemFactory : IItemFactory
    {
        private readonly IStatManagerFactory _statManagerFactory;
        private readonly IBehaviorManager _behaviorManager;
        private readonly IMutableStatsProviderFactory _mutableStatsProviderFactory;

        public ItemFactory(
            IStatManagerFactory statManagerFactory,
            IBehaviorManager behaviorManager,
            IMutableStatsProviderFactory mutableStatsProviderFactory)
        {
            _statManagerFactory = statManagerFactory;
            _behaviorManager = behaviorManager;
            _mutableStatsProviderFactory = mutableStatsProviderFactory;
        }

        public IGameObject Create(IEnumerable<IBehavior> behaviors)
        {
            var mutableStatsProvider = _mutableStatsProviderFactory.Create();
            var statManager = _statManagerFactory.Create(mutableStatsProvider);

            // FIXME: this probably shouldn't even be here... causes dependency on a feature
            var hasMutableStats = new HasMutableStatsBehavior(statManager);

            var item = new Item(
                _behaviorManager,
                hasMutableStats,
                behaviors);
            return item;
        }
    }
}