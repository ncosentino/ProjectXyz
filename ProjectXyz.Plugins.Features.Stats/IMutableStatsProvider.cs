using System;
using System.Collections.Generic;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Stats
{
    public interface IMutableStatsProvider : IStatsProvider
    {
        event EventHandler<StatsChangedEventArgs> StatsModified;

        void UsingMutableStats(Action<IDictionary<IIdentifier, double>> callback);
    }
}