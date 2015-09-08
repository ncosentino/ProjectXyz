using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.MagicTypes
{
    public interface IMagicTypeGroupingRepository
    {
        #region Methods
        IMagicTypeGrouping Add(
            Guid id,
            Guid groupingId,
            Guid magicTypeId);

        void RemoveById(Guid id);

        IMagicTypeGrouping GetById(Guid id);

        IEnumerable<IMagicTypeGrouping> GetByGroupingId(Guid groupingId);

        IEnumerable<IMagicTypeGrouping> GetAll();
        #endregion
    }
}
