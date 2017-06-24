using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.States;
using ProjectXyz.Api.Stats;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Plugins.Api;
using ProjectXyz.Plugins.Enchantments.Calculations.Api;

namespace ProjectXyz.Plugins.Enchantments.Calculations.State
{
    public sealed class Plugin : IEnchantmentPlugin
    {
        public Plugin(IPluginArgs pluginArgs)
        {
            var stateIdToTermMapping = pluginArgs
                .GetFirst<IStateIdToTermRepository>()
                .GetStateIdToTermMappings()
                .ToDictionary(
                    x => x.StateIdentifier, 
                    x => (IReadOnlyDictionary<IIdentifier, string>)x.TermMapping);
            var statDefinitionIdToTermMapping = pluginArgs
                .GetFirst<IStatDefinitionToTermMappingRepository>()
                .GetStatDefinitionIdToTermMappings()
                .ToDictionary(
                    x => x.StateDefinitionId,
                    x => x.Term);

            var stateValueInjector = new StateValueInjector(stateIdToTermMapping);
            var stateExpressionInterceptorFactory = new StateExpressionInterceptorFactory(
                stateValueInjector,
                statDefinitionIdToTermMapping,
                1);

            ContextToExpressionInterceptorConverter = new ContextToExpressionInterceptorConverter(stateExpressionInterceptorFactory);
        }

        public IContextToExpressionInterceptorConverter ContextToExpressionInterceptorConverter { get; }
    }
}
