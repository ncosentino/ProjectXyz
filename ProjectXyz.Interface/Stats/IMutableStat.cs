using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Interface.Stats
{
    public interface IMutableStat : IStat
    {
        #region Methods
        void SetValue(double value);
        #endregion
    }
}
