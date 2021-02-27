using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Weather.Api;

namespace ProjectXyz.Plugins.Features.Weather
{
    public sealed class WeightedWeatherTableEntry : IWeightedWeatherTableEntry
    {
        public WeightedWeatherTableEntry(
            IIdentifier weatherId,
            double weight,
            IInterval minimumDuration,
            IInterval maximumDuration)
        {
            WeatherId = weatherId;
            Weight = weight;
            MinimumDuration = minimumDuration;
            MaximumDuration = maximumDuration;
        }

        public IIdentifier WeatherId { get; }

        public double Weight { get; }

        public IInterval MinimumDuration { get; }

        public IInterval MaximumDuration { get; }
    }
}