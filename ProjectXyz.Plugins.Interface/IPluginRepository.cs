using System.Collections.Generic;
using ProjectXyz.Plugins.Api;

namespace ProjectXyz.Plugins.Interface
{
    public interface IPluginRepository<out TPlugin> : 
        IPluginRepository
        where TPlugin : IPlugin
    {
        #region Properties
        IReadOnlyCollection<TPlugin> Plugins { get; }
        #endregion
    }

    public interface IPluginRepository
    {
    }
}
