using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemStatRepository
    {
        #region Methods
        IItemStat Add(
            Guid id,
            Guid itemId,
            Guid statId);

        void RemoveById(Guid id);

        IItemStat GetById(Guid id);

        IEnumerable<IItemStat> GetByItemId(Guid itemId);

        IEnumerable<IItemStat> GetAll();
        #endregion
    }
}
