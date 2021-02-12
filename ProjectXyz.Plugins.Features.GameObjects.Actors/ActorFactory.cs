using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorFactory : IActorFactory
    {
        private readonly IStatManagerFactory _statManagerFactory;
        private readonly IActiveEnchantmentManagerFactory _activeEnchantmentManagerFactory;
        private readonly IBehaviorCollectionFactory _behaviorCollectionFactory;
        private readonly IBehaviorManager _behaviorManager;
        private readonly IActorBehaviorsProviderFacade _actorBehaviorsProviderFacade;
        private readonly IActorBehaviorsInterceptorFacade _actorBehaviorsInterceptorFacade;
        private readonly IMutableStatsProviderFactory _mutableStatsProviderFactory;

        public ActorFactory(
            IBehaviorCollectionFactory behaviorCollectionFactory,
            IBehaviorManager behaviorManager,
            IActorBehaviorsProviderFacade actorBehaviorsProviderFacade,
            IActorBehaviorsInterceptorFacade actorBehaviorsInterceptorFacade,
            IStatManagerFactory statManagerFactory,
            IActiveEnchantmentManagerFactory activeEnchantmentManagerFactory,
            IMutableStatsProviderFactory mutableStatsProviderFactory)
        {
            _statManagerFactory = statManagerFactory;
            _activeEnchantmentManagerFactory = activeEnchantmentManagerFactory;
            _behaviorCollectionFactory = behaviorCollectionFactory;
            _behaviorManager = behaviorManager;
            _actorBehaviorsProviderFacade = actorBehaviorsProviderFacade;
            _actorBehaviorsInterceptorFacade = actorBehaviorsInterceptorFacade;
            _mutableStatsProviderFactory = mutableStatsProviderFactory;
        }

        public IGameObject Create(
            IReadOnlyTypeIdentifierBehavior typeIdentifierBehavior,
            IReadOnlyTemplateIdentifierBehavior templateIdentifierBehavior,
            IReadOnlyIdentifierBehavior identifierBehavior)
        {
            var mutableStatsProvider = _mutableStatsProviderFactory.Create();
            var statManager = _statManagerFactory.Create(mutableStatsProvider);
            var hasMutableStats = new HasMutableStatsBehavior(statManager);

            var activeEnchantmentManager = _activeEnchantmentManagerFactory.Create();
            var hasEnchantments = new HasEnchantmentsBehavior(activeEnchantmentManager);
            var buffable = new BuffableBehavior(activeEnchantmentManager);

            var baseBehaviours = new IBehavior[]
            {
                typeIdentifierBehavior,
                templateIdentifierBehavior,
                identifierBehavior,
                hasEnchantments,
                buffable,
                hasMutableStats,
            };
            var additionalBehaviors = _actorBehaviorsProviderFacade.GetBehaviors(baseBehaviours);
            var allBehaviors = _behaviorCollectionFactory
                .Create(baseBehaviours
                .Concat(additionalBehaviors));
            _actorBehaviorsInterceptorFacade.Intercept(allBehaviors);

            var actor = new Actor(allBehaviors);
            _behaviorManager.Register(actor, allBehaviors);
            return actor;
        }
    }
}