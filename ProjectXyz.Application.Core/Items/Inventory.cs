using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
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

        #region Events
        public event NotifyCollectionChangedEventHandler CollectionChanged;
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

        public void Add(IEnumerable<IItem> items)
        {
            _items.Add(items);
            OnCollectionChanged(
                NotifyCollectionChangedAction.Add, 
                items.ToArray());
        }

        public bool Remove(IEnumerable<IItem> items)
        {
            var changedItems = new List<IItem>();
            foreach (var item in items)
            {
                if (_items.Remove(item))
                {
                    changedItems.Add(item);
                }
            }

            var removedAny = changedItems.Count > 0;
            if (removedAny)
            {
                OnCollectionChanged(
                    NotifyCollectionChangedAction.Remove,
                    changedItems);
            }

            return removedAny;
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, IList items)
        {
            Contract.Requires<ArgumentNullException>(items != null);

            var handler = CollectionChanged;
            if (handler != null)
            {
                var args = new NotifyCollectionChangedEventArgs(
                    action,
                    items);
                handler.Invoke(this, args);
            }
        }
        #endregion
    }
}
