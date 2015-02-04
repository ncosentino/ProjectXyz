using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Items.ExtensionMethods
{
    public static class IEquipmentExtensionMethods
    {
        #region Methods
        public static bool HasItemEquipped(this IEquipment equipment, IItem item)
        {
            Contract.Requires<ArgumentNullException>(equipment != null);
            Contract.Requires<ArgumentNullException>(item != null);
            return equipment.Contains(item);
        }

        public static bool TryGetEquippedSlot(this IEquipment equipment, IItem item, out string slot)
        {
            Contract.Requires<ArgumentNullException>(equipment != null);
            Contract.Requires<ArgumentNullException>(item != null);
            slot = item.EquippableSlots.FirstOrDefault(s => equipment[s] == item);
            return !string.IsNullOrEmpty(slot);
        }

        public static string GetEquippedSlot(this IEquipment equipment, IItem item)
        {
            Contract.Requires<ArgumentNullException>(equipment != null);
            Contract.Requires<ArgumentNullException>(item != null);

            string slot;
            if (!TryGetEquippedSlot(equipment, item, out slot))
            {
                throw new ArgumentException("The item was not contained within the equipment.", "item");
            }

            return slot;
        }

        public static bool TryGetFirstOpenSlot(this IEquipment equipment, IItem item, out string slot)
        {
            Contract.Requires<ArgumentNullException>(equipment != null);
            Contract.Requires<ArgumentNullException>(item != null);
            return TryGetFirstOpenSlot(equipment, item.EquippableSlots, out slot);
        }

        public static bool TryGetFirstOpenSlot(this IEquipment equipment, IEnumerable<string> slots, out string slot)
        {
            Contract.Requires<ArgumentNullException>(equipment != null);
            Contract.Requires<ArgumentNullException>(slots != null);
            slot = slots.FirstOrDefault(s => equipment[s] == null);
            return !string.IsNullOrEmpty(slot);
        }
        #endregion
    }
}
