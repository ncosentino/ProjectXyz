using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.Weather.Api;

namespace ProjectXyz.Plugins.Features.Weather
{
    public sealed class WeatherFactory : IWeatherFactory
    {
        private readonly IWeatherBehaviorsInterceptorFacade _weatherBehaviorsInterceptorFacade;
        private readonly IWeatherBehaviorsProviderFacade _weatherBehaviorsProviderFacade;
        private readonly IGameObjectFactory _gameObjectFactory;

        public WeatherFactory(
            IWeatherBehaviorsInterceptorFacade weatherBehaviorsInterceptorFacade,
            IWeatherBehaviorsProviderFacade weatherBehaviorsProviderFacade,
            IGameObjectFactory gameObjectFactory)
        {
            _weatherBehaviorsInterceptorFacade = weatherBehaviorsInterceptorFacade;
            _weatherBehaviorsProviderFacade = weatherBehaviorsProviderFacade;
            _gameObjectFactory = gameObjectFactory;
        }

        public IGameObject Create(
            IIdentifier weatherId,
            IWeatherDuration weatherDuration,
            IEnumerable<IBehavior> additionalBehaviors)
        {
            var baseAndInjectedBehaviours = new IBehavior[]
                {
                    new IdentifierBehavior(weatherId),
                    weatherDuration,
                }
                .Concat(additionalBehaviors)
                .ToArray();

            var additionalBehaviorsFromProviders = _weatherBehaviorsProviderFacade
                .GetBehaviors(baseAndInjectedBehaviours);
            var allBehaviors = baseAndInjectedBehaviours
                .Concat(additionalBehaviorsFromProviders)
                .ToArray();
            _weatherBehaviorsInterceptorFacade.Intercept(allBehaviors);

            var weather = _gameObjectFactory.Create(allBehaviors);
            return weather;
        }
    }
}