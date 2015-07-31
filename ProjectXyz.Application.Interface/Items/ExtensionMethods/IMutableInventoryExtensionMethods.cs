using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Application.Interface.Items.ExtensionMethods
{
    public static class IMutableInventoryExtensionMethods
    {
        #region Methods
        public static void Add(this IMutableInventory inventory, IItem item)
        {
            Contract.Requires<ArgumentNullException>(inventory != null);
            Contract.Requires<ArgumentNullException>(item != null);

            inventory.Add(new[] { item });
        }

        public static bool Remove(this IMutableInventory inventory, IItem item)
        {
            Contract.Requires<ArgumentNullException>(inventory != null);
            Contract.Requires<ArgumentNullException>(item != null);

            return inventory.Remove(new[] { item });
        }
        #endregion
    }
}
