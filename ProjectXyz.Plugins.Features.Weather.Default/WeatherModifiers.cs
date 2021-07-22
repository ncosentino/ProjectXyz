using System.Collections.Generic;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Weather.Default
{
    public sealed class WeatherModifiers : IWeatherModifiers
    {
        public double GetMaximumDuration(IIdentifier weatherId, double baseMaximumDuration)
        {
            return baseMaximumDuration;
        }

        public double GetMinimumDuration(IIdentifier weatherId, double baseMinimumDuration, double maximumDuration)
        {
            return baseMinimumDuration;
        }

        public IReadOnlyDictionary<IIdentifier, double> GetWeights(IReadOnlyDictionary<IIdentifier, double> weatherWeights)
        {
            return weatherWeights;
        }
    }
}