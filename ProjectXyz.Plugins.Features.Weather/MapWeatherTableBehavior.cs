using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Weather.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.Weather
{
    public sealed class MapWeatherTableBehavior :
        BaseBehavior,
        IMapWeatherTableBehavior
    {
        public MapWeatherTableBehavior(IIdentifier weatherTableId)
        {
            WeatherTableId = weatherTableId;
        }

        public IIdentifier WeatherTableId { get; }
    }
}
