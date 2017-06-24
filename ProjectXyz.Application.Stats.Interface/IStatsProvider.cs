using System.Collections.Generic;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Stats.Interface
{
    public interface IStatsProvider
    {
        IReadOnlyDictionary<IIdentifier, double> Stats { get; }
    }
}