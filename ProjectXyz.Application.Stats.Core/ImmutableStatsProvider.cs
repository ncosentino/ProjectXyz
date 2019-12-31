using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.Stats;

namespace ProjectXyz.Plugins.Stats
{
    public sealed class ImmutableStatsProvider : IStatsProvider
    {
        public ImmutableStatsProvider(IEnumerable<KeyValuePair<IIdentifier, double>> stats)
        {
            Stats = stats.ToDictionary();
        }

        public IReadOnlyDictionary<IIdentifier, double> Stats { get; }
    }
}