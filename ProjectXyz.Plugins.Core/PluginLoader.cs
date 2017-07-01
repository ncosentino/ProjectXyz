using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Plugins.Api;
using ProjectXyz.Plugins.Interface;

namespace ProjectXyz.Plugins.Core
{
    public sealed class PluginLoader : IPluginLoader
    {
        public IPluginLoaderResult LoadPlugins(
            IPluginArgs pluginArgs,
            IEnumerable<Type> pluginTypes)
        {
            var plugins = new List<IPlugin>();

            foreach (var pluginType in pluginTypes)
            {
                var plugin = (IPlugin)pluginType
                    .GetConstructors()
                    .Single()
                    .Invoke(new object[] { pluginArgs });

                plugins.Add(plugin);

                pluginArgs = new PluginArgs(pluginArgs.Components.Concat(plugin.SharedComponents));
            }

            return new PluginLoaderResult(
                plugins,
                pluginArgs.Components);
        }
    }
}
