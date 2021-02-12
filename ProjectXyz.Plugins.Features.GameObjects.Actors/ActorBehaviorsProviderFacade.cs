using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorBehaviorsProviderFacade : IActorBehaviorsProviderFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableActorBehaviorsProvider> _providers;

        public ActorBehaviorsProviderFacade(IEnumerable<IDiscoverableActorBehaviorsProvider> providers)
        {
            _providers = providers.ToArray();
        }

        public IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors) =>
            _providers.SelectMany(x => x.GetBehaviors(baseBehaviors));
    }
}
