namespace ProjectXyz.Plugins.Features.Weather.Api
{
    public interface IReadOnlyWeatherManager
    {
        IWeatherTable WeatherTable { get; }

        IWeather Weather { get; }
    }
}