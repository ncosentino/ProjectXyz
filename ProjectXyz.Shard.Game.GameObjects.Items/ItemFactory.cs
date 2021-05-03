using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items
{
    public sealed class ItemFactory : IItemFactory
    {
        private readonly IStatManagerFactory _statManagerFactory;
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly IMutableStatsProviderFactory _mutableStatsProviderFactory;

        public ItemFactory(
            IStatManagerFactory statManagerFactory,
            IGameObjectFactory gameObjectFactory,
            IMutableStatsProviderFactory mutableStatsProviderFactory)
        {
            _statManagerFactory = statManagerFactory;
            _gameObjectFactory = gameObjectFactory;
            _mutableStatsProviderFactory = mutableStatsProviderFactory;
        }

        public IGameObject Create(IEnumerable<IBehavior> behaviors)
        {
            var mutableStatsProvider = _mutableStatsProviderFactory.Create();
            var statManager = _statManagerFactory.Create(mutableStatsProvider);

            // FIXME: this probably shouldn't even be here... causes dependency on a feature
            var hasMutableStats = new HasMutableStatsBehavior(statManager);

            var idBehavior = new IdentifierBehavior(new StringIdentifier(Guid.NewGuid().ToString()));

            var item = _gameObjectFactory.Create(behaviors
                .AppendSingle(idBehavior)
                .AppendSingle(hasMutableStats));
            return item;
        }
    }
}