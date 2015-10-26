using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemNamePartRepository
    {
        #region Methods
        IItemNamePart Add(
            Guid id,
            Guid partId,
            Guid nameStringResourceId,
            int order);

        void RemoveById(Guid id);

        void RemoveByPartId(Guid partId);

        IItemNamePart GetById(Guid id);

        IEnumerable<IItemNamePart> GetByPartId(Guid partId);

        IEnumerable<IItemNamePart> GetAll();
        #endregion
    }
}
