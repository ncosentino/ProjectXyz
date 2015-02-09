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
        
        public IEquipment Equipment
        {
            get { return _equipment; }
        }

        public IInventory Inventory
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

        public bool Equip(IItem item)
        {
            return 
                !item.IsBroken() && 
                _equipment.EquipToFirstOpenSlot(item);
        }

        public bool Unequip(string slot, IMutableInventory destination)
        {
            return Unequip(_equipment, slot, destination, false);
        }

        public bool TakeItem(IItem item)
        {
            _inventory.Add(item);
            return true;
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

        private bool Unequip(IMutableEquipment equipment, string slot, IMutableInventory destination, bool skipCapacityCheck)
        {
            Contract.Requires<ArgumentNullException>(equipment != null);
            Contract.Requires<ArgumentNullException>(slot != null);
            Contract.Requires<ArgumentException>(slot != string.Empty);
            Contract.Requires<ArgumentNullException>(destination != null);

            var item = equipment[slot];
            if (item == null)
            {
                return false;
            }

            if (!skipCapacityCheck && (destination.Items.Count >= destination.ItemCapacity ||
                destination.CurrentWeight + item.Weight > destination.WeightCapacity))
            {
                return false;
            }

            equipment.Unequip(slot);
            destination.Add(item);
            return true;
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

            item.Broken += EquippedItem_Broken;

            var enchantments = item.Enchantments.TriggeredBy(EnchantmentTriggers.Equip);
            _enchantments.Add(enchantments);
            
            FlagStatsAsDirty();
        }

        private void OnItemUnequipped(IItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            Contract.Requires<InvalidOperationException>(_enchantments != null);

            item.Broken -= EquippedItem_Broken;

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
            Unequip(equipment, slot, destination, true);
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
        private void EquippedItem_Broken(object sender, EventArgs e)
        {
            Contract.Requires<ArgumentNullException>(sender != null);
            Contract.Requires<ArgumentNullException>(_equipment != null);
            Contract.Requires<ArgumentNullException>(_inventory != null);

            OnEquippedItemBroken((IItem)sender, _equipment, _inventory);
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
