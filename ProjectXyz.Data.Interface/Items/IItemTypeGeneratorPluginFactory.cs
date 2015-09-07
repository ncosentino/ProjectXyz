using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemTypeGeneratorPluginFactory
    {
        #region Methods
        IItemTypeGeneratorPlugin Create(
            Guid id,
            Guid magicTypeId,
            string itemGeneratorClassName);
        #endregion
    }
}
