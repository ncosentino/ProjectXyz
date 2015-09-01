using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemDefinitionStatFactory
    {
        #region Methods
        IItemDefinitionStat Create(
            Guid id,
            Guid itemDefinitionId,
            Guid statDefinitionId,
            double minimumValue,
            double maximumValue);
        #endregion
    }
}
