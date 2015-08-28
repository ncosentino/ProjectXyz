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

        public static bool TryGetEquippedSlot(this IEquipment equipment, IItem item, out Guid slotId)
        {
            Contract.Requires<ArgumentNullException>(equipment != null);
            Contract.Requires<ArgumentNullException>(item != null);
            slotId = item.EquippableSlotIds.FirstOrDefault(s => equipment[s] == item);
            return slotId != Guid.Empty;
        }

        public static Guid GetEquippedSlot(this IEquipment equipment, IItem item)
        {
            Contract.Requires<ArgumentNullException>(equipment != null);
            Contract.Requires<ArgumentNullException>(item != null);

            Guid slotId;
            if (!TryGetEquippedSlot(equipment, item, out slotId))
            {
                throw new ArgumentException("The item was not contained within the equipment.", "item");
            }

            return slotId;
        }

        public static bool TryGetFirstOpenSlot(this IEquipment equipment, IItem item, out Guid slotId)
        {
            Contract.Requires<ArgumentNullException>(equipment != null);
            Contract.Requires<ArgumentNullException>(item != null);
            return TryGetFirstOpenSlot(equipment, item.EquippableSlotIds, out slotId);
        }

        public static bool TryGetFirstOpenSlot(this IEquipment equipment, IEnumerable<Guid> slots, out Guid slotId)
        {
            Contract.Requires<ArgumentNullException>(equipment != null);
            Contract.Requires<ArgumentNullException>(slots != null);
            slotId = slots.FirstOrDefault(s => equipment[s] == null);
            return slotId != Guid.Empty;
        }
        #endregion
    }
}
