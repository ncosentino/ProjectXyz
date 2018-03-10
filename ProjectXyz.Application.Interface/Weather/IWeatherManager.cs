using ProjectXyz.Api.Framework;

namespace ProjectXyz.Application.Interface.Weather
{
    public interface IWeatherManager
    {
        bool WeatherGroupingContainsWeatherDefinition(
            IIdentifier weatherGroupingId,
            IIdentifier weatherDefinitionid);
    }
}