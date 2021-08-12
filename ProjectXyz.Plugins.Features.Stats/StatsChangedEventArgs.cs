using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Stats
{
    public sealed class StatsChangedEventArgs : EventArgs
    {
        public StatsChangedEventArgs(
            IEnumerable<KeyValuePair<IIdentifier, double>> addedStats,
            IEnumerable<IIdentifier> removedStats,
            IEnumerable<KeyValuePair<IIdentifier, Tuple<double, double>>> changedStats)
        {
            AddedStats = addedStats.ToDictionary(x => x.Key, x => x.Value);
            RemovedStats = removedStats.ToArray();
            ChangedStats = changedStats.ToDictionary(x => x.Key, x => x.Value);
        }

        public IReadOnlyDictionary<IIdentifier, double> AddedStats { get; }

        public IReadOnlyCollection<IIdentifier> RemovedStats { get; }

        public IReadOnlyDictionary<IIdentifier, Tuple<double, double>> ChangedStats { get; }
    }
}