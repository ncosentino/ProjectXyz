using ProjectXyz.Plugins.Api;

namespace ProjectXyz.Api.Stats.Plugins
{
    public interface IStatPlugin : IPlugin
    {

        IStatDefinitionToTermMappingRepository StatDefinitionToTermMappingRepository { get; }
    }
}