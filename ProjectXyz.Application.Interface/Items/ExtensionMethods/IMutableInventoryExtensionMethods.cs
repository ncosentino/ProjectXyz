using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Interface.Items.ExtensionMethods
{
    public static class IMutableInventoryExtensionMethods
    {
        #region Methods
        public static void AddItem(this IMutableInventory inventory, IItem item)
        {
            Contract.Requires<ArgumentNullException>(inventory != null);
            Contract.Requires<ArgumentNullException>(item != null);

            inventory.AddItems(new IItem[] { item });
        }

        public static void RemoveItem(this IMutableInventory inventory, IItem item)
        {
            Contract.Requires<ArgumentNullException>(inventory != null);
            Contract.Requires<ArgumentNullException>(item != null);

            inventory.RemoveItems(new IItem[] { item });
        }
        #endregion
    }
}
