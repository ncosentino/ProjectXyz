using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.EquipSlots
{
    public interface IItemTypeEquipSlotTypeFactory
    {
        #region Methods
        IItemTypeEquipSlotType Create(
            Guid id,
            Guid itemTypeId,
            Guid equipSlotTypeId);
        #endregion
    }
}
