namespace ProjectXyz.Plugins.Features.Weather.Api
{
    public interface IWeatherManager : IReadOnlyWeatherManager
    {
        new IWeatherTable WeatherTable { get; set; }

        new IWeather Weather { get; set; }
    }
}