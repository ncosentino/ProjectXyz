using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Maps
{
    public interface IMapStoreFactory
    {
        #region Methods
        IMapStore CreateMapStore(
            Guid mapId,
            bool isInterior);
        #endregion
    }
}
