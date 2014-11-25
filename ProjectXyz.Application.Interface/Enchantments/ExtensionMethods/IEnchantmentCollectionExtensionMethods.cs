using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public static class IEnchantmentCollectionExtensionMethods
    {
        #region Methods
        public static IEnumerable<IEnchantment> CalculatedBy(this IEnumerable<IEnchantment> enchantments, Guid calculationId)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Ensures(Contract.Result<IEnumerable<IEnchantment>>() != null);

            return enchantments.Where(x => x.CalculationId == calculationId);
        }

        public static IEnumerable<IEnchantment> TriggeredBy(this IEnumerable<IEnchantment> enchantments, Guid triggerId)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Requires<ArgumentNullException>(triggerId != null);
            Contract.Ensures(Contract.Result<IEnumerable<IEnchantment>>() != null);

            return enchantments.Where(x => x.TriggerId == triggerId);
        }
        #endregion
    }
}
