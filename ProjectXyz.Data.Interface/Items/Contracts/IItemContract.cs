using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Interface.Items.Contracts
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

            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(value != string.Empty);
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

            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(value != string.Empty);
            }
        }

        public IMutableStatCollection<IMutableStat> Stats
        {
            get
            {
                Contract.Requires<ArgumentNullException>(Stats != null);
                return default(IMutableStatCollection<IMutableStat>);
            }
        }
        
        public IMutableEnchantmentCollection Enchantments
        {
            get
            {
                Contract.Requires<ArgumentNullException>(Enchantments != null);
                return default(IMutableEnchantmentCollection);
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

        public IMutableItemCollection SocketedItems
        {
            get
            {
                Contract.Requires<ArgumentNullException>(SocketedItems != null);
                return default(IMutableItemCollection);
            }
        }

        public abstract Guid Id { get; }
        #endregion
    }
}
