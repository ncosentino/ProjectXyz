using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(IMutableInventory))]
    public abstract class IMutableInventoryContract : IMutableInventory
    {
        #region Events
        public abstract event NotifyCollectionChangedEventHandler CollectionChanged;
        #endregion

        #region Properties
        public abstract double CurrentWeight { get; }

        double IMutableInventory.WeightCapacity
        {
            get
            {
                Contract.Ensures(Contract.Result<double>() >= 0);
                return default(double);
            }

            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value >= 0);
            }
        }

        int IMutableInventory.ItemCapacity
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }

            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value >= 0);
            }
        }

        public abstract double WeightCapacity { get; }

        public abstract int ItemCapacity { get; }

        public abstract IItemCollection Items { get; }
        #endregion       

        #region Methods
        public abstract void UpdateElapsedTime(TimeSpan elapsedTime);

        public void Add(IEnumerable<IItem> items)
        {
            Contract.Requires<ArgumentNullException>(items != null);
        }

        public bool Remove(IEnumerable<IItem> items)
        {
            Contract.Requires<ArgumentNullException>(items != null);
            return default(bool);
        }
        #endregion
    }
}
