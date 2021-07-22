using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Weather
{
    public interface IWeatherTable : IHasFilterAttributes
    {
        IIdentifier WeatherTableId { get; }

        IReadOnlyCollection<IWeightedWeatherTableEntry> WeightedEntries { get; }
    }
}