using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Maps
{
    public interface IMapStoreRepository
    {
        #region Methods
        IMapStore GetMapStoreById(Guid mapStoreId);
        #endregion
    }
}
