using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Sockets
{
    public interface IStatSocketTypeFactory
    {
        #region Methods
        IStatSocketType Create(Guid statId, Guid socketTypeId);
        #endregion
    }
}
