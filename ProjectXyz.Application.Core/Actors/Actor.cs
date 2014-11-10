using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.Collections.Specialized;

using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Stats.ExtensionMethods;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Application.Interface.Actors;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Core.Enchantments;

namespace ProjectXyz.Application.Core.Actors
{
    public class Actor : IActor
    {
        #region Fields
        private readonly IActorContext _context;
        private readonly ProjectXyz.Data.Interface.Actors.IActor _actor;
        private readonly IMutableEquipment _equipment;
        private readonly IEnchantmentBlock _enchantments;
        private readonly IMutableInventory _inventory;
        
        private IMutableStatCollection _stats;
        private bool _statsDirty;
        #endregion

        #region Constructors
        protected Actor(IActorBuilder builder, IActorContext context, ProjectXyz.Data.Interface.Actors.IActor actor)
        {
            Contract.Requires<ArgumentNullException>(builder != null);
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(actor != null);

            _context = context;
            _actor = actor;

            _stats = StatCollection.Create();

            _equipment = Items.Equipment.Create();
            _equipment.CollectionChanged += Equipment_CollectionChanged;

            _inventory = Items.Inventory.Create();

            _enchantments = EnchantmentBlock.Create();
            _enchantments.CollectionChanged += Enchantments_CollectionChanged;
        }
        #endregion

        #region Properties
        public double MaximumLife
        {
            get
            {
                EnsureEnchantmentsCalculated();
                return _stats.GetValueOrDefault(ActorStats.MaximumLife, 0);
            }
        }

        public double CurrentLife
        {
            get
            {
                EnsureEnchantmentsCalculated();
                return _stats.GetValueOrDefault(ActorStats.CurrentLife, 0);
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
        public static IActor Create(IActorBuilder builder, IActorContext context, ProjectXyz.Data.Interface.Actors.IActor actor)
        {
            Contract.Requires<ArgumentNullException>(builder != null);
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(actor != null);
            Contract.Ensures(Contract.Result<IActor>() != null);
            return new Actor(builder, context, actor);
        }

        public bool Equip(IItem item)
        {
            return 
                !item.IsBroken() && 
                _equipment.EquipToFirstOpenSlot(item);
        }

        public bool Unequip(string slot, IMutableInventory destination)
        {
            return Unequip(slot, destination, false);
        }

        public void UpdateElapsedTime(TimeSpan elapsedTime)
        {
            _equipment.UpdateElapsedTime(elapsedTime);
        }

        private void FlagStatsAsDirty()
        {
            _statsDirty = true;
        }

        protected void EnsureEnchantmentsCalculated()
        {
            if (!_statsDirty)
            {
                return;
            }

            _statsDirty = false;

            _stats = StatCollection.Create(_context.EnchantmentCalculator.Calculate(
                _actor.Stats,
                _enchantments));
            _stats.Set(Stat.Create(
                ActorStats.CurrentLife,
                CalculateCurrentLife(_stats)));
        }

        private bool Unequip(string slot, IMutableInventory destination, bool skipCapacityCheck)
        {
            var item = _equipment[slot];
            if (item == null)
            {
                return false;
            }

            if (!skipCapacityCheck && (destination.Items.Count >= destination.ItemCapacity ||
                destination.CurrentWeight + item.Weight > destination.WeightCapacity))
            {
                return false;
            }

            _equipment.Unequip(slot);
            destination.AddItem(item);
            return true;
        }

        private double CalculateCurrentLife(IMutableStatCollection stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Ensures(Contract.Result<double>() >= 0);

            return Math.Max(
                0,
                Math.Min(
                    _stats.GetValueOrDefault(ActorStats.CurrentLife, 0),
                    _stats.GetValueOrDefault(ActorStats.MaximumLife, 0)));
        }

        private void OnItemEquipped(IItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null);

            item.Broken += EquippedItem_Broken;

            foreach (var enchantment in item.Enchantments)
            {
                _enchantments.Add(enchantment);
            }
            
            FlagStatsAsDirty();
        }

        private void OnItemUnequipped(IItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null);

            item.Broken -= EquippedItem_Broken;

            foreach (var enchantment in item.Enchantments)
            {
                _enchantments.Remove(enchantment);
            }

            FlagStatsAsDirty();
        }
        #endregion

        #region Event Handlers
        private void EquippedItem_Broken(object sender, EventArgs e)
        {
            Contract.Requires<ArgumentNullException>(sender != null);
            Contract.Requires<ArgumentNullException>(e != null);

            var slot = _equipment.GetEquippedSlot((IItem)sender);
            Unequip(slot, _inventory, true);
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
        #endregion
    }
}
