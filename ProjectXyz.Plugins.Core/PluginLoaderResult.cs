using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Plugins.Api;
using ProjectXyz.Plugins.Interface;

namespace ProjectXyz.Plugins.Core
{
    public sealed class PluginLoaderResult : IPluginLoaderResult
    {
        public PluginLoaderResult(
            IEnumerable<IPlugin> plugins,
            IEnumerable<IComponent> components)
        {
            Plugins = plugins.ToArray();
            Components = new ComponentCollection(components);
        }

        public IReadOnlyCollection<IPlugin> Plugins { get; }

        public IComponentCollection Components { get; }
    }
}
