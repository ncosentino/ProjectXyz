using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Stats;

namespace ProjectXyz.Plugins.Stats
{
    public sealed class MutableStatsProviderFactory : IMutableStatsProviderFactory
    {
        public IMutableStatsProvider Create() =>
            Create(new Dictionary<IIdentifier, double>());

        public IMutableStatsProvider Create(IEnumerable<KeyValuePair<IIdentifier, double>> stats)
        {
            return new MutableStatsProvider(stats);
        }
    }
}