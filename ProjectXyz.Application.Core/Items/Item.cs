using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.ExtensionMethods;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.Affixes;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;
using ProjectXyz.Application.Interface.Items.Requirements;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Items.Names;
using ProjectXyz.Data.Interface.Items.Sockets;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Stats.ExtensionMethods;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class Item : IItem
    {
        #region Fields
        private readonly IMutableItemCollection _socketedItems;
        private readonly IEnchantmentBlock _enchantments;
        private readonly IItemContext _context;
        private readonly IItemRequirements _requirements;
        private readonly Guid _id;
        private readonly Guid _itemDefinitionId;
        private readonly IItemMetaData _itemMetaData;
        private readonly List<Guid> _equippableSlots;
        private readonly IMutableStatCollection _baseStats;
        private readonly List<IItemAffix> _affixes;
        private readonly List<IItemNamePart> _itemNameParts;

        private IMutableStatCollection _stats;
        private bool _statsDirty;
        #endregion

        #region Constructors
        private Item(
            IItemContext context,
            Guid id,
            Guid itemDefinitionId,
            IItemMetaData itemMetaData,
            IEnumerable<IItemNamePart> itemNameParts,
            IItemRequirements itemRequirements,
            IEnumerable<IStat> stats,
            IEnumerable<IEnchantment> enchantments,
            IEnumerable<IItemAffix> affixes,
            IEnumerable<IItem> socketedItems,
            IEnumerable<Guid> equippableSlots)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentNullException>(itemMetaData != null);
            Contract.Requires<ArgumentNullException>(itemNameParts != null);
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Requires<ArgumentNullException>(affixes != null);
            Contract.Requires<ArgumentNullException>(socketedItems != null);
            Contract.Requires<ArgumentNullException>(equippableSlots != null);

            _context = context;
            _id = id;
            _itemDefinitionId = itemDefinitionId;
            _itemMetaData = itemMetaData;
            _itemNameParts = new List<IItemNamePart>(itemNameParts);
            _requirements = itemRequirements;
            _equippableSlots = new List<Guid>(equippableSlots);
            _affixes = new List<IItemAffix>(affixes);
            
            _baseStats = StatCollection.Create(stats);
            _statsDirty = true;

            _socketedItems = ItemCollection.Create(socketedItems);

            _enchantments = EnchantmentBlock.Create(enchantments);
            _enchantments.CollectionChanged += Enchantments_CollectionChanged;
        }
        #endregion

        #region Events
        public event EventHandler<EventArgs> DurabilityChanged;

        public event NotifyCollectionChangedEventHandler EnchantmentsChanged;
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id
        {
            get { return _id; }
        }

        /// <inheritdoc />
        public Guid ItemDefinitionId
        {
            get { return _itemDefinitionId; }
        }

        /// <inheritdoc />
        public IEnumerable<IItemNamePart> ItemNameParts
        {
            get { return _itemNameParts; }
        }

        /// <inheritdoc />
        public Guid InventoryGraphicResourceId
        {
            get { return _itemMetaData.InventoryGraphicResourceId; }
        }

        /// <inheritdoc />
        public Guid MagicTypeId
        {
            get { return _itemMetaData.MagicTypeId; }
        }

        /// <inheritdoc />
        public Guid MaterialTypeId
        {
            get { return _itemMetaData.MaterialTypeId; }
        }

        /// <inheritdoc />
        public Guid SocketTypeId
        {
            get { return _itemMetaData.SocketTypeId; }
        }

        /// <inheritdoc />
        public Guid ItemTypeId
        {
            get { return _itemMetaData.ItemTypeId; }
        }

        /// <inheritdoc />
        public double Weight
        {
            get
            {
                EnsureStatsCalculated();
                return _stats.GetValueOrDefault(ItemStats.Weight, 0);
            }
        }

        /// <inheritdoc />
        public double Value
        {
            get
            {
                EnsureStatsCalculated();
                return _stats.GetValueOrDefault(ItemStats.Value, 0);
            }
        }

        /// <inheritdoc />
        public int RequiredSockets
        {
            get
            {
                EnsureStatsCalculated();
                return (int)_stats.GetValueOrDefault(ItemStats.RequiredSockets, 0);
            }
        }

        /// <inheritdoc />
        public int CurrentDurability
        {
            get
            {
                EnsureStatsCalculated();
                return (int)_stats.GetValueOrDefault(ItemStats.CurrentDurability, 0);
            }
        }

        /// <inheritdoc />
        public int MaximumDurability
        {
            get
            {
                EnsureStatsCalculated();
                return (int)_stats.GetValueOrDefault(ItemStats.MaximumDurability, 0);
            }
        }

        /// <inheritdoc />
        public IStatCollection Stats
        {
            get
            {
                EnsureStatsCalculated();
                return _stats;
            }
        }

        /// <inheritdoc />
        public IEnumerable<IStat> BaseStats
        {
            get
            {
                return _baseStats;
            }
        }

        /// <inheritdoc />
        public IEnumerable<Guid> EquippableSlotIds
        {
            get { return _equippableSlots; }
        }

        /// <inheritdoc />
        public IEnchantmentCollection Enchantments
        {
            get { return _enchantments; }
        }

        /// <inheritdoc />
        public IItemRequirements Requirements
        {
            get { return _requirements; }
        }

        /// <inheritdoc />
        public IItemCollection SocketedItems
        {
            get { return _socketedItems; }
        }

        /// <inheritdoc />
        public IEnumerable<IItemAffix> Affixes
        {
            get { return _affixes; }
        }
        #endregion

        #region Methods
        public static IItem Create(
            IItemContext context,
            Guid id,
            Guid itemDefinitionId,
            IItemMetaData itemMetaData,
            IEnumerable<IItemNamePart> itemNameParts,
            IItemRequirements itemRequirements,
            IEnumerable<IStat> stats,
            IEnumerable<IEnchantment> enchantments,
            IEnumerable<IItemAffix> affixes,
            IEnumerable<IItem> socketedItems,
            IEnumerable<Guid> equippableSlots)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentNullException>(itemMetaData != null);
            Contract.Requires<ArgumentNullException>(itemNameParts != null);
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Requires<ArgumentNullException>(affixes != null);
            Contract.Requires<ArgumentNullException>(socketedItems != null);
            Contract.Requires<ArgumentNullException>(equippableSlots != null);
            Contract.Ensures(Contract.Result<IItem>() != null);
            
            return new Item(
                context, 
                id,
                itemDefinitionId,
                itemMetaData,
                itemNameParts,
                itemRequirements,
                stats,
                enchantments,
                affixes,
                socketedItems,
                equippableSlots);
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
                GetOpenSocketsForType(item.SocketTypeId) < item.RequiredSockets)
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
            _enchantments.UpdateElapsedTime(elapsedTime);
        }

        public int GetOpenSocketsForType(Guid socketTypeId)
        {
            return CalculateOpenSockets(
                    GetTotalSocketsForType(socketTypeId),
                    _socketedItems,
                    socketTypeId);
        }

        public int GetTotalSocketsForType(Guid socketTypeId)
        {
            EnsureStatsCalculated();
            var statSocketType = _context.StatSocketTypeRepository.GetBySocketTypeId(socketTypeId);
            return (int)_stats.GetValueOrDefault(statSocketType.StatDefinitionId, 0);
        }

        public override string ToString()
        {
            return $"Id: {Id}, Item Name Parts: {string.Join(", ", ItemNameParts.OrderBy(x => x.Order).Select(x => x.Id.ToString()).ToArray())}";
        }

        private void EnsureStatsCalculated()
        {
            Contract.Ensures(_stats != null);
            Contract.Ensures(!_statsDirty);
            if (!_statsDirty)
            {
                return;
            }

            var currentDurability = _stats == null 
                ? 0 
                : (int)_stats.GetValueOrDefault(ItemStats.CurrentDurability, 0);
            var maximumDurability = _stats == null 
                ? 0 
                : (int)_stats.GetValueOrDefault(ItemStats.MaximumDurability, 0);

            var itemOnlyEnchantments = _enchantments.TriggeredBy(EnchantmentTriggers.Item).ToArray();
            var result = _context.EnchantmentCalculator.Calculate(
                _baseStats,
                itemOnlyEnchantments);

            // we need to keep the enchantment change event from triggering
            try
            {
                _enchantments.CollectionChanged -= Enchantments_CollectionChanged;

                var newEnchantments = result.Enchantments.ToArray();
                var addedEnchantments = newEnchantments.Except(_enchantments);
                var removedEnchantments = _enchantments.Except(newEnchantments);
                var enchantmentsDiffered = addedEnchantments.Any() || removedEnchantments.Any();
                var otherEnchantments = _enchantments.Except(itemOnlyEnchantments).ToArray();

                _enchantments.Clear();
                _enchantments.Add(otherEnchantments);
                _enchantments.Add(newEnchantments);

                if (enchantmentsDiffered)
                {
                    OnEnchantmentsChanged(new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Reset,
                        addedEnchantments.ToArray(),
                        removedEnchantments.ToArray()));
                }
            }
            finally 
            {
                _enchantments.CollectionChanged += Enchantments_CollectionChanged;
            }

            _stats = StatCollection.Create(result.Stats);
            _stats.Set(Stat.Create(
                Guid.NewGuid(), 
                ItemStats.Weight, 
                CalculateWeight(_stats, _socketedItems)));
            _stats.Set(Stat.Create(
                Guid.NewGuid(), 
                ItemStats.Value, 
                CalculateValue(_stats)));

            foreach (var statSocketType in _context.StatSocketTypeRepository.GetAll())
            {
                _stats.Set(Stat.Create(
                    Guid.NewGuid(), 
                    statSocketType.StatDefinitionId,
                    CalculateTotalSockets(_stats, statSocketType)));                
            }
            
            EnsureDurabilityInRange(_stats);

            if (currentDurability != (int)_stats.GetValueOrDefault(ItemStats.CurrentDurability, 0) ||
                maximumDurability != (int)_stats.GetValueOrDefault(ItemStats.MaximumDurability, 0))
            {
                OnDurabilityChanged();
            }

            _statsDirty = false;
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
                Guid.NewGuid(), 
                ItemStats.MaximumDurability,
                Math.Max(0, stats.GetValueOrDefault(ItemStats.MaximumDurability, 0))));
            stats.Set(Stat.Create(
                Guid.NewGuid(), 
                ItemStats.CurrentDurability,
                Math.Max(0,
                    Math.Min(
                        stats.GetValueOrDefault(ItemStats.CurrentDurability, 0),
                        stats.GetValueOrDefault(ItemStats.MaximumDurability, 0)))));
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

        private int CalculateTotalSockets(IStatCollection stats, IStatSocketType statSocketType)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Ensures(Contract.Result<int>() >= 0);
            return (int)stats.GetValueOrDefault(statSocketType.StatDefinitionId, 0);
        }

        private int CalculateOpenSockets(int totalSockets, IEnumerable<IItem> socketedItems, Guid socketTypeId)
        {
            Contract.Requires<ArgumentOutOfRangeException>(totalSockets >= 0);
            Contract.Requires<ArgumentNullException>(socketedItems != null);
            Contract.Requires<ArgumentException>(socketTypeId != Guid.Empty);
            Contract.Ensures(Contract.Result<int>() >= 0);
            return Math.Max(0, totalSockets - socketedItems.Where(x => x.SocketTypeId == socketTypeId).TotalRequiredSockets());
        }

        private void OnDurabilityChanged()
        {
            var handler = DurabilityChanged;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnEnchantmentsChanged(NotifyCollectionChangedEventArgs e)
        {
            var handler = EnchantmentsChanged;
            if (handler != null)
            {
                handler.Invoke(this, e);
            }
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
            OnEnchantmentsChanged(e);
        }
        #endregion
    }
}
