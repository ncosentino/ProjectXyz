using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.Weather.Api
{
    public interface IMapWeatherTableBehavior : IBehavior
    {
        IIdentifier WeatherTableId { get; }
    }
}