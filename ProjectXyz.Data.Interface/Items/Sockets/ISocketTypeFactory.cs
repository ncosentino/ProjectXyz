using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Sockets
{
    public interface ISocketTypeFactory
    {
        #region Methods
        ISocketType Create(
            Guid id, 
            Guid nameStringResourceId);
        #endregion
    }
}
