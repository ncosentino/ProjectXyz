using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Application.Interface.Items
{
    public interface IMutableDurability : IDurability
    {
        #region Properties
        new int Maximum { get; set; }

        new int Current { get; set; }
        #endregion
    }
}
