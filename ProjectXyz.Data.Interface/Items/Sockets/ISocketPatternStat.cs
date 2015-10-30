using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Interface.Items.Sockets
{
    public interface ISocketPatternStat : IStatRange
    {
        #region Properties
        Guid Id { get; }

        Guid SocketPatternDefinitionId { get; }
        #endregion
    }
}
