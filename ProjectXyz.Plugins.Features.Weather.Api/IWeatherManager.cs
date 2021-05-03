using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Weather.Api
{
    public interface IWeatherManager : IReadOnlyWeatherManager
    {
        new IWeatherTable WeatherTable { get; set; }

        new IGameObject Weather { get; set; }
    }
}