using System.Collections.Generic;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Stats
{
    public interface IStatsProvider
    {
        IReadOnlyDictionary<IIdentifier, double> Stats { get; }
    }
}