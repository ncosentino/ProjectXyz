using System.Collections.Generic;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Stats.Default
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