using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.EquipSlots
{
    public interface IItemTypeEquipSlotType
    {
        #region Properties
        Guid Id { get; }

        Guid ItemTypeId { get; }

        Guid EquipSlotTypeId { get; }
        #endregion
    }
}
