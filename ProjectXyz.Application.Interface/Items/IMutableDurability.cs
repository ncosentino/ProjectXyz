using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Application.Interface.Items
{
    public interface IMutableDurability : IDurability
    {
        #region Methods
        void SetMaximum(int value);

        void SetCurrent(int value);
        #endregion
    }
}
