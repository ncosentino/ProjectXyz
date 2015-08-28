using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class Equipment : IMutableEquipment
    {
        #region Fields
        private readonly Dictionary<Guid, IItem> _items;
        #endregion

        #region Constructors
        private Equipment()
        {
            _items = new Dictionary<Guid, IItem>();
        }
        #endregion

        #region Events
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public event EventHandler<EquipmentChangedEventArgs> EquipmentChanged;
        #endregion

        #region Properties
        public int Count
        {
            get { return _items.Count; }
        }

        public IItem this[Guid slotId]
        {
            get
            {
                return _items.ContainsKey(slotId)
                    ? _items[slotId]
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

        public bool CanEquip(IItem item, Guid slotId)
        {
            return !item.IsBroken() && !_items.ContainsKey(slotId) && item.EquippableSlotIds.Contains(slotId);
        }

        public void Equip(IItem item, Guid slotId)
        {
            _items[slotId] = item;
            OnCollectionChanged(
                NotifyCollectionChangedAction.Add,
                item);
            OnEquipmentChanged(slotId);
        }

        public bool CanUnequip(Guid slotId)
        {
            return _items.ContainsKey(slotId);
        }

        public IItem Unequip(Guid slotId)
        {
            if (!_items.ContainsKey(slotId))
            {
                throw new InvalidOperationException(string.Format("There is no item to unequip from slot {0}.", slotId));
            }

            var item = _items[slotId];
            if (!_items.Remove(slotId) || item == null)
            {
                throw new InvalidOperationException(string.Format("No item was removed from slot {0}.", slotId));
            }

            OnCollectionChanged(
                NotifyCollectionChangedAction.Remove, 
                item);
            OnEquipmentChanged(slotId);
            
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

        private void OnEquipmentChanged(Guid slotId)
        {
            var handler = EquipmentChanged;
            if (handler != null)
            {
                var args = new EquipmentChangedEventArgs(slotId);
                handler.Invoke(this, args);
            }
        }
        #endregion
    }
}
