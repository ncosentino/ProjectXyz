using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Drops
{
    public interface IDropLinkRepository
    {
        #region Methods
        IDropLinkRepository Add(
            Guid id,
            Guid parentDropId,
            Guid childDropId,
            int weighting);

        IDropLink GetById(Guid id);

        IEnumerable<IDropLink> GetByParentDropId(Guid parentDropId);

        IEnumerable<IDropLink> GetAll();

        void RemoveById(Guid id);
        #endregion
    }
}
