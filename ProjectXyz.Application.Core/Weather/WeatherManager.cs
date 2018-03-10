using ProjectXyz.Api.Framework;
using ProjectXyz.Application.Interface.Weather;

namespace ProjectXyz.Application.Core.Weather
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
