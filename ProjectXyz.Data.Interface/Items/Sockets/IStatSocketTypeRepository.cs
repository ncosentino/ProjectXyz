using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Sockets
{
    public interface IStatSocketTypeRepository
    {
        #region Methods
        IStatSocketType GetByStatId(Guid statId);

        IStatSocketType GetBySocketTypeId(Guid socketTypeId);

        IEnumerable<IStatSocketType> GetAll();
        #endregion
    }
}
