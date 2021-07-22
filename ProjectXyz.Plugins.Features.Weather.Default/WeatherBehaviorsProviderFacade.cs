using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.Weather.Default
{
    public sealed class WeatherBehaviorsProviderFacade : IWeatherBehaviorsProviderFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableWeatherBehaviorsProvider> _providers;

        public WeatherBehaviorsProviderFacade(IEnumerable<IDiscoverableWeatherBehaviorsProvider> providers)
        {
            _providers = providers.ToArray();
        }

        public IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors) =>
            _providers.SelectMany(x => x.GetBehaviors(baseBehaviors));
    }
}