
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Weather.Api
{
    public interface IWeatherIdentifiers
    {
        IIdentifier FilterContextWeatherIdentifier { get; }

        IIdentifier WeatherIdentifier { get; }
    }
}