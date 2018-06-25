using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Weather
{
    public interface IWeather
    {
        IIdentifier WeatherDefinitionId { get; }
    }
}
