using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemStat
    {
        #region Properties
        Guid Id { get; }

        Guid ItemId { get; }

        Guid StatId { get; }
        #endregion
    }
}
