using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.EquipSlots
{
    public interface IItemTypeEquipSlotTypeRepository
    {
        #region Methods
        void Add(IItemTypeEquipSlotType itemStore);

        void RemoveById(Guid id);

        IItemTypeEquipSlotType GetById(Guid id);

        IItemTypeEquipSlotType GetByItemTypeId(Guid itemTypeId);

        IEnumerable<IItemTypeEquipSlotType> GetAll();
        #endregion
    }
}
