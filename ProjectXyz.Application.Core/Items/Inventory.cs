using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class Inventory : IMutableInventory
    {
        #region Fields
        private readonly IMutableItemCollection _items;
        #endregion

        #region Constructors
        private Inventory()
        {
            _items = ItemCollection.Create();
        }
        #endregion

        #region Properties
        public double CurrentWeight
        {
            get { return _items.TotalWeight(); }
        }

        public double WeightCapacity
        {
            get;
            set;
        }

        public int ItemCapacity
        {
            get;
            set;
        }

        public IItemCollection Items
        {
            get { return _items; }
        }
        #endregion

        #region Methods
        public static IMutableInventory Create()
        {
            Contract.Ensures(Contract.Result<IMutableInventory>() != null);
            return new Inventory();
        }

        public void UpdateElapsedTime(TimeSpan elapsedTime)
        {
            foreach (var item in _items)
            {
                item.UpdateElapsedTime(elapsedTime);
            }
        }

        public void AddItem(IItem item)
        {
            _items.Add(item);
        }

        public void RemoveItem(IItem item)
        {
            _items.Remove(item);
        }
        #endregion
    }
}
