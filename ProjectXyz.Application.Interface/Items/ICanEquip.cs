using System;
using System.Diagnostics.Contracts;
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
