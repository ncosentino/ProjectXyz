using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public static class IEnchantmentCollectionExtensionMethods
    {
        #region Methods
        public static IEnumerable<IEnchantment> CalculatedBy(this IEnchantmentCollection enchantments, Guid calculationId)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);

            return enchantments.Where(x => x.CalculationId == calculationId);
        }
        #endregion
    }
}
