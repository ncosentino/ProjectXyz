using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.Weather.Api;

namespace ProjectXyz.Plugins.Features.Weather
{
    public sealed class WeatherFactory : IWeatherFactory
    {
        private readonly IWeatherBehaviorsInterceptorFacade _weatherBehaviorsInterceptorFacade;
        private readonly IWeatherBehaviorsProviderFacade _weatherBehaviorsProviderFacade;
        private readonly IBehaviorManager _behaviorManager;

        public WeatherFactory(
            IWeatherBehaviorsInterceptorFacade weatherBehaviorsInterceptorFacade,
            IWeatherBehaviorsProviderFacade weatherBehaviorsProviderFacade,
            IBehaviorManager behaviorManager)
        {
            _weatherBehaviorsInterceptorFacade = weatherBehaviorsInterceptorFacade;
            _weatherBehaviorsProviderFacade = weatherBehaviorsProviderFacade;
            _behaviorManager = behaviorManager;
        }

        public IWeather Create(
            IIdentifier weatherId,
            double durationInTurns,
            IInterval transitionInDuration,
            IInterval transitionOutDuration,
            IEnumerable<IBehavior> additionalBehaviors)
        {
            var baseAndInjectedBehaviours = new IBehavior[]
                {
                    new IdentifierBehavior(weatherId)
                }
                .Concat(additionalBehaviors)
                .ToArray();

            var additionalBehaviorsFromProviders = _weatherBehaviorsProviderFacade
                .GetBehaviors(baseAndInjectedBehaviours);
            var allBehaviors = baseAndInjectedBehaviours
                .Concat(additionalBehaviorsFromProviders)
                .ToArray();
            _weatherBehaviorsInterceptorFacade.Intercept(allBehaviors);

            var weather = new Weather(
                durationInTurns,
                transitionInDuration,
                transitionOutDuration,
                allBehaviors);
            _behaviorManager.Register(weather, allBehaviors);            
            return weather;
        }
    }
}