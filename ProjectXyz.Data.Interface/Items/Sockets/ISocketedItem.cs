using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Sockets
{
    public interface ISocketedItem
    {
        #region Properties
        Guid Id { get; }

        Guid ParentItemId { get; }

        Guid ChildItemId { get; }
        #endregion
    }
}
