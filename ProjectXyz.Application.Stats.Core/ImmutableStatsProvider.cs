using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Application.Stats.Interface;

namespace ProjectXyz.Application.Stats.Core
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