using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Weather
{
    public interface IWeather
    {
        IIdentifier WeatherDefinitionId { get; }
    }
}
