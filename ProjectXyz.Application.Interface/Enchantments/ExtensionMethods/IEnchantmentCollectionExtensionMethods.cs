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
        public static IEnumerable<IEnchantment> CalculatedBy(this IEnumerable<IEnchantment> enchantments, string calculationId)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Requires<ArgumentNullException>(calculationId != null);
            Contract.Requires<ArgumentException>(calculationId != string.Empty);
            Contract.Ensures(Contract.Result<IEnumerable<IEnchantment>>() != null);

            return enchantments.Where(x => x.CalculationId == calculationId);
        }

        public static IEnumerable<IEnchantment> TriggeredBy(this IEnumerable<IEnchantment> enchantments, string trigger)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Requires<ArgumentNullException>(trigger != null);
            Contract.Requires<ArgumentException>(trigger != string.Empty);
            Contract.Ensures(Contract.Result<IEnumerable<IEnchantment>>() != null);

            return enchantments.Where(x => x.Trigger == trigger);
        }
        #endregion
    }
}
