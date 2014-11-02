using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Interface.Enchantments;

namespace ProjectXyz.Interface.Items.ExtensionMethods
{
    public static class IItemCollectionExtensionMethods
    {
        #region Methods
        public static double TotalWeight(this IEnumerable<IItem> items)
        {
            Contract.Requires<ArgumentNullException>(items != null);
            return items.Sum(x => x.Weight);
        }
        #endregion
    }
}
