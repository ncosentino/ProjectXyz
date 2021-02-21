using System.Collections.Generic;
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
        private readonly IBehaviorManager _behaviorManager;
        private readonly IActorBehaviorsProviderFacade _actorBehaviorsProviderFacade;
        private readonly IActorBehaviorsInterceptorFacade _actorBehaviorsInterceptorFacade;
        private readonly IMutableStatsProviderFactory _mutableStatsProviderFactory;
        private readonly IHasEnchantmentsBehaviorFactory _hasEnchantmentsBehaviorFactory;

        public ActorFactory(
            IBehaviorManager behaviorManager,
            IActorBehaviorsProviderFacade actorBehaviorsProviderFacade,
            IActorBehaviorsInterceptorFacade actorBehaviorsInterceptorFacade,
            IStatManagerFactory statManagerFactory,
            IMutableStatsProviderFactory mutableStatsProviderFactory,
            IHasEnchantmentsBehaviorFactory hasEnchantmentsBehaviorFactory)
        {
            _statManagerFactory = statManagerFactory;
            _behaviorManager = behaviorManager;
            _actorBehaviorsProviderFacade = actorBehaviorsProviderFacade;
            _actorBehaviorsInterceptorFacade = actorBehaviorsInterceptorFacade;
            _mutableStatsProviderFactory = mutableStatsProviderFactory;
            _hasEnchantmentsBehaviorFactory = hasEnchantmentsBehaviorFactory;
        }

        public IGameObject Create(
            IReadOnlyTypeIdentifierBehavior typeIdentifierBehavior,
            IReadOnlyTemplateIdentifierBehavior templateIdentifierBehavior,
            IReadOnlyIdentifierBehavior identifierBehavior,
            IEnumerable<IBehavior> additionalbehaviors)
        {
            var mutableStatsProvider = _mutableStatsProviderFactory.Create();
            var statManager = _statManagerFactory.Create(mutableStatsProvider);
            var hasMutableStats = new HasMutableStatsBehavior(statManager);
            var hasEnchantments = _hasEnchantmentsBehaviorFactory.Create();

            var baseAndInjectedBehaviours = new IBehavior[]
                {
                    typeIdentifierBehavior,
                    templateIdentifierBehavior,
                    identifierBehavior,
                    hasEnchantments,
                    hasMutableStats,
                }
                .Concat(additionalbehaviors)
                .ToArray();
            var additionalBehaviorsFromProviders = _actorBehaviorsProviderFacade
                .GetBehaviors(baseAndInjectedBehaviours);
            var allBehaviors = baseAndInjectedBehaviours
                .Concat(additionalBehaviorsFromProviders)
                .ToArray();
            _actorBehaviorsInterceptorFacade.Intercept(allBehaviors);

            var actor = new Actor(allBehaviors);
            _behaviorManager.Register(actor, allBehaviors);
            return actor;
        }
    }
}