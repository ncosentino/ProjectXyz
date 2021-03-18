using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorBehaviorsInterceptorFacade : IActorBehaviorsInterceptorFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableActorBehaviorsInterceptor> _interceptors;

        public ActorBehaviorsInterceptorFacade(IEnumerable<IDiscoverableActorBehaviorsInterceptor> interceptors)
        {
            _interceptors = interceptors
                .OrderBy(x => x.Priority)
                .ToArray();
        }

        public IEnumerable<IBehavior> Intercept(IReadOnlyCollection<IBehavior> behaviors)
        {
            IReadOnlyCollection<IBehavior> currentBehaviors = new List<IBehavior>(behaviors);
            foreach (var interceptor in _interceptors)
            {
                currentBehaviors = interceptor
                    .Intercept(currentBehaviors)
                    .ToArray();
            }

            return currentBehaviors;
        }
    }
}
