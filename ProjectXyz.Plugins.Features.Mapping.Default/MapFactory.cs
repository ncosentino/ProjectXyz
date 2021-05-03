using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class MapFactory : IMapFactory
    {
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly IMapBehaviorsProviderFacade _mapBehaviorsProviderFacade;
        private readonly IMapBehaviorsInterceptorFacade _mapBehaviorsInterceptorFacade;

        public MapFactory(
            IGameObjectFactory gameObjectFactory,
            IMapBehaviorsProviderFacade mapBehaviorsProviderFacade,
            IMapBehaviorsInterceptorFacade mapBehaviorsInterceptorFacade)
        {
            _gameObjectFactory = gameObjectFactory;
            _mapBehaviorsProviderFacade = mapBehaviorsProviderFacade;
            _mapBehaviorsInterceptorFacade = mapBehaviorsInterceptorFacade;
        }

        public IGameObject Create(
            IIdentifier id,
            IEnumerable<IMapLayer> layers,
            IEnumerable<IBehavior> additionalBehaviors)
        {
            var baseAndInjectedBehaviours = new IBehavior[]
                {
                    new IdentifierBehavior(id),
                    new MapLayersBehavior(layers),
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

            var map = _gameObjectFactory.Create(allBehaviors);
            return map;
        }
    }
}