using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Plugins.Api;

namespace ProjectXyz.Plugins.Core
{
    public sealed class PluginArgs : IPluginArgs
    {
        public PluginArgs(IComponentCollection components)
        {
            Components = components;
        }

        public IComponentCollection Components { get; }
    }
}