using ProjectXyz.Api.States.Plugins;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;

namespace ProjectXyz.Plugins.States.Simple
{
    public sealed class Plugin : IStatePlugin
    {
        public IComponentCollection SharedComponents => ComponentCollection.Empty;
    }
}
