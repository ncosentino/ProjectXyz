using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Weather.Api
{
    public interface IMapWeatherTableBehavior : IBehavior
    {
        IIdentifier WeatherTableId { get; }
    }
}