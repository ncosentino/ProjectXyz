using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.Weather.Default
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
