using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface.Items.Materials;

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
                Contract.Ensures(Contract.Result<string>() != null);
                Contract.Ensures(Contract.Result<string>() != string.Empty);
                return default(string);
            }
        }

        public string MagicType
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                Contract.Ensures(Contract.Result<string>() != string.Empty);
                return default(string);
            }
        }

        public string ItemType
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                Contract.Ensures(Contract.Result<string>() != string.Empty);
                return default(string);
            }
        }

        public IMaterial Material
        {
            get
            {
                Contract.Ensures(Contract.Result<IMaterial>() != null);
                return default(IMaterial);
            }
        }

        public IDurability Durability
        {
            get
            {
                Contract.Ensures(Contract.Result<IDurability>() != null);
                return default(IDurability);
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

        public IRequirements Requirements
        {
            get
            {
                Contract.Ensures(Contract.Result<IRequirements>() != null);
                return default(IRequirements);
            }
        }

        public abstract int TotalSockets { get; }

        public abstract int OpenSockets { get; }

        public abstract IItemCollection SocketedItems { get; }

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
