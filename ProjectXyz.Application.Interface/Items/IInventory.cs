using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Items.Contracts;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(IInventoryContract))]
    public interface IInventory : IUpdateElapsedTime, INotifyCollectionChanged, IItemCollection
    {
        #region Events
        event EventHandler<EventArgs> CapacityChanged;
        #endregion

        #region Properties
        IEnumerable<int> UsedSlots { get; }
        
        IItem this[int slot] { get; }

        double CurrentWeight { get; }

        double WeightCapacity { get; }

        int ItemCapacity { get; }
        #endregion
    }
}
