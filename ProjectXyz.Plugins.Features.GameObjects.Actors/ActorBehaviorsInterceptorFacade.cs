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
            _interceptors = interceptors.ToArray();
        }

        public void Intercept(IReadOnlyCollection<IBehavior> behaviors)
        {
            foreach (var interceptor in _interceptors)
            {
                interceptor.Intercept(behaviors);
            }
        }
    }
}
