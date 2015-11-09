using System.Collections.Generic;
using ProjectXyz.Application.Interface.Maps;

namespace ProjectXyz.Application.Interface.Worlds
{
    public interface IWorld
    {
        #region Properties
        IEnumerable<IMap> Maps { get; }
        #endregion

        #region Methods
        void ActivateMap(IMap map);
        #endregion
    }
}