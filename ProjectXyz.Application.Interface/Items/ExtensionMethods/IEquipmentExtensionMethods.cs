using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        #endregion
    }
}
