using System.Collections.Generic;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Stats
{
    public interface IMutableStatsProviderFactory
    {
        IMutableStatsProvider Create();

        IMutableStatsProvider Create(IEnumerable<KeyValuePair<IIdentifier, double>> stats);
    }
}