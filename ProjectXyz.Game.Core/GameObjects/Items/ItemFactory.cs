using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Items;
using ProjectXyz.Application.Stats.Core;
using ProjectXyz.Game.Core.Behaviors;
using ProjectXyz.Game.Interface.Behaviors;
using ProjectXyz.Game.Interface.Stats;

namespace ProjectXyz.Game.Core.GameObjects.Items
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