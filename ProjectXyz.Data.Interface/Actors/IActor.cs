using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Interface.Actors
{
    public interface IActor
    {
        #region Properties
        IMutableStatCollection<IStat> Stats { get; }
        #endregion
    }
}
