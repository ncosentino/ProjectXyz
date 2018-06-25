using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Weather
{
    public interface IWeatherManager
    {
        bool WeatherGroupingContainsWeatherDefinition(
            IIdentifier weatherGroupingId,
            IIdentifier weatherDefinitionid);
    }
}