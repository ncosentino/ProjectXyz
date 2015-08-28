using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using ProjectXyz.Application.Interface.Actors.Contracts;
using ProjectXyz.Application.Interface.Items.Contracts;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(ICanEquipItemContract))]
    public interface ICanEquip
    {
        #region Methods
        bool CanEquip(IItem item, Guid slotId);

        void Equip(IItem item, Guid slotId);
        #endregion
    }
}
