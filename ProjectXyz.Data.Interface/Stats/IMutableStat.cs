using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Interface.Stats
{
    public interface IMutableStat : IStat
    {
        #region Properties
        new string Id { get; set; }

        new double Value { get; set; }
        #endregion
    }
}
