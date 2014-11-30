using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public static class IEnchantmentStoreCollectionExtensionMethods
    {
        #region Methods
        public static IEnumerable<IEnchantmentStore> CalculatedBy(this IEnchantmentStoreCollection enchantments, Guid calculationId)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Ensures(Contract.Result<IEnumerable<IEnchantmentStore>>() != null);

            return enchantments.Where(x => x.CalculationId == calculationId);
        }
        #endregion
    }
}
