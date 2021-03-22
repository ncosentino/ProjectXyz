using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Weather.Api;

namespace ProjectXyz.Plugins.Features.Weather
{
    public sealed class WeightedWeatherTableEntry : IWeightedWeatherTableEntry
    {
        public WeightedWeatherTableEntry(
            IIdentifier weatherId,
            double weight,
            double minimumDurationInTurns,
            double maximumDurationInTurns)
        {
            WeatherId = weatherId;
            Weight = weight;
            MinimumDurationInTurns = minimumDurationInTurns;
            MaximumDurationInTurns = maximumDurationInTurns;
        }

        public IIdentifier WeatherId { get; }

        public double Weight { get; }

        public double MinimumDurationInTurns { get; }

        public double MaximumDurationInTurns { get; }
    }
}