using System.Collections.Generic;
using ClassLibrary1.Plugins.Api;

namespace ClassLibrary1.Plugins.Interface
{
    public interface IPluginProvider
    {
        #region Methods
        IEnumerable<TPlugin> GetPlugins<TPlugin>()
            where TPlugin : IPlugin;
        #endregion
    }
}
