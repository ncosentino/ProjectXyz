using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Affixes
{
    public interface IItemAffixDefinitionFactory
    {
        #region Methods
        IItemAffixDefinition Create(
            Guid id,
            Guid nameStringResourceId,
            bool isPrefix,
            int minimumLevel,
            int maximumLevel);
        #endregion
    }
}
