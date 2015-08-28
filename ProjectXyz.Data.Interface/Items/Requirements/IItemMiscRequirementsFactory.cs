using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Requirements
{
    public interface IItemMiscRequirementsFactory
    {
        #region Methods
        IItemMiscRequirements Create(
            Guid id,
            Guid itemId,
            Guid raceDefinitionId,
            Guid classDefinitionId);
        #endregion
    }
}
