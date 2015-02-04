using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Interface.Items.ExtensionMethods
{
    public static class IMutableInventoryExtensionMethods
    {
        #region Methods
        public static void Add(this IMutableInventory inventory, IItem item)
        {
            Contract.Requires<ArgumentNullException>(inventory != null);
            Contract.Requires<ArgumentNullException>(item != null);

            inventory.Add(new IItem[] { item });
        }

        public static bool Remove(this IMutableInventory inventory, IItem item)
        {
            Contract.Requires<ArgumentNullException>(inventory != null);
            Contract.Requires<ArgumentNullException>(item != null);

            return inventory.Remove(new IItem[] { item });
        }
        #endregion
    }
}
