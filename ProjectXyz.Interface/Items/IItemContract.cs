using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Interface.Enchantments;

namespace ProjectXyz.Interface.Items
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

        public IInventory Sockets
        {
            get
            {
                Contract.Requires<ArgumentNullException>(Sockets != null);
                return default(IInventory);
            }
        }

        public abstract Guid Id { get; }
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

        public abstract void UpdateElapsedTime(TimeSpan elapsedTime);
        #endregion
    }
}
