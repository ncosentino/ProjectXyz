using System;
using System.Collections.Generic;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Application.Stats.Interface
{
    public interface IMutableStatsProvider : IStatsProvider
    {
        void UsingMutableStats(Action<IDictionary<IIdentifier, double>> callback);
    }
}