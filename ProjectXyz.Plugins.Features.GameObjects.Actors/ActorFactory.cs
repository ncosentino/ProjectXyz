using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorFactory : IActorFactory
    {
        private readonly IActorIdentifiers _actorIdentifiers;
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly IActorBehaviorsProviderFacade _actorBehaviorsProviderFacade;
        private readonly IActorBehaviorsInterceptorFacade _actorBehaviorsInterceptorFacade;
        private readonly IHasEnchantmentsBehaviorFactory _hasEnchantmentsBehaviorFactory;
        private readonly IHasStatsBehaviorFactory _hasMutableStatsBehaviorFactory;

        public ActorFactory(
            IActorIdentifiers actorIdentifiers,
            IGameObjectFactory gameObjectFactory,
            IActorBehaviorsProviderFacade actorBehaviorsProviderFacade,
            IActorBehaviorsInterceptorFacade actorBehaviorsInterceptorFacade,
            IHasEnchantmentsBehaviorFactory hasEnchantmentsBehaviorFactory,
            IHasStatsBehaviorFactory hasMutableStatsBehaviorFactory)
        {
            _actorIdentifiers = actorIdentifiers;
            _gameObjectFactory = gameObjectFactory;
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

            var actor = _gameObjectFactory.Create(allBehaviors);
            return actor;
        }
    }
}