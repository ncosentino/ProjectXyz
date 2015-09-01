using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Requirements
{
    public interface IItemDefinitionStatRequirementsFactory
    {
        #region Methods
        IItemDefinitionStatRequirements Create(
            Guid id,
            Guid itemDefinitionId,
            Guid statId);
        #endregion
    }
}
