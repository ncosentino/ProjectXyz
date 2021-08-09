using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Stats.Default
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