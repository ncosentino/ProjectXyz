using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using ProjectXyz.Application.Interface.Items.Contracts;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(IObservableDurabilityContract))]
    public interface IObservableDurability : IDurable
    {
        #region Events
        event EventHandler<EventArgs> DurabilityChanged;
        #endregion
    }
}
