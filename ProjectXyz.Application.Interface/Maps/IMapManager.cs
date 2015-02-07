using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Maps
{
    public interface IMapManager
    {
        #region Methods
        IMap GetMapById(Guid mapId, IMapContext mapContext);
        #endregion
    }
}
