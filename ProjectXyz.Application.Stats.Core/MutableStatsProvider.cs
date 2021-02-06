using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Stats;

namespace ProjectXyz.Plugins.Stats
{

    public sealed class MutableStatsProvider : IMutableStatsProvider
    {
        private readonly Dictionary<IIdentifier, double> _stats;

        public MutableStatsProvider()
            : this(new Dictionary<IIdentifier, double>())
        {
        }

        public MutableStatsProvider(IEnumerable<KeyValuePair<IIdentifier, double>> stats)
        {
            _stats = stats.ToDictionary(x => x.Key, x => x.Value);
        }

        public IReadOnlyDictionary<IIdentifier, double> Stats => _stats;

        public void UsingMutableStats(Action<IDictionary<IIdentifier, double>> callback)
        {
            callback(_stats);
        }
    }
}