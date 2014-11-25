using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Interface.Stats
{
    public interface IStat
    {
        #region Properties
        Guid Id { get; }

        double Value { get; }
        #endregion
    }
}
