using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Weather
{
    public sealed class WeatherManager : IWeatherManager
    {
        public bool WeatherGroupingContainsWeatherDefinition(
            IIdentifier weatherGroupingId,
            IIdentifier weatherDefinitionId)
        {
            return true;
        }
    }
}
