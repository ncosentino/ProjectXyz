using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Core.Enchantments
{
    public class ReadonlyEnchantmentCollection : 
        EnchantmentCollection, 
        IReadonlyEnchantmentCollection
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
        public static IReadonlyEnchantmentCollection CreateEmpty()
        {
            Contract.Ensures(Contract.Result<IReadonlyEnchantmentCollection>() != null);
            return new ReadonlyEnchantmentCollection();
        }

        public static IReadonlyEnchantmentCollection CreateCopy(IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Ensures(Contract.Result<IReadonlyEnchantmentCollection>() != null);
            return new ReadonlyEnchantmentCollection(enchantments);
        }
        #endregion
    }
}
