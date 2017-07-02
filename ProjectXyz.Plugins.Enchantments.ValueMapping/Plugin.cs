using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Plugins.Api;

namespace ProjectXyz.Plugins.Enchantments.ValueMapping
{
    public sealed class Plugin : IPlugin
    {
        public Plugin(IPluginArgs _)
        {
            SharedComponents = new ComponentCollection(new IComponent[]
            {
                GenericComponent.Create(new ValueMapperRepository()),
            });
        }

        public IComponentCollection SharedComponents { get; }
    }
}
