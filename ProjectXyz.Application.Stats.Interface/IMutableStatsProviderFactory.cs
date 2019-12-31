using System.Collections.Generic;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Stats
{
    public interface IMutableStatsProviderFactory
    {
        IMutableStatsProvider Create();

        IMutableStatsProvider Create(IEnumerable<KeyValuePair<IIdentifier, double>> stats);
    }
}