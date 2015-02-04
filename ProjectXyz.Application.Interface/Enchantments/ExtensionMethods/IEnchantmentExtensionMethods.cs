using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Enchantments.ExtensionMethods
{
    public static class IEnchantmentExtensionMethods
    {
        #region Methods
        public static bool HasInfiniteDuration(this IEnchantment enchantment)
        {
            Contract.Requires<ArgumentNullException>(enchantment != null);
            return enchantment.RemainingDuration == TimeSpan.MinValue;
        }

        public static bool IsExpired(this IEnchantment enchantment)
        {
            Contract.Requires<ArgumentNullException>(enchantment != null);
            return enchantment.RemainingDuration == TimeSpan.Zero;
        }
        #endregion
    }
}
