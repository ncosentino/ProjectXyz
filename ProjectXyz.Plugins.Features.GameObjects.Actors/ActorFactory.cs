using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Application.Stats.Core;
using ProjectXyz.Game.Core.Behaviors;
using ProjectXyz.Game.Interface.Behaviors;
using ProjectXyz.Game.Interface.Enchantments;
using ProjectXyz.Game.Interface.GameObjects;
using ProjectXyz.Game.Interface.Stats;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorFactory : IActorFactory
    {
        private readonly IBehaviorCollectionFactory _behaviorCollectionFactory;
        private readonly IStatManagerFactory _statManagerFactory;
        private readonly IActiveEnchantmentManagerFactory _activeEnchantmentManagerFactory;
        private readonly IBehaviorManager _behaviorManager;
        private readonly IReadOnlyCollection<IAdditionalActorBehaviorsProvider> _additionalActorBehaviorsProviders;

        public ActorFactory(
            IBehaviorCollectionFactory behaviorCollectionFactory,
            IStatManagerFactory statManagerFactory,
            IActiveEnchantmentManagerFactory activeEnchantmentManagerFactory,
            IBehaviorManager behaviorManager,
            IEnumerable<IAdditionalActorBehaviorsProvider> additionalActorBehaviorsProviders)
        {
            _behaviorCollectionFactory = behaviorCollectionFactory;
            _statManagerFactory = statManagerFactory;
            _activeEnchantmentManagerFactory = activeEnchantmentManagerFactory;
            _behaviorManager = behaviorManager;
            _additionalActorBehaviorsProviders = additionalActorBehaviorsProviders.ToArray();
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
                _behaviorCollectionFactory,
                _behaviorManager,
                hasEnchantments,
                buffable,
                hasMutableStats,
                canEquip,
                applyEquipmentEnchantmentsBehavior,
                _additionalActorBehaviorsProviders.SelectMany(x => x.GetBehaviors()));
            return actor;
        }
    }
}