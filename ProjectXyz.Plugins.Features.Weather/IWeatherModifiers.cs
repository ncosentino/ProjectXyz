using System.Collections.Generic;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Weather
{
    public interface IWeatherModifiers
    {
        IReadOnlyDictionary<IIdentifier, double> GetWeights(IReadOnlyDictionary<IIdentifier, double> weatherWeights);

        double GetMinimumDuration(
            IIdentifier weatherId,
            double baseMinimumDuration,
            double maximumDuration);

        double GetMaximumDuration(
            IIdentifier weatherId,
            double baseMaximumDuration);
    }
}