using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
{
    public class ReadonlyEnchantmentCollection : 
        EnchantmentCollection, 
        IEnchantmentCollection
    {
        #region Constructors
        protected ReadonlyEnchantmentCollection()
            : this(new IEnchantment[0])
        {
        }

        protected ReadonlyEnchantmentCollection(IEnumerable<IEnchantment> enchantments)
            : base(enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
        }
        #endregion

        #region Methods
        public static IEnchantmentCollection CreateEmpty()
        {
            Contract.Ensures(Contract.Result<IEnchantmentCollection>() != null);
            return new ReadonlyEnchantmentCollection();
        }

        public static IEnchantmentCollection CreateCopy(IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Ensures(Contract.Result<IEnchantmentCollection>() != null);
            return new ReadonlyEnchantmentCollection(enchantments);
        }
        #endregion
    }
}
