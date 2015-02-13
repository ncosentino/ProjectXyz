using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ProjectXyz.Application.Interface.Actors;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Maps;

namespace ProjectXyz.Application.Interface
{
    public interface IManager
    {
        #region Properties
        IActorManager Actors { get; }

        IMapManager Maps { get; }

        IItemManager Items { get; }
        #endregion
    }
}
