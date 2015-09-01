using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Requirements
{
    public interface IItemDefinitionItemMiscRequirementsFactory
    {
        #region Methods
        IItemDefinitionItemMiscRequirements Create(
            Guid id,
            Guid itemDefinitionId,
            Guid itemMiscRequirementsId);
        #endregion
    }
}
