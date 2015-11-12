using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Maps
{
    public interface IMapStore : IGameObject
    {
        #region Properties
        bool IsInterior { get; }
        #endregion
    }
}
