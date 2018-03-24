using ProjectXyz.Application.Stats.Core;
using ProjectXyz.Game.Core.Behaviors;
using ProjectXyz.Game.Interface.Behaviors;
using ProjectXyz.Game.Interface.Enchantments;
using ProjectXyz.Game.Interface.GameObjects;
using ProjectXyz.Game.Interface.GameObjects.Actors;
using ProjectXyz.Game.Interface.Stats;

namespace ProjectXyz.Game.Core.GameObjects.Actors
{
    public sealed class ActorFactory : IActorFactory
    {
        private readonly IStatManagerFactory _statManagerFactory;
        private readonly IActiveEnchantmentManagerFactory _activeEnchantmentManagerFactory;
        private readonly IBehaviorManager _behaviorManager;

        public ActorFactory(
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
            var canEquip = new CanEquipBehavior();
            var applyEquipmentEnchantmentsBehavior = new ApplyEquipmentEnchantmentsBehavior();
            var actor = new Actor(
                _behaviorManager,
                hasEnchantments,
                buffable,
                hasMutableStats,
                canEquip,
                applyEquipmentEnchantmentsBehavior);
            return actor;
        }
    }
}