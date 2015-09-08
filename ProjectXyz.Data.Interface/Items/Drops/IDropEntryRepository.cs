using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Drops
{
    public interface IDropEntryRepository
    {
        #region Methods
        IDropEntry Add(
            Guid id,
            Guid parentDropId,
            Guid magicTypeGroupingId,
            Guid weatherTypeGroupingId,
            Guid itemDefinitionId,
            int weighting);

        IDropEntry GetById(Guid id);

        IEnumerable<IDropEntry> GetByParentDropId(Guid parentDropId);

        IEnumerable<IDropEntry> GetAll();

        void RemoveById(Guid id);
        #endregion
    }
}
