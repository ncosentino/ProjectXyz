using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Application.Interface.Items
{
    public interface IMutableDurability : IObservableDurability
    {
        #region Properties
        new int MaximumDurability { get; set; }

        new int CurrentDurability { get; set; }
        #endregion
    }
}
