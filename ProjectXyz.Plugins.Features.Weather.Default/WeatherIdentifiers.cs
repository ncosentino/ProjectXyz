using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.Weather.Default
{
    public sealed class WeatherIdentifiers : IWeatherIdentifiers
    {
        public IIdentifier FilterContextWeatherIdentifier { get; } = new StringIdentifier("weather");

        public IIdentifier WeatherIdentifier { get; } = new StringIdentifier("id");

        public IIdentifier WeatherStateTypeId { get; } = new StringIdentifier("weather");

        public IIdentifier KindOfWeatherStateId { get; } = new StringIdentifier("Kind Of Weather");
    }
}