using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.EquipSlots
{
    public interface IItemTypeEquipSlotTypeRepository
    {
        #region Methods
        IItemTypeEquipSlotType Add(
            Guid id,
            Guid itemTypeId,
            Guid equipSlotTypeId);

        void RemoveById(Guid id);

        IItemTypeEquipSlotType GetById(Guid id);

        IEnumerable<IItemTypeEquipSlotType> GetByItemTypeId(Guid itemTypeId);

        IEnumerable<IItemTypeEquipSlotType> GetAll();
        #endregion
    }
}
