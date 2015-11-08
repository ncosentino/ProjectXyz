using System;
using System.Diagnostics.Contracts;
using ProjectXyz.Application.Interface.Items.Contracts;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(ICanUnequipItemContract))]
    public interface ICanUnequip
    {
        #region Methods
        IItem Unequip(Guid slotId);

        bool CanUnequip(Guid slotId);
        #endregion
    }
}
