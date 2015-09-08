using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Drops
{
    public interface IDropEntryFactory
    {
        #region Methods
        IDropEntry Create(
            Guid id,
            Guid parentDropId,
            Guid magicTypeGroupingId,
            Guid weatherTypeGroupingId,
            Guid itemDefinitionId,
            int weighting);
        #endregion
    }
}
