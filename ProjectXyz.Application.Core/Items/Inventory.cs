using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class Inventory : IMutableInventory
    {
        #region Fields
        private readonly Dictionary<int, IItem> _items;

        private int _itemCapacity;
        private double _weightCapacity;
        #endregion

        #region Constructors
        private Inventory()
        {
            _items = new Dictionary<int, IItem>();
        }
        #endregion

        #region Events
        public event EventHandler<EventArgs> CapacityChanged;

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        #endregion

        #region Properties
        public IEnumerable<int> UsedSlots
        {
            get { return _items.Keys; }
        }

        public IItem this[int slot]
        {
            get
            {
                return _items.ContainsKey(slot)
                    ? _items[slot]
                    : null;
            }
        }

        public double CurrentWeight
        {
            get { return _items.Values.TotalWeight(); }
        }

        public double WeightCapacity
        {
            get
            {
                return _weightCapacity;
            }

            set
            {
                if (_weightCapacity == value)
                {
                    return;
                }

                _weightCapacity = value;
                OnCapacityChanged();
            }
        }

        public int ItemCapacity
        {
            get
            {
                return _itemCapacity;
            }

            set
            {
                if (_itemCapacity == value)
                {
                    return;
                }

                _itemCapacity = value;
                OnCapacityChanged();
            }
        }

        public int Count
        {
            get { return _items.Count; }
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
            foreach (var item in _items.Values)
            {
                item.UpdateElapsedTime(elapsedTime);
            }
        }

        public void Add(IEnumerable<IItem> items)
        {
            IList<IItem> itemsCopy = items.ToArray();
            if (itemsCopy.Count < 1)
            {
                return;
            }

            foreach (var item in itemsCopy)
            {
                var nextSlot = FindNextOpenSlot();
                _items.Add(nextSlot, item);
            }

            OnCollectionChanged(
                NotifyCollectionChangedAction.Add,
                itemsCopy.ToArray());
        }

        public bool Remove(IEnumerable<IItem> items)
        {
            var changedItems = new List<IItem>();

            var slots = new HashSet<int>(_items.Keys);
            foreach (var item in items)
            {
                int removedSlot = -1;
                foreach (var slot in slots)
                {
                    if (_items[slot] == item)
                    {
                        removedSlot = slot;
                        break;
                    }
                }

                if (removedSlot >= 0)
                {
                    slots.Remove(removedSlot);
                    _items.Remove(removedSlot);
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

        public void Clear()
        {
            var previousCount = _items.Count;
            var items = _items.Keys.ToArray();

            _items.Clear();

            if (previousCount > 0)
            {
                OnCollectionChanged(
                    NotifyCollectionChangedAction.Reset, 
                    items);
            }
        }

        public bool SlotOccupied(int slot)
        {
            return _items.ContainsKey(slot);
        }

        public void Add(IItem item, int slot)
        {
            if (SlotOccupied(slot))
            {
                throw new InvalidOperationException(string.Format("Cannot add '{0}' because slot {1} is occupied by '{2}'.", item, slot, _items[slot]));
            }

            _items[slot] = item;

            OnCollectionChanged(
                NotifyCollectionChangedAction.Add,
                new[] { item });
        }

        private int FindNextOpenSlot()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (!_items.ContainsKey(i))
                {
                    return i;
                }
            }

            return _items.Count;
        }

        private void OnCapacityChanged()
        {
            var handler = CapacityChanged;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
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

        public IEnumerator<IItem> GetEnumerator()
        {
            return _items.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
        #endregion
    }
}
