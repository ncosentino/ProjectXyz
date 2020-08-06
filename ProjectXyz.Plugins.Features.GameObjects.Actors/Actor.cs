using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors
{
    public sealed class Actor : IGameObject
    {
        public Actor(
            IIdentifierBehavior identifierBehavior,
            IBehaviorCollectionFactory behaviorCollectionFactory,
            IBehaviorManager behaviorManager,
            IHasEnchantmentsBehavior hasEnchantmentsBehavior,
            IBuffableBehavior buffableBehavior,
            IHasMutableStatsBehavior hasStatsBehavior,
            IEnumerable<IAdditionalActorBehaviorsProvider> additionalActorBehaviorsProviders,
            IEnumerable<IActorBehaviorsInterceptor> actorBehaviorsInterceptors)
        {
            var additionalBehaviors = additionalActorBehaviorsProviders.SelectMany(x => x.GetBehaviors(this));
            var behaviors = new IBehavior[]
                {
                    identifierBehavior,
                    hasEnchantmentsBehavior,
                    buffableBehavior,
                    hasStatsBehavior,
                }
                .Concat(additionalBehaviors)
                .ToArray();
            foreach (var behaviorInterceptor in actorBehaviorsInterceptors)
            {
                behaviorInterceptor.Intercept(behaviors);
            }

            Behaviors = behaviorCollectionFactory.Create(behaviors);
            behaviorManager.Register(this, Behaviors);
        }

        public delegate Actor Factory(
            IIdentifierBehavior identifierBehavior,
            IBehaviorManager behaviorManager,
            IHasEnchantmentsBehavior hasEnchantmentsBehavior,
            IBuffableBehavior buffableBehavior,
            IHasMutableStatsBehavior hasStatsBehavior);

        public IBehaviorCollection Behaviors { get; }
    }
}