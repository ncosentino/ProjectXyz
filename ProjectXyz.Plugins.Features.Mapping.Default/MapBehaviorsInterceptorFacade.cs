using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class MapBehaviorsInterceptorFacade : IMapBehaviorsInterceptorFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableMapBehaviorsInterceptor> _interceptors;

        public MapBehaviorsInterceptorFacade(IEnumerable<IDiscoverableMapBehaviorsInterceptor> interceptors)
        {
            _interceptors = interceptors.ToArray();
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