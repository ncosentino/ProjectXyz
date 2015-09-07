using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Data.Interface;
using ProjectXyz.Plugins.Interface;

namespace ProjectXyz.Game.Interface
{
    public interface IGameManager
    {
        #region Properties
        IDataManager DataManager { get; }

        IApplicationManager ApplicationManager { get; }

        IPluginManager PluginManager { get; }

        IPluginRegistrarManager PluginRegistrarManager { get; }
        #endregion
    }
}
