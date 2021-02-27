using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Weather.Api
{
    public interface IWeatherTable : IHasFilterAttributes
    {
        IIdentifier WeatherTableId { get; }

        IReadOnlyCollection<IWeightedWeatherTableEntry> WeightedEntries { get; }
    }
}