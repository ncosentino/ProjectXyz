using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class MapFactory : IMapFactory
    {
        private readonly IBehaviorManager _behaviorManager;
        private readonly IMapBehaviorsProviderFacade _mapBehaviorsProviderFacade;
        private readonly IMapBehaviorsInterceptorFacade _mapBehaviorsInterceptorFacade;

        public MapFactory(
            IBehaviorManager behaviorManager,
            IMapBehaviorsProviderFacade mapBehaviorsProviderFacade,
            IMapBehaviorsInterceptorFacade mapBehaviorsInterceptorFacade)
        {
            _behaviorManager = behaviorManager;
            _mapBehaviorsProviderFacade = mapBehaviorsProviderFacade;
            _mapBehaviorsInterceptorFacade = mapBehaviorsInterceptorFacade;
        }

        public IMap Create(
            IIdentifier id,
            IEnumerable<IMapLayer> layers,
            IEnumerable<IBehavior> additionalBehaviors)
        {
            var baseAndInjectedBehaviours = new IBehavior[]
                {
                    new IdentifierBehavior(id),
                }
                .Concat(additionalBehaviors)
                .ToArray();
            var additionalBehaviorsFromProviders = _mapBehaviorsProviderFacade
                .GetBehaviors(baseAndInjectedBehaviours);
            var allBehaviors = baseAndInjectedBehaviours
                .Concat(additionalBehaviorsFromProviders)
                .ToArray();
            allBehaviors = _mapBehaviorsInterceptorFacade
                .Intercept(allBehaviors)
                .ToArray();

            var map = new Map(
                id,
                layers,
                allBehaviors);
            _behaviorManager.Register(map, map.Behaviors);
            return map;
        }
    }
}