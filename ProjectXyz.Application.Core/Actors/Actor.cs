﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Collections.Specialized;

using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Stats.ExtensionMethods;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Application.Interface.Actors;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Data.Interface.Actors;

namespace ProjectXyz.Application.Core.Actors
{
    public sealed class Actor : IActor
    {
        #region Fields
        private readonly IActorContext _context;
        private readonly IActorStore _actor;
        private readonly IMutableEquipment _equipment;
        private readonly IEnchantmentBlock _enchantments;
        private readonly IMutableInventory _inventory;
        
        private IMutableStatCollection _stats;
        #endregion

        #region Constructors
        private Actor(IActorContext context, IActorStore actorStore)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(actorStore != null);

            _context = context;
            _actor = actorStore;

            _stats = StatCollection.Create();

            _equipment = Items.Equipment.Create();
            _equipment.CollectionChanged += Equipment_CollectionChanged;

            _inventory = Items.Inventory.Create();
            _inventory.CollectionChanged += Inventory_CollectionChanged;

            _enchantments = EnchantmentBlock.Create();
            _enchantments.CollectionChanged += Enchantments_CollectionChanged;
        }
        #endregion

        #region Properties
        public float X
        {
            get;
            private set;
        }

        public float Y
        {
            get;
            private set;
        }

        public string AnimationResource
        {
            get { throw new NotImplementedException(); }
        }

        public IStatCollection Stats
        {
            get
            {
                EnsureStatsCalculated();
                return _stats;
            }
        }

        public IObservableEquipment Equipment
        {
            get { return _equipment; }
        }

        public IMutableInventory Inventory
        {
            get { return _inventory; }
        }
        #endregion

        #region Methods
        public static IActor Create(IActorContext context, IActorStore actorStore)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(actorStore != null);
            Contract.Ensures(Contract.Result<IActor>() != null);
            return new Actor(context, actorStore);
        }

        public void UpdatePosition(float x, float y)
        {
            X = x;
            Y = y;
        }

        public bool CanUseItem(IItem item)
        {
            // TODO: check requirements

            var slots = item.EquippableSlots.ToArray();
            foreach (var slot in slots)
            {
                if (CanEquip(item, slot))
                {
                    return true;
                }
            }

            // TODO: handle potions and consumables?
            throw new NotImplementedException("Cannot check if '" + item + "' can be used because there is no implementation yet!");
        }

        public void UseItem(IItem item)
        {
            var slots = item.EquippableSlots.ToArray();
            foreach (var slot in slots)
            {
                if (CanEquip(item, slot))
                {
                    Equip(item, slot);
                    return;
                }
            }

            // TODO: handle potions and consumables?
            throw new NotImplementedException("Cannot use '" + item + "' because there is no implementation yet!");
        }

        public bool CanEquip(IItem item, string slot)
        {
            // TODO: check requirements

            return _equipment.CanEquip(item, slot);
        }

        public void Equip(IItem item, string slot)
        {
            _equipment.Equip(item, slot);
        }

        public bool CanUnequip(string slot)
        {
            return _equipment.CanUnequip(slot);
        }

        public IItem Unequip(string slot)
        {
            return _equipment.Unequip(slot);
        }

        public void UpdateElapsedTime(TimeSpan elapsedTime)
        {
            _equipment.UpdateElapsedTime(elapsedTime);
        }

        private void FlagStatsAsDirty()
        {
            _stats = null;
        }

        private void EnsureStatsCalculated()
        {
            Contract.Ensures(_stats != null);
            if (_stats != null)
            {
                return;
            }

            _stats = StatCollection.Create(_context.EnchantmentCalculator.Calculate(
                _actor.Stats,
                _enchantments));
            _stats.Set(Stat.Create(
                ActorStats.CurrentLife,
                CalculateCurrentLife(_stats)));
        }

        private void UnequipToInventory(IMutableEquipment equipment, string slot, IMutableItemCollection items)
        {
            Contract.Requires<ArgumentNullException>(equipment != null);
            Contract.Requires<ArgumentNullException>(slot != null);
            Contract.Requires<ArgumentException>(slot.Trim().Length > 0);
            Contract.Requires<ArgumentNullException>(items != null);

            var item = equipment.Unequip(slot);
            if (item == null)
            {
                throw new InvalidOperationException(string.Format("There is no item to unequip from slot {0}.", slot));
            }

            items.Add(item);
        }

        private double CalculateCurrentLife(IMutableStatCollection stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Ensures(Contract.Result<double>() >= 0);

            return Math.Max(
                0,
                Math.Min(
                    stats.GetValueOrDefault(ActorStats.CurrentLife, 0),
                    stats.GetValueOrDefault(ActorStats.MaximumLife, 0)));
        }

        private void OnItemEquipped(IItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            Contract.Requires<InvalidOperationException>(_enchantments != null);

            item.DurabilityChanged += EquippedItem_DurabilityChanged;

            var enchantments = item.Enchantments.TriggeredBy(EnchantmentTriggers.Equip);
            _enchantments.Add(enchantments);
            
            FlagStatsAsDirty();
        }

        private void OnItemUnequipped(IItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            Contract.Requires<InvalidOperationException>(_enchantments != null);

            item.DurabilityChanged -= EquippedItem_DurabilityChanged;

            var enchantments = item.Enchantments.TriggeredBy(EnchantmentTriggers.Equip);
            _enchantments.Remove(enchantments);

            FlagStatsAsDirty();
        }

        private void OnEquippedItemBroken(IItem item, IMutableEquipment equipment, IMutableInventory destination)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            Contract.Requires<ArgumentNullException>(equipment != null);
            Contract.Requires<ArgumentNullException>(destination != null);

            var slot = equipment.GetEquippedSlot(item);
            UnequipToInventory(equipment, slot, _inventory);
        }

        private void OnItemAcquired(IItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            Contract.Requires<InvalidOperationException>(_enchantments != null);

            var enchantments = item.Enchantments.TriggeredBy(EnchantmentTriggers.Hold);
            _enchantments.Add(enchantments);

            FlagStatsAsDirty();
        }

        private void OnItemLost(IItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            Contract.Requires<InvalidOperationException>(_enchantments != null);

            var enchantments = item.Enchantments.TriggeredBy(EnchantmentTriggers.Hold);
            _enchantments.Remove(enchantments);

            FlagStatsAsDirty();
        }
        #endregion

        #region Event Handlers
        private void EquippedItem_DurabilityChanged(object sender, EventArgs e)
        {
            Contract.Requires<ArgumentNullException>(sender != null);
            Contract.Requires<ArgumentNullException>(_equipment != null);
            Contract.Requires<ArgumentNullException>(_inventory != null);

            var item = (IItem)sender;
            if (item.IsBroken())
            {
                OnEquippedItemBroken(item, _equipment, _inventory);   
            }
        }

        private void Enchantments_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            FlagStatsAsDirty();
        }

        private void Equipment_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (IItem item in e.NewItems)
                {
                    OnItemEquipped(item);
                }
            }

            if (e.OldItems != null)
            {
                foreach (IItem item in e.OldItems)
                {
                    OnItemUnequipped(item);
                }
            }
        }

        private void Inventory_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (IItem item in e.NewItems)
                {
                    OnItemAcquired(item);
                }
            }

            if (e.OldItems != null)
            {
                foreach (IItem item in e.OldItems)
                {
                    OnItemLost(item);
                }
            }
        }
        #endregion
    }
}
