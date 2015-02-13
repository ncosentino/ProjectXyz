using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Collections.Specialized;

using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Stats.ExtensionMethods;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Application.Core.Items
{
    public class Item : IItem
    {
        #region Fields
        private readonly IMutableItemCollection _socketedItems;
        private readonly IEnchantmentBlock _enchantments;
        private readonly IItemContext _context;
        private readonly IMutableRequirements _requirements;
        private readonly Guid _id;
        private readonly string _name;
        private readonly Guid _magicTypeId;
        private readonly string _itemType;
        private readonly Guid _materialTypeId;
        private readonly List<string> _equippableSlots;
        private readonly IMutableStatCollection _baseStats;

        private IMutableStatCollection _stats;
        #endregion

        #region Constructors
        protected Item(IItemContext context, IItemStore item)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(item != null);

            _context = context;
            _id = item.Id;
            _name = item.Name;
            _magicTypeId = item.MagicTypeId;
            _itemType = item.ItemType;
            _materialTypeId = item.MaterialTypeId;
            _equippableSlots = new List<string>(item.EquippableSlots);
            
            _baseStats = StatCollection.Create();
            _baseStats.Add(item.Stats);

            var socketedItems = item.SocketedItems.Select(x => Item.Create(_context, x));
            _socketedItems = ItemCollection.Create(socketedItems);

            var enchantments = item.Enchantments.Select(x => Enchantment.Create(context.EnchantmentContext, x));
            _enchantments = EnchantmentBlock.Create(enchantments);
            _enchantments.CollectionChanged += Enchantments_CollectionChanged;

            _requirements = Items.Requirements.Create(item.Requirements);
        }
        #endregion

        #region Events
        public event EventHandler<EventArgs> Broken;
        #endregion

        #region Properties
        public Guid Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }

        public Guid MagicTypeId
        {
            get { return _magicTypeId; }
        }

        public Guid MaterialTypeId
        {
            get { return _materialTypeId; }
        }

        public string ItemType
        {
            get { return _itemType; }
        }

        public double Weight
        {
            get
            {
                EnsureStatsCalculated();
                return _stats.GetValueOrDefault(ItemStats.Weight, 0);
            }
        }

        public double Value
        {
            get
            {
                EnsureStatsCalculated();
                return _stats.GetValueOrDefault(ItemStats.Value, 0);
            }
        }

        public int RequiredSockets
        {
            get
            {
                EnsureStatsCalculated();
                return (int)_stats.GetValueOrDefault(ItemStats.RequiredSockets, 0);
            }
        }

        public int TotalSockets
        {
            get
            {
                EnsureStatsCalculated();
                return (int)_stats.GetValueOrDefault(ItemStats.TotalSockets, 0);
            }
        }

        public int OpenSockets
        {
            get
            {
                return CalculateOpenSockets(
                    TotalSockets, 
                    _socketedItems);
            }
        }

        public IStatCollection Stats
        {
            get
            {
                EnsureStatsCalculated();
                return _stats;
            }
        }

        public IEnumerable<string> EquippableSlots
        {
            get { return _equippableSlots; }
        }

        public IDurability Durability
        {
            get
            {
                EnsureStatsCalculated();
                return CalculateDurability(_stats);
            }
        }

        public IEnchantmentCollection Enchantments
        {
            get { return _enchantments; }
        }

        public Interface.Items.IRequirements Requirements
        {
            get { return _requirements; }
        }

        public IItemCollection SocketedItems
        {
            get { return _socketedItems; }
        }
        #endregion

        #region Methods
        public static IItem Create(IItemContext context, IItemStore item)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(item != null);
            Contract.Ensures(Contract.Result<IItem>() != null);
            return new Item(context, item);
        }

        public void Enchant(IEnumerable<IEnchantment> enchantments)
        {
            _enchantments.Add(enchantments);
        }

        public void Disenchant(IEnumerable<IEnchantment> enchantments)
        {
            _enchantments.Remove(enchantments);
        }

        public bool Socket(IItem item)
        {
            if (!item.CanBeUsedForSocketing() ||
                OpenSockets < item.RequiredSockets)
            {
                return false;
            }

            _socketedItems.Add(item);
            Enchant(item.Enchantments);
            _stats = null;
            return true;
        }

        public void UpdateElapsedTime(TimeSpan elapsedTime)
        {
            _enchantments.UpdateElapsedTime(elapsedTime);
        }

        public override string ToString()
        {
            return string.Format("Id: {0}, Name: {1}", Id, Name);
        }

        protected void EnsureStatsCalculated()
        {
            Contract.Ensures(_stats != null);
            if (_stats != null)
            {
                return;
            }
            
            _stats = StatCollection.Create(_context.EnchantmentCalculator.Calculate(
                _baseStats,
                _enchantments));
            _stats.Set(Stat.Create(
                ItemStats.Weight, 
                CalculateWeight(_stats, _socketedItems)));
            _stats.Set(Stat.Create(
                ItemStats.Value, 
                CalculateValue(_stats)));
            _stats.Set(Stat.Create(
                ItemStats.TotalSockets, 
                CalculateTotalSockets(_stats)));
            EnsureDurabilityInRange(_stats);

            if (this.IsBroken() && 
                _stats.GetValueOrDefault(ItemStats.Broken, 0) < 1)
            {
                var brokenStat = Stat.Create(ItemStats.Broken, 1);
                _stats.Set(brokenStat);
                _baseStats.Set(brokenStat);
                OnBroken();
            }
        }

        private void OnBroken()
        {
            var handler = Broken;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        private void RecalculateStats()
        {
            _stats = null;
            EnsureStatsCalculated();
        }

        private void EnsureDurabilityInRange(IMutableStatCollection stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);

            stats.Set(Stat.Create(
                ItemStats.MaximumDurability,
                Math.Max(0, stats.GetValueOrDefault(ItemStats.MaximumDurability, 0))));
            stats.Set(Stat.Create(
                ItemStats.CurrentDurability,
                Math.Max(0,
                    Math.Min(
                        stats.GetValueOrDefault(ItemStats.CurrentDurability, 0),
                        stats.GetValueOrDefault(ItemStats.MaximumDurability, 0)))));
        }

        private IDurability CalculateDurability(IStatCollection stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Ensures(Contract.Result<IDurability>() != null);
            return Items.Durability.Create(
                (int)stats.GetValueOrDefault(ItemStats.MaximumDurability, 0),
                (int)stats.GetValueOrDefault(ItemStats.CurrentDurability, 0));
        }

        private double CalculateWeight(IStatCollection stats, IEnumerable<IItem> socketedItems)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Requires<ArgumentNullException>(socketedItems != null);
            return stats.GetValueOrDefault(ItemStats.Weight, 0) + socketedItems.TotalWeight();
        }

        private double CalculateValue(IStatCollection stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            return stats.GetValueOrDefault(ItemStats.Value, 0);
        }

        private int CalculateTotalSockets(IStatCollection stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Ensures(Contract.Result<int>() >= 0);
            return (int)stats.GetValueOrDefault(ItemStats.TotalSockets, 0);
        }

        private int CalculateOpenSockets(int totalSockets, IEnumerable<IItem> socketedItems)
        {
            Contract.Requires<ArgumentOutOfRangeException>(totalSockets >= 0);
            Contract.Requires<ArgumentNullException>(socketedItems != null);
            Contract.Ensures(Contract.Result<int>() >= 0);
            return Math.Max(0, totalSockets - socketedItems.TotalRequiredSockets());
        }

        [ContractInvariantMethod]
        private void InvariantMethod()
        {
            Contract.Invariant(_socketedItems != null);
            Contract.Invariant(_enchantments != null);
            Contract.Invariant(_context != null);
            Contract.Invariant(_requirements != null);
            Contract.Invariant(_equippableSlots != null);
            Contract.Invariant(_baseStats != null);
        }
        #endregion

        #region Event Handlers
        private void Enchantments_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RecalculateStats();
        }
        #endregion
    }
}
