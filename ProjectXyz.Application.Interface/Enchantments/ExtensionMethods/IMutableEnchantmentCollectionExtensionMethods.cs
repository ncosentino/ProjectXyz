using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public static class IMutableEnchantmentCollectionExtensionMethods
    {
        #region Methods
        public static void Add(this IMutableEnchantmentCollection enchantments, IEnchantment enchantment)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Requires<ArgumentNullException>(enchantment != null);

            enchantments.Add(new IEnchantment[] { enchantment });
        }

        public static bool Remove(this IMutableEnchantmentCollection enchantments, IEnchantment enchantment)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Requires<ArgumentNullException>(enchantment != null);

            return enchantments.Remove(new IEnchantment[] { enchantment });
        }
        #endregion
    }
}
