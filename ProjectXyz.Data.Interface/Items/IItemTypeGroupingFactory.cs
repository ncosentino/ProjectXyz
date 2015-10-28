using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemTypeGroupingFactory
    {
        #region Methods
        IItemTypeGrouping Create(
            Guid id,
            Guid groupingId,
            Guid itemTypeId);
        #endregion
    }
}
