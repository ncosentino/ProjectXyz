using System.Collections.Generic;
using ProjectXyz.Plugins.Api;

namespace ProjectXyz.Plugins.Interface
{
    public interface IPluginProvider
    {
        #region Methods
        IEnumerable<TPlugin> GetPlugins<TPlugin>()
            where TPlugin : IPlugin;
        #endregion
    }
}
