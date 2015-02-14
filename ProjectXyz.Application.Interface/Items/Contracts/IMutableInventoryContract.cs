using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(IMutableInventory))]
    public abstract class IMutableInventoryContract : IMutableInventory
    {
        #region Events
        public abstract event EventHandler<EventArgs> CapacityChanged;

        public abstract event NotifyCollectionChangedEventHandler CollectionChanged;
        #endregion

        #region Properties
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

        public abstract int Count { get; }

        public abstract double CurrentWeight { get; }

        public abstract double WeightCapacity { get; }

        public abstract int ItemCapacity { get; }

        public abstract IItemCollection Items { get; }

        public abstract IItem this[int slot] { get; }

        public abstract IEnumerable<int> UsedSlots { get; }
        #endregion       

        #region Methods
        public void Add(IEnumerable<IItem> items)
        {
            Contract.Requires<ArgumentNullException>(items != null);
        }

        public bool Remove(IEnumerable<IItem> items)
        {
            Contract.Requires<ArgumentNullException>(items != null);
            return default(bool);
        }

        public bool SlotOccupied(int slot)
        {
            Contract.Requires<ArgumentOutOfRangeException>(slot >= 0);

            return default(bool);
        }

        public void Add(IItem item, int slot)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            Contract.Requires<ArgumentOutOfRangeException>(slot >= 0);
        }

        public abstract void UpdateElapsedTime(TimeSpan elapsedTime);

        public abstract void Clear();

        public abstract IEnumerator<IItem> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return default(IEnumerator);
        }
        #endregion
    }
}
