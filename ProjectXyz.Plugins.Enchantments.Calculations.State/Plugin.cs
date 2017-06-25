using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.States;
using ProjectXyz.Api.Stats;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;
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
                .GetFirst<IComponent<IStateIdToTermRepository>>()
                .Value
                .GetStateIdToTermMappings()
                .ToDictionary(
                    x => x.StateIdentifier, 
                    x => (IReadOnlyDictionary<IIdentifier, string>)x.TermMapping);
            var statDefinitionIdToTermMapping = pluginArgs
                .GetFirst<IComponent<IStatDefinitionToTermMappingRepository>>()
                .Value
                .GetStatDefinitionIdToTermMappings()
                .ToDictionary(
                    x => x.StateDefinitionId,
                    x => x.Term);

            var stateValueInjector = new StateValueInjector(stateIdToTermMapping);
            var stateExpressionInterceptorFactory = new StateExpressionInterceptorFactory(
                stateValueInjector,
                statDefinitionIdToTermMapping,
                1);

            SharedComponents = new ComponentCollection(new[]
            {
                new GenericComponent<IContextToExpressionInterceptorConverter>(new ContextToExpressionInterceptorConverter(stateExpressionInterceptorFactory)),
            });
        }

        public IComponentCollection SharedComponents { get; }
    }
}
