using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.MagicTypes
{
    public interface IMagicTypeGrouping
    {
        #region Properties
        Guid Id { get; }

        Guid GroupingId { get; }

        Guid MagicTypeId { get; }
        #endregion
    }
}
