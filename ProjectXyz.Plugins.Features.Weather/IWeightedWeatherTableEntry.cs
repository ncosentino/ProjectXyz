using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Weather
{
    public interface IWeightedWeatherTableEntry
    {
        double Weight { get; }

        IIdentifier WeatherId { get; }

        double MinimumDurationInTurns { get; }

        double MaximumDurationInTurns { get; }
    }
}