using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.EquipSlots
{
    public interface IEquipSlotTypeRepository
    {
        #region Methods
        IEquipSlotType Add(
            Guid id,
            Guid nameStringResourceId);

        void RemoveById(Guid id);

        IEquipSlotType GetById(Guid id);

        IEnumerable<IEquipSlotType> GetAll();
        #endregion
    }
}
