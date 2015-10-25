using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.MagicTypes
{
    public interface IMagicTypeGroupingFactory
    {
        #region Methods
        IMagicTypeGrouping Create(
            Guid id,
            Guid groupingId,
            Guid magicTypeId);
        #endregion
    }
}
