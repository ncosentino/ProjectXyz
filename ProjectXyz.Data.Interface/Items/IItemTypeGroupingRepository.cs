using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemTypeGroupingRepository
    {
        #region Methods
        IItemTypeGrouping Add(
            Guid id,
            Guid groupingId,
            Guid itemTypeId);

        void RemoveById(Guid id);

        IItemTypeGrouping GetById(Guid id);

        IEnumerable<IItemTypeGrouping> GetByGroupingId(Guid groupingId);

        IEnumerable<IItemTypeGrouping> GetAll();
        #endregion
    }
}
