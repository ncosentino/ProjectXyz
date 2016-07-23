using System.Collections.Generic;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Stats
{
    public interface IStatsProvider
    {
        IReadOnlyDictionary<IIdentifier, double> Stats { get; }
    }
}