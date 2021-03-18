using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorFactory : IActorFactory
    {
        private readonly IActorIdentifiers _actorIdentifiers;
        private readonly IBehaviorManager _behaviorManager;
        private readonly IActorBehaviorsProviderFacade _actorBehaviorsProviderFacade;
        private readonly IActorBehaviorsInterceptorFacade _actorBehaviorsInterceptorFacade;
        private readonly IHasEnchantmentsBehaviorFactory _hasEnchantmentsBehaviorFactory;
        private readonly IHasMutableStatsBehaviorFactory _hasMutableStatsBehaviorFactory;

        public ActorFactory(
            IActorIdentifiers actorIdentifiers,
            IBehaviorManager behaviorManager,
            IActorBehaviorsProviderFacade actorBehaviorsProviderFacade,
            IActorBehaviorsInterceptorFacade actorBehaviorsInterceptorFacade,
            IHasEnchantmentsBehaviorFactory hasEnchantmentsBehaviorFactory,
            IHasMutableStatsBehaviorFactory hasMutableStatsBehaviorFactory)
        {
            _actorIdentifiers = actorIdentifiers;
            _behaviorManager = behaviorManager;
            _actorBehaviorsProviderFacade = actorBehaviorsProviderFacade;
            _actorBehaviorsInterceptorFacade = actorBehaviorsInterceptorFacade;
            _hasEnchantmentsBehaviorFactory = hasEnchantmentsBehaviorFactory;
            _hasMutableStatsBehaviorFactory = hasMutableStatsBehaviorFactory;
        }

        public IGameObject Create(
            IReadOnlyIdentifierBehavior identifierBehavior,
            IEnumerable<IBehavior> additionalBehaviors)
        {
            var hasMutableStats = _hasMutableStatsBehaviorFactory.Create();
            var hasEnchantments = _hasEnchantmentsBehaviorFactory.Create();

            var baseAndInjectedBehaviours = new IBehavior[]
                {
                    new TypeIdentifierBehavior(_actorIdentifiers.ActorTypeIdentifier),
                    identifierBehavior,
                    hasEnchantments,
                    hasMutableStats,
                }
                .Concat(additionalBehaviors)
                .ToArray();
            var additionalBehaviorsFromProviders = _actorBehaviorsProviderFacade
                .GetBehaviors(baseAndInjectedBehaviours);
            var allBehaviors = baseAndInjectedBehaviours
                .Concat(additionalBehaviorsFromProviders)
                .ToArray();
            allBehaviors = _actorBehaviorsInterceptorFacade
                .Intercept(allBehaviors)
                .ToArray();

            var actor = new Actor(allBehaviors);
            _behaviorManager.Register(actor, allBehaviors);
            return actor;
        }
    }
}