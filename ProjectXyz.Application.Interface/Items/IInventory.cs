using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Items.Contracts;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(IInventoryContract))]
    public interface IInventory : IUpdateElapsedTime, INotifyCollectionChanged
    {
        #region Events
        event EventHandler<EventArgs> CapacityChanged;
        #endregion

        #region Properties
        double CurrentWeight { get; }

        double WeightCapacity { get; }

        int ItemCapacity { get; }

        IItemCollection Items { get; }
        #endregion
    }
}
