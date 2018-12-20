using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Application.Stats.Core;
using ProjectXyz.Game.Interface.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Shared.Framework;

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
            var identifierBehavior = new IdentifierBehavior();

            var mutableStatsProvider = new MutableStatsProvider();
            var statManager = _statManagerFactory.Create(mutableStatsProvider);
            var hasMutableStats = new HasMutableStatsBehavior(statManager);

            var activeEnchantmentManager = _activeEnchantmentManagerFactory.Create();
            var hasEnchantments = new HasEnchantmentsBehavior(activeEnchantmentManager);
            var buffable = new BuffableBehavior(activeEnchantmentManager);
            
            // TODO: where should these come from? not here...
            var canEquip = new CanEquipBehavior(new[]
            {
                new StringIdentifier("head"),
                new StringIdentifier("body"),
                new StringIdentifier("left hand"),
                new StringIdentifier("right hand"),
                new StringIdentifier("amulet"),
                new StringIdentifier("ring1"),
                new StringIdentifier("ring2"),
                new StringIdentifier("shoulders"),
                new StringIdentifier("hands"),
                new StringIdentifier("waist"),
                new StringIdentifier("feet"),
                new StringIdentifier("legs"),
                new StringIdentifier("back"),
            });

            var applyEquipmentEnchantmentsBehavior = new ApplyEquipmentEnchantmentsBehavior();
            var actor = new Actor(
                identifierBehavior,
                _behaviorCollectionFactory,
                _behaviorManager,
                hasEnchantments,
                buffable,
                hasMutableStats,
                canEquip,
                applyEquipmentEnchantmentsBehavior,
                _additionalActorBehaviorsProviders);
            return actor;
        }
    }
}