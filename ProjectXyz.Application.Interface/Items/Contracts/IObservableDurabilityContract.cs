using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(IObservableDurability))]
    public abstract class IObservableDurabilityContract : IObservableDurability
    {
        #region Events
        public abstract event EventHandler<EventArgs> DurabilityChanged;
        #endregion

        #region Properties
        public abstract int MaximumDurability { get; }

        public abstract int CurrentDurability { get; }
        #endregion
    }
}
