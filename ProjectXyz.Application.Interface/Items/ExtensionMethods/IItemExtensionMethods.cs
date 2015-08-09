using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Interface.Items.ExtensionMethods
{
    public static class IItemExtensionMethods
    {
        #region Methods
        public static bool IsWeightless(this IItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            return item.Weight == 0;
        }
        
        public static void Enchant(this IItem item, IEnchantment enchantment)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            Contract.Requires<ArgumentNullException>(enchantment != null);
            item.Enchant(new[] { enchantment });
        }

        public static void Disenchant(this IItem item, IEnchantment enchantment)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            Contract.Requires<ArgumentNullException>(enchantment != null);
            item.Disenchant(new[] { enchantment });
        }

        public static bool IsEquipped(this IItem item, IEquipment equipment)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            Contract.Requires<ArgumentNullException>(equipment != null);
            return equipment.HasItemEquipped(item);
        }
        #endregion
    }
}
