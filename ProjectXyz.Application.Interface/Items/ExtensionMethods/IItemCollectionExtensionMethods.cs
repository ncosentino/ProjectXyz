using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Interface.Items.ExtensionMethods
{
    public static class IItemCollectionExtensionMethods
    {
        #region Methods
        public static double TotalWeight(this IEnumerable<IItem> items)
        {
            Contract.Requires<ArgumentNullException>(items != null);
            return items.Sum(x => x.Weight);
        }

        public static int TotalRequiredSockets(this IEnumerable<IItem> items)
        {
            Contract.Requires<ArgumentNullException>(items != null);
            return items.Sum(x => x.RequiredSockets);
        }

        public static IEnumerable<IEnchantment> AllEnchantments(this IEnumerable<IItem> items)
        {
            Contract.Requires<ArgumentNullException>(items != null);
            Contract.Ensures(Contract.Result<IEnumerable<IEnchantment>>() != null);

            foreach (var item in items)
            {
                foreach (var enchantment in item.Enchantments)
                {
                    yield return enchantment;
                }
            }
        }
        #endregion
    }
}
