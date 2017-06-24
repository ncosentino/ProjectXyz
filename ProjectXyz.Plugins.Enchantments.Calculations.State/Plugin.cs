using System.Collections.Generic;
using ProjectXyz.Application.Enchantments.Api.Calculations;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Plugins.Api;
using ProjectXyz.Plugins.Enchantments.Calculations.Api;

namespace ProjectXyz.Plugins.Enchantments.Calculations.State
{
    public sealed class Plugin : IEnchantmentCalculationPlugin
    {
        public Plugin(IPluginArgs pluginArgs)
        {
            // TODO: how to get stateIdToTermMapping
            var stateIdToTermMapping = new Dictionary<IIdentifier, IReadOnlyDictionary<IIdentifier, string>>();

            // TODO: how to get statDefinitionIdToTermMapping
            var statDefinitionIdToTermMapping = new Dictionary<IIdentifier, string>();

            var stateValueInjector = new StateValueInjector(stateIdToTermMapping);
            var stateExpressionInterceptorFactory = new StateExpressionInterceptorFactory(
                stateValueInjector,
                statDefinitionIdToTermMapping);

            ContextToExpressionInterceptorConverter = new ContextToExpressionInterceptorConverter(stateExpressionInterceptorFactory);
        }

        public IContextToExpressionInterceptorConverter ContextToExpressionInterceptorConverter { get; }
    }
}
