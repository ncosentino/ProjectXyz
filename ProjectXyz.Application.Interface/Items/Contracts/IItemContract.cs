using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items.Affixes;
using ProjectXyz.Application.Interface.Items.Requirements;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(IItem))]
    public abstract class IItemContract : IItem
    {
        #region Events
        public abstract event EventHandler<EventArgs> DurabilityChanged;

        public abstract event NotifyCollectionChangedEventHandler EnchantmentsChanged;
        #endregion

        #region Properties
        public IStatCollection Stats
        {
            get
            {
                Contract.Ensures(Contract.Result<IStatCollection>() != null);
                return default(IStatCollection);
            }
        }

        public IEnumerable<IItemNamePart> ItemNameParts
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<IItemNamePart>>() != null);
                return default(IEnumerable<IItemNamePart>);
            }
        }

        public IEnumerable<Guid> EquippableSlotIds
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<Guid>>() != null);
                return default(IEnumerable<Guid>);
            }
        }

        public IEnumerable<IItemAffix> Affixes
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<IItemAffix>>() != null);
                return default(IEnumerable<IItemAffix>);
            }
        }

        public double Weight
        {
            get
            {
                Contract.Ensures(Contract.Result<double>() >= 0);
                return default(double);
            }
        }

        public double Value
        {
            get
            {
                Contract.Ensures(Contract.Result<double>() >= 0);
                return default(double);
            }
        }

        public IEnchantmentCollection Enchantments
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnchantmentCollection>() != null);
                return default(IEnchantmentCollection);
            }
        }

        public IItemRequirements Requirements
        {
            get
            {
                Contract.Ensures(Contract.Result<IItemRequirements>() != null);
                return default(IItemRequirements);
            }
        }

        public Guid ItemDefinitionId
        {
            get
            {
                Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);
                return default(Guid);
            }
        }

        public abstract int MaximumDurability { get; }

        public abstract int CurrentDurability { get; }

        public abstract IItemCollection SocketedItems { get; }

        public abstract Guid Id { get; }

        public abstract int RequiredSockets { get; }

        public abstract Guid NameStringResourceId { get; }

        public abstract Guid InventoryGraphicResourceId { get; }

        public abstract Guid MagicTypeId { get; }

        public abstract Guid ItemTypeId { get; }

        public abstract Guid MaterialTypeId { get; }

        public abstract Guid SocketTypeId { get; }
        #endregion

        #region Methods
        public void Enchant(IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
        }

        public void Disenchant(IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
        }

        public abstract bool Socket(IItem item);

        public abstract int GetOpenSocketsForType(Guid socketTypeId);

        public abstract int GetTotalSocketsForType(Guid socketTypeId);

        public abstract void UpdateElapsedTime(TimeSpan elapsedTime);
        #endregion
    }
}
