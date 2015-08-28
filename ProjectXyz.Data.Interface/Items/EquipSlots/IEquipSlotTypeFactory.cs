using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.EquipSlots
{
    public interface IEquipSlotTypeFactory
    {
        #region Methods
        IEquipSlotType Create(
            Guid id,
            Guid nameStringResourceId);
        #endregion
    }
}
