using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Items.Contracts;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(IcanUseItemContract))]
    public interface ICanUseItem
    {
        #region Methods
        bool CanUseItem(IItem item);

        void UseItem(IItem item);
        #endregion
    }
}
