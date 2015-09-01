using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Requirements
{
    public interface IItemDefinitionItemMiscRequirements
    {
        #region Properties
        Guid Id { get; }
        
        Guid ItemDefinitionId { get; }

        Guid ItemMiscRequirementsId { get; }
        #endregion
    }
}
