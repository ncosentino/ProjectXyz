using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Weather
{
    public sealed class WeatherManager : IWeatherManager
    {
        public IIdentifier WeatherId { get; set; }
    }
}
