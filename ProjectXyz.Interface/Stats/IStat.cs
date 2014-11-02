using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Interface.Stats
{
    public interface IStat
    {
        #region Properties
        string Id { get; }

        double Value { get; }
        #endregion
    }
}
