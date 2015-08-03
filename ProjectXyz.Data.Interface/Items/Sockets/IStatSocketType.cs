using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Items.Sockets
{
    public interface IStatSocketType
    {
        #region Properties
        Guid StatId { get; }

        Guid SocketTypeId { get; }
        #endregion
    }
}
