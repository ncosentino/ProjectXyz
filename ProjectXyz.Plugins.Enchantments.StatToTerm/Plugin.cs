using System.Linq;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Stats;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Plugins.Api;
using ProjectXyz.Plugins.Enchantments.Calculations.Api;

namespace ProjectXyz.Plugins.Enchantments.StatToTerm
{
    public sealed class Plugin : IEnchantmentPlugin
    {
        public Plugin(IPluginArgs pluginArgs)
        {
            var statDefinitionIdToTermMapping = pluginArgs
                .GetFirst<IComponent<IStatDefinitionToTermMappingRepository>>()
                .Value
                .GetStatDefinitionIdToTermMappings()
                .ToDictionary(
                    x => x.StateDefinitionId,
                    x => x.Term);

            var statToTermExpressionInterceptorFactory = new StatToTermExpressionInterceptorFactory(
                statDefinitionIdToTermMapping,
                0);

            SharedComponents = new ComponentCollection(new[]
            {
                new GenericComponent<IContextToExpressionInterceptorConverter>(new ContextToExpressionInterceptorConverter(statToTermExpressionInterceptorFactory)),
            });
        }

        public IComponentCollection SharedComponents { get; }
    }
}
