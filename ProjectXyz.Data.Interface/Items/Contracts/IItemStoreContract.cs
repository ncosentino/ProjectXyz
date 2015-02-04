using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Interface.Items.Contracts
{
    [ContractClassFor(typeof(IItemStore))]
    public abstract class IItemStoreContract : IItemStore
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

            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(value != string.Empty);
            }
        }

        public Guid MagicTypeId
        {
            get
            {
                return default(Guid);
            }

            set
            {
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

            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(value != string.Empty);
            }
        }

        public string MaterialType
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                Contract.Ensures(Contract.Result<string>() != string.Empty);
                return default(string);
            }

            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(value != string.Empty);
            }
        }

        public IMutableStatCollection Stats
        {
            get
            {
                Contract.Ensures(Contract.Result<IMutableStatCollection>() != null);
                return default(IMutableStatCollection);
            }
        }
        
        public IMutableEnchantmentCollection Enchantments
        {
            get
            {
                Contract.Ensures(Contract.Result<IMutableEnchantmentCollection>() != null);
                return default(IMutableEnchantmentCollection);
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

        public IMutableItemStoreCollection SocketedItems
        {
            get
            {
                Contract.Ensures(Contract.Result<IMutableItemStoreCollection>() != null);
                return default(IMutableItemStoreCollection);
            }
        }

        public IList<string> EquippableSlots
        {
            get
            {
                Contract.Ensures(Contract.Result<IList<string>>() != null);
                return default(IList<string>);
            }
        }

        public abstract Guid Id { get; set; }
        #endregion
    }
}
