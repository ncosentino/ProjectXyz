using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Enchantments.ExtensionMethods
{
    public static class IEnchantmentStoreExtensionMethods
    {
        #region Methods
        public static bool HasInfiniteDuration(this IEnchantmentStore enchantment)
        {
            Contract.Requires<ArgumentNullException>(enchantment != null);
            return enchantment.RemainingDuration == TimeSpan.MinValue;
        }

        public static bool IsExpired(this IEnchantmentStore enchantment)
        {
            Contract.Requires<ArgumentNullException>(enchantment != null);
            return enchantment.RemainingDuration == TimeSpan.Zero;
        }
        #endregion
    }
}
