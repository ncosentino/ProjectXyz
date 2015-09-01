using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Requirements
{
    public interface IItemStoreItemMiscRequirementsFactory
    {
        #region Methods
        IItemMiscRequirements Create(
            Guid id,
            Guid itemId,
            Guid itemMiscRequirementsId);
        #endregion
    }
}
