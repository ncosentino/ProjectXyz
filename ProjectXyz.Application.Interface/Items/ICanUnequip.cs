using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using ProjectXyz.Application.Interface.Actors.Contracts;
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
