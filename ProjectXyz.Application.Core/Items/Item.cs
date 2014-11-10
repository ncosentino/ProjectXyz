using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.Collections.Specialized;

using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Items.Materials;
using ProjectXyz.Data.Interface.Stats.ExtensionMethods;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;

namespace ProjectXyz.Application.Core.Items
{
    public class Item : IItem
    {
        #region Fields
        private readonly IMutableItemCollection _socketedItems;
        private readonly IEnchantmentBlock _enchantments;
        private readonly IItemContext _context;
        private readonly ProjectXyz.Data.Interface.Items.IItem _item;
        private readonly IMutableRequirements _requirements;

        private bool _statsDirty;
        private IMutableStatCollection _stats;
        #endregion

        #region Constructors
        protected Item(IItemBuilder builder, IItemContext context, ProjectXyz.Data.Interface.Items.IItem item)
        {
            Contract.Requires<ArgumentNullException>(builder != null);
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(item != null);

            _context = context;
            _item = item;

            this.Material = builder.MaterialFactory.Load(_item.MaterialType);

            var socketedItems = _item.SocketedItems.Select(x => Item.Create(builder, _context, x));
            _socketedItems = ItemCollection.Create(socketedItems);

            var enchantments = _item.Enchantments.Select(x => Enchantment.Create(context.EnchantmentContext, x));
            _enchantments = EnchantmentBlock.Create(enchantments);
            _enchantments.CollectionChanged += Enchantments_CollectionChanged;

            _requirements = Items.Requirements.Create(_item.Requirements);

            _statsDirty = true;
        }
        #endregion

        #region Events
        public event EventHandler<EventArgs> Broken;
        #endregion

        #region Properties
        public Guid Id
        {
            get { return _item.Id; }
        }

        public string Name
        {
            get { return _item.Name; }
        }

        public string MagicType
        {
            get { return _item.MagicType; }
        }

        public IMaterial Material
        {
            get;
            private set;
        }

        public string ItemType
        {
            get { return _item.ItemType; }
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
            get { return _item.EquippableSlots; }
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

        public IRequirements Requirements
        {
            get { return _requirements; }
        }

        public IItemCollection SocketedItems
        {
            get { return _socketedItems; }
        }
        #endregion

        #region Methods
        public static IItem Create(IItemBuilder builder, IItemContext context, ProjectXyz.Data.Interface.Items.IItem item)
        {
            Contract.Requires<ArgumentNullException>(builder != null);
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(item != null);
            Contract.Ensures(Contract.Result<IItem>() != null);
            return new Item(builder, context, item);
        }

        public void Enchant(IEnumerable<IEnchantment> enchantments)
        {
            _enchantments.AddRange(enchantments);
        }

        public void Disenchant(IEnumerable<IEnchantment> enchantments)
        {
            _enchantments.RemoveRange(enchantments);
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
            _statsDirty = true;
            return true;
        }

        public void UpdateElapsedTime(TimeSpan elapsedTime)
        {
            var enchantmentCount = _enchantments.Count;
            _enchantments.UpdateElapsedTime(elapsedTime);
            _statsDirty |= enchantmentCount != _enchantments.Count;
        }

        protected void EnsureStatsCalculated()
        {
            if (!_statsDirty)
            {
                return;
            }

            _statsDirty = false;

            bool wasBroken = _stats != null && this.IsBroken();

            _stats = StatCollection.Create(_context.EnchantmentCalculator.Calculate(
                _item.Stats,
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

            if (!wasBroken && this.IsBroken())
            {
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
            _statsDirty = true;
            EnsureStatsCalculated();
        }

        private void EnsureDurabilityInRange(IMutableStatCollection stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);

            stats.Set(Stat.Create(
                ItemStats.MaximumDurability,
                Math.Max(0, _stats.GetValueOrDefault(ItemStats.MaximumDurability, 0))));
            stats.Set(Stat.Create(
                ItemStats.CurrentDurability,
                Math.Max(0,
                    Math.Min(
                        _stats.GetValueOrDefault(ItemStats.CurrentDurability, 0),
                        _stats.GetValueOrDefault(ItemStats.MaximumDurability, 0)))));
        }

        private IDurability CalculateDurability(IStatCollection stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Ensures(Contract.Result<IDurability>() != null);
            return Items.Durability.Create(
                (int)_stats.GetValueOrDefault(ItemStats.MaximumDurability, 0),
                (int)_stats.GetValueOrDefault(ItemStats.CurrentDurability, 0));
        }

        private double CalculateWeight(IStatCollection stats, IEnumerable<IItem> socketedItems)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Requires<ArgumentNullException>(socketedItems != null);
            return _stats.GetValueOrDefault(ItemStats.Weight, 0) + socketedItems.TotalWeight();
        }

        private double CalculateValue(IStatCollection stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            return _stats.GetValueOrDefault(ItemStats.Value, 0);
        }

        private int CalculateTotalSockets(IStatCollection stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Ensures(Contract.Result<int>() >= 0);
            return (int)_stats.GetValueOrDefault(ItemStats.TotalSockets, 0);
        }

        private int CalculateOpenSockets(int totalSockets, IEnumerable<IItem> socketedItems)
        {
            Contract.Requires<ArgumentOutOfRangeException>(totalSockets >= 0);
            Contract.Requires<ArgumentNullException>(socketedItems != null);
            Contract.Ensures(Contract.Result<int>() >= 0);
            return Math.Max(0, totalSockets - socketedItems.TotalRequiredSockets());
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
