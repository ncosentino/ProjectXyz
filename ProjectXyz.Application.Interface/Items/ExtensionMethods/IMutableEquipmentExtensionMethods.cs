using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Application.Interface.Items.ExtensionMethods
{
    public static class IMutableEquipmentExtensionMethods
    {
        #region Methods
        public static bool EquipToFirstOpenSlot(this IMutableEquipment equipment, IItem item)
        {
            Contract.Requires<ArgumentNullException>(equipment != null);
            Contract.Requires<ArgumentNullException>(item != null);
            
            string slot;
            if (!equipment.TryGetFirstOpenSlot(item, out slot))
            {
                return false;
            }

            return equipment.Equip(item, slot);
        }
        #endregion
    }
}
