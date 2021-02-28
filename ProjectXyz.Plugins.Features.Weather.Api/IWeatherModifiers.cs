using System.Collections.Generic;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Weather.Api
{
    public interface IWeatherModifiers
    {
        IReadOnlyDictionary<IIdentifier, double> GetWeights(IReadOnlyDictionary<IIdentifier, double> weatherWeights);

        double GetMinimumDuration(
            IIdentifier weatherId,
            double baseMinimumDuration);

        double GetMaximumDuration(
            IIdentifier weatherId,
            double baseMaximumDuration);
    }
}