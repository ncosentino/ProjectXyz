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
        public static IEnumerable<IEnchantment> EnchantmentsCalculatedBy(this IEnchantmentCollection enchantments, string calculationId)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Requires<ArgumentNullException>(calculationId != null);
            Contract.Requires<ArgumentException>(calculationId != string.Empty);

            foreach (var enchantment in enchantments)
            {
                if (enchantment.CalculationId == calculationId)
                {
                    yield return enchantment;
                }
            }
        }
        #endregion
    }
}
