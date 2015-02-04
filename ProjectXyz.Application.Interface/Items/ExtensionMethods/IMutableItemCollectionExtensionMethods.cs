using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Interface.Items.ExtensionMethods
{
    public static class IMutableItemCollectionExtensionMethods
    {
        #region Methods
        public static void Add(this IMutableItemCollection items, IItem item)
        {
            Contract.Requires<ArgumentNullException>(items != null);
            Contract.Requires<ArgumentNullException>(item != null);

            items.Add(new IItem[] { item });
        }

        public static bool Remove(this IMutableItemCollection items, IItem item)
        {
            Contract.Requires<ArgumentNullException>(items != null);
            Contract.Requires<ArgumentNullException>(item != null);

            return items.Remove(new IItem[] { item });
        }
        #endregion
    }
}
