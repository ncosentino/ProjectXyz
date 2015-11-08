using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

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
                inventory.Count > inventory.ItemCapacity;
        }

        public static bool IsFull(this IInventory inventory)
        {
            Contract.Requires<ArgumentNullException>(inventory != null);
            return
                inventory.CurrentWeight >= inventory.WeightCapacity ||
                inventory.Count >= inventory.ItemCapacity;
        }

        public static bool TryGetFirstUsedSlot(this IInventory inventory, out int slot)
        {
            Contract.Requires<ArgumentNullException>(inventory != null);

            var slots = new List<int>(inventory.UsedSlots);
            if (slots.Count < 1)
            {
                slot = -1;
                return false;
            }

            slots.Sort();
            slot = slots[0];
            return true;
        }

        public static bool TryGetLastUsedSlot(this IInventory inventory, out int slot)
        {
            Contract.Requires<ArgumentNullException>(inventory != null);

            var slots = new List<int>(inventory.UsedSlots);
            if (slots.Count < 1)
            {
                slot = -1;
                return false;
            }

            slots.Sort();
            slot = slots[slots.Count - 1];
            return true;
        }

        public static bool CanAddItem(this IInventory inventory, int slot, IItem item)
        {
            Contract.Requires<ArgumentNullException>(inventory != null);
            Contract.Requires<ArgumentNullException>(item != null);

            return
                !inventory.UsedSlots.Contains(slot) &&
                !inventory.IsFull() &&
                inventory.TotalWeight() + item.Weight <= inventory.WeightCapacity;
        }
        #endregion
    }
}
