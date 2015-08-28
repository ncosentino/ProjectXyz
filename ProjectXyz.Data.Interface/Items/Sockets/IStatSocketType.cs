using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Items.Sockets
{
    public interface IStatSocketType
    {
        #region Properties
        Guid Id { get; }

        Guid StatDefinitionId { get; }

        Guid SocketTypeId { get; }
        #endregion
    }
}
