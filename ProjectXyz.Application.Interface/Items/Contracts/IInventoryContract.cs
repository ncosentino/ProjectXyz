using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(IInventory))]
    public abstract class IInventoryContract : IInventory
    {
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
