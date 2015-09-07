using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Plugins.Items
{
    public interface IItemPlugin
    {
        #region Properties
        IItemTypeGenerator ItemTypeGenerator { get; }
        #endregion
    }
}
