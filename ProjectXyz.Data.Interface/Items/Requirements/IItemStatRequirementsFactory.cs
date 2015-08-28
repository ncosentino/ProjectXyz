using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Requirements
{
    public interface IItemStatRequirementsFactory
    {
        #region Methods
        IItemStatRequirements Create(
            Guid id,
            Guid itemId,
            Guid statId);
        #endregion
    }
}
