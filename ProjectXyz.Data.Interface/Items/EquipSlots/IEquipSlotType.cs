using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.EquipSlots
{
    public interface IEquipSlotType
    {
        #region Properties
        Guid Id { get; }

        Guid NameStringResourceId { get; }
        #endregion
    }
}
