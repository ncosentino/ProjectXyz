using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using ProjectXyz.Application.Interface.Items.Contracts;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(ISocketableContract))]
    public interface ISocketable
    {
        #region Properties
        IItemCollection SocketedItems { get; }
        #endregion

        #region Methods
        bool Socket(IItem item);

        int GetOpenSocketsForType(Guid socketTypeId);

        int GetTotalSocketsForType(Guid socketTypeId);
        #endregion
    }
}
