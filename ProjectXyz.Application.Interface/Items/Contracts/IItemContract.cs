using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(IItem))]
    public abstract class IItemContract : IItem
    {
        #region Events
        public abstract event EventHandler<EventArgs> DurabilityChanged;
        #endregion

        #region Properties
        public string Name
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                Contract.Ensures(Contract.Result<string>().Trim().Length > 0);
                return default(string);
            }
        }

        public string InventoryGraphicResource
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                Contract.Ensures(Contract.Result<string>().Trim().Length > 0);
                return default(string);
            }
        }

        public Guid MagicTypeId
        {
            get
            {
                return default(Guid);
            }
        }

        public string ItemType
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                Contract.Ensures(Contract.Result<string>().Trim().Length > 0);
                return default(string);
            }
        }

        public IStatCollection Stats
        {
            get
            {
                Contract.Ensures(Contract.Result<IStatCollection>() != null);
                return default(IStatCollection);
            }
        }

        public IEnumerable<string> EquippableSlots
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
                return default(IEnumerable<string>);
            }
        }

        public Guid MaterialTypeId
        {
            get
            {
                Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);
                return default(Guid);
            }
        }

        public Guid SocketTypeId
        {
            get
            {
                Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);
                return default(Guid);
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

        public IObservableEnchantmentCollection Enchantments
        {
            get
            {
                Contract.Ensures(Contract.Result<IObservableEnchantmentCollection>() != null);
                return default(IObservableEnchantmentCollection);
            }
        }

        public IRequirements Requirements
        {
            get
            {
                Contract.Ensures(Contract.Result<IRequirements>() != null);
                return default(IRequirements);
            }
        }

        public abstract int MaximumDurability { get; }

        public abstract int CurrentDurability { get; }

        public abstract IItemCollection SocketedItems { get; }

        public abstract Guid Id { get; }

        public abstract int RequiredSockets { get; }
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
