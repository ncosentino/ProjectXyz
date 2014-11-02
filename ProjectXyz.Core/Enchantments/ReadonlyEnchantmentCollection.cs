using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Interface.Enchantments;

namespace ProjectXyz.Core.Enchantments
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
        }
        #endregion

        #region Methods
        public static IReadonlyEnchantmentCollection CreateEmpty()
        {
            return new ReadonlyEnchantmentCollection();
        }

        public static IReadonlyEnchantmentCollection CreateCopy(IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            return new ReadonlyEnchantmentCollection(enchantments);
        }
        #endregion
    }
}
