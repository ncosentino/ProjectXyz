using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Weather
{
    public sealed class WeatherManager : IWeatherManager
    {
        public WeatherManager()
        {
            // FIXME: hardcoding a weather here seems wrong, but allowing a 
            // null weather ID also seems wrong?
            WeatherId = WeatherIds.Clear;
        }

        public IIdentifier WeatherId { get; set; }
    }
}
