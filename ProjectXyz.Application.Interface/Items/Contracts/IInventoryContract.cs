using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(IInventory))]
    public abstract class IInventoryContract : IInventory
    {
        #region Events
        public abstract event EventHandler<EventArgs> CapacityChanged;

        public abstract event NotifyCollectionChangedEventHandler CollectionChanged;
        #endregion

        #region Properties
        public double CurrentWeight
        {
            get
            {
                Contract.Ensures(Contract.Result<double>() >= 0);
                return default(double);
            }
        }

        public double WeightCapacity
        {
            get
            {
                Contract.Ensures(Contract.Result<double>() >= 0);
                return default(double);
            }
        }

        public int ItemCapacity
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }
        }

        public IItemCollection Items
        {
            get
            {
                Contract.Ensures(Contract.Result<IItemCollection>() != null);
                return default(IItemCollection);
            }
        }
        #endregion       

        #region Methods
        public abstract void UpdateElapsedTime(TimeSpan elapsedTime);
        #endregion
    }
}
