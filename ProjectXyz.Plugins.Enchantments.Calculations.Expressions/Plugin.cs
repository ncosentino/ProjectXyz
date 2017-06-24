using System;
using System.Collections.Generic;
using ProjectXyz.Application.Enchantments.Api.Calculations;
using ProjectXyz.Plugins.Api;
using ProjectXyz.Plugins.Enchantments.Calculations.Api;

namespace ProjectXyz.Plugins.Enchantments.Calculations.Expressions
{
    public sealed class Plugin : IEnchantmentCalculationPlugin
    {
        public Plugin(IPluginArgs pluginArgs)
        {
            // TODO: how to get value mappers?
            var valueMappers = new List<ValueMapperDelegate>();

            var contextToTermValueMappingConverter = new ContextToTermValueMappingConverter(valueMappers);
            var valueMappingExpressionInterceptorFactory = new ValueMappingExpressionInterceptorFactory();

            ContextToExpressionInterceptorConverter = new ContextToExpressionInterceptorConverter(
                contextToTermValueMappingConverter,
                valueMappingExpressionInterceptorFactory);
        }

        public IContextToExpressionInterceptorConverter ContextToExpressionInterceptorConverter { get; }
    }
}
