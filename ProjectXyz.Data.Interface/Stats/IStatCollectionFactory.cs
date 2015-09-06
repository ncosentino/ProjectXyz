using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Stats
{
    public interface IStatCollectionFactory
    {
        #region Methods
        IMutableStatCollection CreateEmpty();

        IMutableStatCollection Create(IEnumerable<IStat> stats);
        #endregion
    }
}
