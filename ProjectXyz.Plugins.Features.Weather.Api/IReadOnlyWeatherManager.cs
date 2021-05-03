using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Weather.Api
{
    public interface IReadOnlyWeatherManager
    {
        IWeatherTable WeatherTable { get; }

        IGameObject Weather { get; }
    }
}