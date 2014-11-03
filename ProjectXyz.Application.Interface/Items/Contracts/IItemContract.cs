using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(IItem))]
    public abstract class IItemContract : IItem
    {
        #region Properties
        public string Name
        {
            get
            {
                Contract.Requires<ArgumentNullException>(Name != null);
                Contract.Requires<ArgumentException>(Name != string.Empty);
                return default(string);
            }
        }

        public string MagicType
        {
            get
            {
                Contract.Requires<ArgumentNullException>(MagicType != null);
                Contract.Requires<ArgumentException>(MagicType != string.Empty);
                return default(string);
            }
        }

        public string ItemType
        {
            get
            {
                Contract.Requires<ArgumentNullException>(ItemType != null);
                Contract.Requires<ArgumentException>(ItemType != string.Empty);
                return default(string);
            }
        }

        public string MaterialType
        {
            get
            {
                Contract.Requires<ArgumentNullException>(MaterialType != null);
                Contract.Requires<ArgumentException>(MaterialType != string.Empty);
                return default(string);
            }
        }

        public IDurability Durability
        {
            get
            {
                Contract.Requires<ArgumentNullException>(Durability != null);
                return default(IDurability);
            }
        }

        public double Weight
        {
            get
            {
                Contract.Requires<ArgumentOutOfRangeException>(Weight >= 0);
                return default(double);
            }
        }

        public double Value
        {
            get
            {
                Contract.Requires<ArgumentOutOfRangeException>(Value >= 0);
                return default(double);
            }
        }

        public IEnchantmentCollection Enchantments
        {
            get
            {
                Contract.Requires<ArgumentNullException>(Enchantments != null);
                return default(IEnchantmentCollection);
            }
        }

        public IRequirements Requirements
        {
            get
            {
                Contract.Requires<ArgumentNullException>(Requirements != null);
                return default(IRequirements);
            }
        }

        public abstract int TotalSockets { get; }

        public abstract int OpenSockets { get; }

        public abstract IReadonlyItemCollection SocketedItems { get; }

        public abstract Guid Id { get; }

        public abstract int RequiredSockets { get; }
        #endregion

        #region Methods
        public double GetStat(string statId)
        {
            Contract.Requires<ArgumentNullException>(statId != null);
            Contract.Requires<ArgumentException>(statId != string.Empty);
            return default(double);
        }

        public void Enchant(IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
        }

        public void Disenchant(IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
        }

        public abstract bool Socket(IItem item);

        public abstract void UpdateElapsedTime(TimeSpan elapsedTime);
        #endregion
    }
}
