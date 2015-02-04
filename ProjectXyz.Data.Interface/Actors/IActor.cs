using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Interface.Actors
{
    public interface IActor
    {
        #region Properties
        IMutableStatCollection Stats { get; }
        #endregion
    }
}
