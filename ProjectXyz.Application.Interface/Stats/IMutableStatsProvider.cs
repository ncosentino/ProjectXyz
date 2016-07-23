using System;
using System.Collections.Generic;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Stats
{
    public interface IMutableStatsProvider : IStatsProvider
    {
        void UsingMutableStats(Action<IDictionary<IIdentifier, double>> callback);
    }
}