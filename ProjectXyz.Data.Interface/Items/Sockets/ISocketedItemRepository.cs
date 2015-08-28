using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Sockets
{
    public interface ISocketedItemRepository
    {
        #region Methods
        ISocketedItem Add(
            Guid id,
            Guid parentItemId,
            Guid childItemId);

        void RemoveById(Guid id);

        ISocketedItem GetById(Guid id);

        IEnumerable<ISocketedItem> GetByParentItemId(Guid id);

        ISocketedItem GetByChildItemId(Guid id);

        IEnumerable<ISocketedItem> GetAll();
        #endregion
    }
}
