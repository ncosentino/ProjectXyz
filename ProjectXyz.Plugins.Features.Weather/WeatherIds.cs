using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.Weather
{
    public static class WeatherIds
    {
        public static IIdentifier Rain { get; } = new StringIdentifier("rain");

        public static IIdentifier Snow { get; } = new StringIdentifier("snow");

        public static IIdentifier Sunny { get; } = new StringIdentifier("sunny");

        public static IIdentifier Overcast { get; } = new StringIdentifier("overcast");
    }
}