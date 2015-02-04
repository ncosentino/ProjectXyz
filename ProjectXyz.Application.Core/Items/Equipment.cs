using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class Equipment : IMutableEquipment
    {
        #region Fields
        private readonly Dictionary<string, IItem> _items;
        #endregion

        #region Constructors
        private Equipment()
        {
            _items = new Dictionary<string, IItem>();
        }
        #endregion

        #region Events
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        #endregion

        #region Properties
        public int Count
        {
            get { return _items.Count; }
        }

        public IItem this[string slot]
        {
            get
            {
                return _items.ContainsKey(slot)
                    ? _items[slot]
                    : default(IItem);
            }
        }
        #endregion

        #region Methods
        public static IMutableEquipment Create()
        {
            Contract.Ensures(Contract.Result<IMutableEquipment>() != null);
            return new Equipment();
        }

        public IEnumerator<IItem> GetEnumerator()
        {
            return _items.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.Values.GetEnumerator();
        }

        public bool Equip(IItem item, string slot)
        {
            if (_items.ContainsKey(slot))
            {
                return false;
            }

            _items[slot] = item;
            OnCollectionChanged(
                NotifyCollectionChangedAction.Add,
                item);

            return true;
        }

        public IItem Unequip(string slot)
        {
            if (!_items.ContainsKey(slot))
            {
                return default(IItem);
            }

            var item = _items[slot];
            
            _items.Remove(slot);
            OnCollectionChanged(
                NotifyCollectionChangedAction.Remove, 
                item);
            
            return item;
        }

        public void UpdateElapsedTime(TimeSpan elapsedTime)
        {
            foreach (var item in _items.Values)
            {
                item.UpdateElapsedTime(elapsedTime);
            }
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, IItem item)
        {
            var handler = CollectionChanged;
            if (handler != null)
            {
                var args = new NotifyCollectionChangedEventArgs(
                    action,
                    item);
                handler.Invoke(this, args);
            }
        }
        #endregion
    }
}
