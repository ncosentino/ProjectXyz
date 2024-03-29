﻿using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Weather.Default
{
    public sealed class WeatherTable : IWeatherTable
    {
        public WeatherTable(
            IIdentifier weatherTableId,
            IEnumerable<IWeightedWeatherTableEntry> weightedEntries,
            IEnumerable<IFilterAttribute> supportedAttributes)
        {
            WeatherTableId = weatherTableId;
            WeightedEntries = weightedEntries.ToArray();
            SupportedAttributes = supportedAttributes;
        }

        public IIdentifier WeatherTableId { get; }

        public IReadOnlyCollection<IWeightedWeatherTableEntry> WeightedEntries { get; }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }
    }
}