using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Application.Interface.Enchantments.ExtensionMethods
{
    public static class IEnchantmentCollectionExtensionMethods
    {
        #region Methods
        public static IEnumerable<IEnchantment> TriggeredBy(this IEnumerable<IEnchantment> enchantments, Guid triggerId)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Ensures(Contract.Result<IEnumerable<IEnchantment>>() != null);

            return enchantments.Where(x => x.TriggerId == triggerId);
        }
        #endregion
    }
}
