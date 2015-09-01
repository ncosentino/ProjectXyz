using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Requirements
{
    public interface IItemStoreItemMiscRequirements
    {
        #region Properties
        Guid Id { get; }
        
        Guid ItemId { get; }

        Guid ItemMiscRequirementsId { get; }
        #endregion
    }
}
