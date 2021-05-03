using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.Weather.Api;

namespace ProjectXyz.Plugins.Features.Weather
{
    public sealed class WeatherBehaviorsInterceptorFacade : IWeatherBehaviorsInterceptorFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableWeatherBehaviorsInterceptor> _interceptors;

        public WeatherBehaviorsInterceptorFacade(IEnumerable<IDiscoverableWeatherBehaviorsInterceptor> interceptors)
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