using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Interface.Enchantments;
using ProjectXyz.Interface.Items.ExtensionMethods;

namespace ProjectXyz.Interface.Items
{
    public static class IItemExtensionMethods
    {
        #region Methods
        public static bool IsWeightless(this IItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            return item.Weight == 0;
        }

        public static bool IsIndestructible(this IItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            return item.Durability.IsIndestructible();
        }

        public static bool IsBroken(this IItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            return item.Durability.IsBroken();
        }

        public static void Enchant(this IItem item, IEnchantment enchantment)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            Contract.Requires<ArgumentNullException>(enchantment != null);
            item.Enchant(new IEnchantment[] { enchantment });
        }

        public static void Disenchant(this IItem item, IEnchantment enchantment)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            Contract.Requires<ArgumentNullException>(enchantment != null);
            item.Disenchant(new IEnchantment[] { enchantment });
        }
        #endregion
    }
}
