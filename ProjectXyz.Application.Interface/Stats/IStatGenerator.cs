using System.Collections.Generic;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Interface.Stats
{
    public interface IStatGenerator
    {
        IEnumerable<IStat> GenerateStats(
            IRandom randomizer,
            IEnumerable<IStatRange> statRanges);
    }
}