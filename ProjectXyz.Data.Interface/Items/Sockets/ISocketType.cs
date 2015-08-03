using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Sockets
{
    public interface ISocketType
    {
        #region Properties
        Guid Id { get; }

        Guid StringResourceId { get; }
        #endregion
    }
}
