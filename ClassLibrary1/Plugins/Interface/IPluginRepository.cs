using System.Collections.Generic;
using ClassLibrary1.Plugins.Api;

namespace ClassLibrary1.Plugins.Interface
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
