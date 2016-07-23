using System.Collections.Generic;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;

namespace ProjectXyz.Application.Core.Stats
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