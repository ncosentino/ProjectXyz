using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        #endregion
    }
}
