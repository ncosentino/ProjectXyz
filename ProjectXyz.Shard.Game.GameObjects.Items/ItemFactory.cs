using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Items;
using ProjectXyz.Application.Stats.Core;
using ProjectXyz.Game.Interface.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors;

namespace ProjectXyz.Shared.Game.GameObjects.Items
{
    public sealed class ItemFactory : IItemFactory
    {
        private readonly IStatManagerFactory _statManagerFactory;
        private readonly IBehaviorManager _behaviorManager;

        public ItemFactory(
            IStatManagerFactory statManagerFactory,
            IBehaviorManager behaviorManager)
        {
            _statManagerFactory = statManagerFactory;
            _behaviorManager = behaviorManager;
        }

        public IGameObject Create(IEnumerable<IBehavior> behaviors)
        {
            // FIXME: this probably shouldn't even be here... causes dependency on a feature
            var mutableStatsProvider = new MutableStatsProvider();
            var statManager = _statManagerFactory.Create(mutableStatsProvider);
            var hasMutableStats = new HasMutableStatsBehavior(statManager);
            
            var item = new Item(
                _behaviorManager,
                hasMutableStats,
                behaviors);
            return item;
        }
    }
}