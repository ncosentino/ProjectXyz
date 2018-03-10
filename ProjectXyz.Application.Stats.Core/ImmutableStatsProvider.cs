using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Application.Stats.Interface;
using ProjectXyz.Framework.Interface.Collections;

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