using System;
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
using ProjectXyz.Application.Interface.Enchantments.ExtensionMethods;
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

            foreach (var slotId in item.EquippableSlotIds)
            {
                if (CanEquip(item, slotId))
                {
                    return true;
                }
            }

            // TODO: handle potions and consumables?
            throw new NotImplementedException("Cannot check if '" + item + "' can be used because there is no implementation yet!");
        }

        public void UseItem(IItem item)
        {
            foreach (var slotId in item.EquippableSlotIds)
            {
                if (CanEquip(item, slotId))
                {
                    Equip(item, slotId);
                    return;
                }
            }

            // TODO: handle potions and consumables?
            throw new NotImplementedException("Cannot use '" + item + "' because there is no implementation yet!");
        }

        public bool CanEquip(IItem item, Guid slotId)
        {
            // TODO: check requirements

            return _equipment.CanEquip(item, slotId);
        }

        public void Equip(IItem item, Guid slotId)
        {
            _equipment.Equip(item, slotId);
        }

        public bool CanUnequip(Guid slotId)
        {
            return _equipment.CanUnequip(slotId);
        }

        public IItem Unequip(Guid slotId)
        {
            return _equipment.Unequip(slotId);
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

            var result = _context.EnchantmentCalculator.Calculate(
                _actor.Stats,
                _enchantments);

            // keep the enchantments changed event from triggering
            try
            {
                _enchantments.CollectionChanged -= Enchantments_CollectionChanged;

                _enchantments.Clear();
                _enchantments.Add(result.Enchantments);
            }
            finally 
            {

                _enchantments.CollectionChanged += Enchantments_CollectionChanged;
            }

            _stats = StatCollection.Create(result.Stats);
            _stats.Set(Stat.Create(
                Guid.NewGuid(), 
                ActorStats.CurrentLife,
                CalculateCurrentLife(_stats)));
        }

        private void UnequipToInventory(IMutableEquipment equipment, Guid slotId, IMutableItemCollection items)
        {
            Contract.Requires<ArgumentNullException>(equipment != null);
            Contract.Requires<ArgumentException>(slotId != Guid.Empty);
            Contract.Requires<ArgumentNullException>(items != null);

            var item = equipment.Unequip(slotId);
            if (item == null)
            {
                throw new InvalidOperationException(string.Format("There is no item to unequip from slot {0}.", slotId));
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
            item.EnchantmentsChanged += EquippedItem_EnchantmentsChanged;

            var enchantments = item.Enchantments.TriggeredBy(EnchantmentTriggers.Equip);
            _enchantments.Add(enchantments);
        }

        private void OnItemUnequipped(IItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            Contract.Requires<InvalidOperationException>(_enchantments != null);

            item.DurabilityChanged -= EquippedItem_DurabilityChanged;
            item.EnchantmentsChanged -= EquippedItem_EnchantmentsChanged;

            var enchantments = item.Enchantments.TriggeredBy(EnchantmentTriggers.Equip);
            _enchantments.Remove(enchantments);
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

            item.EnchantmentsChanged += HeldItem_EnchantmentsChanged;

            var enchantments = item.Enchantments.TriggeredBy(EnchantmentTriggers.Hold);
            _enchantments.Add(enchantments);
        }

        private void OnItemLost(IItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            Contract.Requires<InvalidOperationException>(_enchantments != null);

            item.EnchantmentsChanged -= HeldItem_EnchantmentsChanged;

            var enchantments = item.Enchantments.TriggeredBy(EnchantmentTriggers.Hold);
            _enchantments.Remove(enchantments);
        }

        private void HandleItemEnchantmentsChanged(NotifyCollectionChangedEventArgs e, Guid enchantmentTriggerId)
        {
            if (e.NewItems != null)
            {
                _enchantments.Add(e.NewItems
                    .Cast<IEnchantment>()
                    .TriggeredBy(enchantmentTriggerId));
            }

            if (e.OldItems != null)
            {
                _enchantments.Remove(e.NewItems
                    .Cast<IEnchantment>()
                    .TriggeredBy(enchantmentTriggerId));
            }
        }
        #endregion

        #region Event Handlers
        private void HeldItem_EnchantmentsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            HandleItemEnchantmentsChanged(e, EnchantmentTriggers.Hold);
        }

        private void EquippedItem_EnchantmentsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            HandleItemEnchantmentsChanged(e, EnchantmentTriggers.Equip);
        }

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
