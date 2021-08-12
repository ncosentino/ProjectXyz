using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Stats
{
    public interface IMutableStatsProvider : IStatsProvider
    {
        event EventHandler<StatsChangedEventArgs> StatsModified;

        Task UsingMutableStatsAsync(Func<IDictionary<IIdentifier, double>, Task> callback);
    }
}