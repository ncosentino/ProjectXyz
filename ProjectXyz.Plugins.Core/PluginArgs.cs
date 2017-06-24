using System.Collections.Generic;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Plugins.Api;

namespace ProjectXyz.Plugins.Core
{
    public sealed class PluginArgs : IPluginArgs
    {
        public PluginArgs(IEnumerable<IComponent> components)
        {
            Components = new ComponentCollection(components);;
        }

        public IComponentCollection Components { get; }
    }
}