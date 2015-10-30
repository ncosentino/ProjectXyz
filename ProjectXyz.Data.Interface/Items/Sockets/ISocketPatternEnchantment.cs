using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Items.Sockets
{
    public interface ISocketPatternEnchantment
    {
        #region Properties
        Guid Id { get; }

        Guid SocketPatternDefinitionId { get; }

        Guid EnchantmentDefinitionId { get; }
        #endregion
    }
}
