using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Plugins.Api;

namespace ProjectXyz.Plugins.Interface
{
    public interface IPluginLoader
    {
        IPluginLoaderResult LoadPlugins(
            IPluginArgs pluginArgs,
            IEnumerable<Type> pluginTypes);
    }
}
