using ProjectXyz.Plugins.Features.Weather.Api;

namespace ProjectXyz.Plugins.Features.Weather
{
    public sealed class WeatherManager : IWeatherManager
    {
        public IWeatherTable WeatherTable { get; set; }

        public IWeather Weather { get; set; }
    }
}
