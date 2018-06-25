using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.StateEnchantments.Api;
using ProjectXyz.Plugins.Features.StateEnchantments.Shared;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.Weather
{
    public sealed class StateIdToTermRepo : IStateIdToTermRepository
    {
        public IEnumerable<IStateIdToTermMapping> GetStateIdToTermMappings()
        {
            yield return new StateIdToTermMapping(
                new StringIdentifier("Weather"),
                new TermMapping(
                    new[]
                    {
                        new KeyValuePair<IIdentifier, string>(
                            WeatherIds.Rain,
                            "weather_rain"),
                        new KeyValuePair<IIdentifier, string>(
                            WeatherIds.Snow,
                            "weather_snow"),
                        new KeyValuePair<IIdentifier, string>(
                            WeatherIds.Sunny,
                            "weather_sunny"),
                        new KeyValuePair<IIdentifier, string>(
                            WeatherIds.Overcast,
                            "weather_overcast"),
                    }));
        }
    }
}