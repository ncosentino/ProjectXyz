using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemStatFactory
    {
        #region Methods
        IItemStat Create(
            Guid id,
            Guid itemId,
            Guid statId);
        #endregion
    }
}
