using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Interface.Items.ExtensionMethods
{
    public static class IInventoryExtensionMethods
    {
        #region Methods
        public static bool IsOverburderned(this IInventory inventory)
        {
            Contract.Requires<ArgumentNullException>(inventory != null);
            return
                inventory.CurrentWeight > inventory.WeightCapacity ||
                inventory.Items.Count > inventory.ItemCapacity;
        }

        public static bool IsFull(this IInventory inventory)
        {
            Contract.Requires<ArgumentNullException>(inventory != null);
            return
                inventory.CurrentWeight >= inventory.WeightCapacity ||
                inventory.Items.Count >= inventory.ItemCapacity;
        }
        #endregion
    }
}
