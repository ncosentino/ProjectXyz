using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Sockets
{
    public interface ISocketedItemFactory
    {
        #region Methods
        ISocketedItem Create(
            Guid id,
            Guid parentItemId,
            Guid childItemId);
        #endregion
    }
}
