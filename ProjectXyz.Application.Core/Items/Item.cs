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
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;

namespace ProjectXyz.Application.Core.Items
{
    public partial class Item : IItem
    {
        #region Fields
        private readonly IMutableItemCollection _socketedItems;
        private readonly IEnchantmentBlock _enchantments;
        private readonly IEnchantmentCalculator _enchantmentCalculator;
        private readonly ProjectXyz.Data.Interface.Items.IItem _item;

        private bool _statsDirty;
        private int _openSockets;
        private IDurability _durability;
        private IMutableStatCollection<IStat> _stats;
        #endregion

        #region Constructors
        protected Item(IItemBuilder builder, IEnchantmentCalculator enchantmentCalculator, ProjectXyz.Data.Interface.Items.IItem item)
        {
            Contract.Requires<ArgumentNullException>(builder != null);
            Contract.Requires<ArgumentNullException>(enchantmentCalculator != null);
            Contract.Requires<ArgumentNullException>(item != null);

            _enchantmentCalculator = enchantmentCalculator;
            _item = item;

            this.Material = builder.MaterialFactory.Load(_item.MaterialType);

            _socketedItems = MutableItemCollection.Create();
            foreach (var socketCandidateData in _item.SocketedItems)
            {
                var socketCandidate = Item.Create(
                    builder, 
                    enchantmentCalculator, 
                    socketCandidateData);
                _socketedItems.Add(socketCandidate);
            }

            _enchantments = EnchantmentBlock.Create();
            foreach (var enchantment in _item.Enchantments)
            {
                _enchantments.Add(Enchantment.CreateFrom(enchantment));
            }

            _enchantments.CollectionChanged += Enchantments_CollectionChanged;

            _statsDirty = true;
        }
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
                EnsureEnchantmentsCalculated();
                return _stats[ItemStats.Weight].Value;
            }
        }

        public double Value
        {
            get
            {
                EnsureEnchantmentsCalculated();
                return _stats[ItemStats.Value].Value;
            }
        }

        public int RequiredSockets
        {
            get
            {
                EnsureEnchantmentsCalculated();
                return (int)_stats[ItemStats.RequiredSockets].Value;
            }
        }

        public int TotalSockets
        {
            get
            {
                EnsureEnchantmentsCalculated();
                return (int)_stats[ItemStats.TotalSockets].Value;
            }
        }

        public int OpenSockets
        {
            get
            {
                EnsureEnchantmentsCalculated();
                return _openSockets;
            }
        }

        public IDurability Durability
        {
            get
            {
                EnsureEnchantmentsCalculated();
                return _durability;
            }
        }

        public IEnchantmentCollection Enchantments
        {
            get { return _enchantments; }
        }

        public IRequirements Requirements
        {
            get { return Items.Requirements.Create(_item.Requirements); }
        }

        public IItemCollection SocketedItems
        {
            get { return _socketedItems; }
        }
        #endregion

        #region Methods
        public static IItem Create(IItemBuilder builder, IEnchantmentCalculator enchantmentCalculator, ProjectXyz.Data.Interface.Items.IItem item)
        {
            Contract.Requires<ArgumentNullException>(builder != null);
            Contract.Requires<ArgumentNullException>(enchantmentCalculator != null);
            Contract.Requires<ArgumentNullException>(item != null);
            Contract.Ensures(Contract.Result<IItem>() != null);
            return new Item(builder, enchantmentCalculator, item);
        }

        public double GetStat(string statId)
        {
            EnsureEnchantmentsCalculated();
            return _stats.Contains(statId)
                ? _stats[statId].Value
                : 0;
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
            _openSockets -= item.RequiredSockets;
            _statsDirty = true;
            return true;
        }

        public void UpdateElapsedTime(TimeSpan elapsedTime)
        {
            var enchantmentCount = _enchantments.Count;
            _enchantments.UpdateElapsedTime(elapsedTime);
            _statsDirty |= enchantmentCount != _enchantments.Count;
        }

        protected void EnsureEnchantmentsCalculated()
        {
            if (!_statsDirty)
            {
                return;
            }

            _statsDirty = false;

            _stats = MutableStatCollection<IStat>.Create(_enchantmentCalculator.Calculate(
                _item.Stats,
                _enchantments));
            _durability = CalculateDurability(_stats);
            _stats.Set(MutableStat.Create(
                ItemStats.Weight, 
                CalculateWeight(_stats, _socketedItems)));
            _stats.Set(MutableStat.Create(
                ItemStats.Value, 
                CalculateValue(_stats)));
            _stats.Set(MutableStat.Create(
                ItemStats.TotalSockets, 
                CalculateTotalSockets(_stats)));
            _openSockets = CalculateOpenSockets(
                this.TotalSockets, 
                _socketedItems);
        }

        private void FlagStatsAsDirty()
        {
            _statsDirty = true;
        }

        private IDurability CalculateDurability(IStatCollection<IStat> stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Ensures(Contract.Result<IDurability>() != null);
            return ReadonlyDurability.Create(
                (int)stats[ItemStats.MaximumDurability].Value,
                (int)stats[ItemStats.CurrentDurability].Value);
        }

        private double CalculateWeight(IStatCollection<IStat> stats, IEnumerable<IItem> socketedItems)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Requires<ArgumentNullException>(socketedItems != null);
            return stats[ItemStats.Weight].Value + socketedItems.TotalWeight();
        }

        private double CalculateValue(IStatCollection<IStat> stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            return stats[ItemStats.Value].Value;
        }

        private int CalculateTotalSockets(IStatCollection<IStat> stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Ensures(Contract.Result<int>() >= 0);
            return (int)stats[ItemStats.TotalSockets].Value;
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
            FlagStatsAsDirty();
        }
        #endregion
    }
}
