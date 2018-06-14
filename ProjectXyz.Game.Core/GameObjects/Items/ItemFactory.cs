using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Items;
using ProjectXyz.Application.Stats.Core;
using ProjectXyz.Game.Core.Behaviors;
using ProjectXyz.Game.Interface.Behaviors;
using ProjectXyz.Game.Interface.Enchantments;
using ProjectXyz.Game.Interface.Stats;

namespace ProjectXyz.Game.Core.GameObjects.Items
{
    public sealed class ItemFactory : IItemFactory
    {
        private readonly IStatManagerFactory _statManagerFactory;
        private readonly IActiveEnchantmentManagerFactory _activeEnchantmentManagerFactory;
        private readonly IBehaviorManager _behaviorManager;

        public ItemFactory(
            IStatManagerFactory statManagerFactory,
            IActiveEnchantmentManagerFactory activeEnchantmentManagerFactory,
            IBehaviorManager behaviorManager)
        {
            _statManagerFactory = statManagerFactory;
            _activeEnchantmentManagerFactory = activeEnchantmentManagerFactory;
            _behaviorManager = behaviorManager;
        }

        public IGameObject Create()
        {
            var mutableStatsProvider = new MutableStatsProvider();
            var statManager = _statManagerFactory.Create(mutableStatsProvider);
            var hasMutableStats = new HasMutableStatsBehavior(statManager);

            var activeEnchantmentManager = _activeEnchantmentManagerFactory.Create();
            var hasEnchantments = new HasEnchantmentsBehavior(activeEnchantmentManager);
            var buffable = new BuffableBehavior(activeEnchantmentManager);
            var canBeEquipped = new CanBeEquippedBehavior();
            var item = new Item(
                _behaviorManager,
                hasEnchantments,
                buffable,
                hasMutableStats,
                canBeEquipped);
            return item;
        }
    }
}