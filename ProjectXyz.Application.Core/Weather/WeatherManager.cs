using ProjectXyz.Application.Interface.Weather;
using ProjectXyz.Framework.Interface;

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
