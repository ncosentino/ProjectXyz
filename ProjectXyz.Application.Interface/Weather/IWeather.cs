using ProjectXyz.Api.Framework;

namespace ProjectXyz.Application.Interface.Weather
{
    public interface IWeather
    {
        IIdentifier WeatherDefinitionId { get; }
    }
}
