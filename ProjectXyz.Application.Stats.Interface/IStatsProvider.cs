using System.Collections.Generic;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Application.Stats.Interface
{
    public interface IStatsProvider
    {
        IReadOnlyDictionary<IIdentifier, double> Stats { get; }
    }
}