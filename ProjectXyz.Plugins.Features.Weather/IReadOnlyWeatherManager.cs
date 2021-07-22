using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Weather
{
    public interface IReadOnlyWeatherManager
    {
        IWeatherTable WeatherTable { get; }

        IGameObject Weather { get; }
    }
}