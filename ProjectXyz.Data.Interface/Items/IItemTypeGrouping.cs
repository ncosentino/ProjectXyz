using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemTypeGrouping
    {
        #region Properties
        Guid Id { get; }

        Guid GroupingId { get; }

        Guid ItemTypeId { get; }
        #endregion
    }
}
