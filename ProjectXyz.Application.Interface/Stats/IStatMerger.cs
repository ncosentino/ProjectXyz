using System.Collections.Generic;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Interface.Stats
{
    public interface IStatMerger
    {
        #region Methods
        IEnumerable<IStat> MergeStats(
            IEnumerable<IStat> stats1,
            IEnumerable<IStat> stats2);
        #endregion
    }
}