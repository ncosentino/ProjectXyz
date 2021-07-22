using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Weather
{
    public interface IWeatherIdentifiers
    {
        IIdentifier FilterContextWeatherIdentifier { get; }

        IIdentifier WeatherIdentifier { get; }

        IIdentifier WeatherStateTypeId { get; }

        IIdentifier KindOfWeatherStateId { get; }
    }
}