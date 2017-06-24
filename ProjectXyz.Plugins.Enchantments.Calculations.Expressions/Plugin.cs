using System.Linq;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Plugins.Api;
using ProjectXyz.Plugins.Enchantments.Calculations.Api;

namespace ProjectXyz.Plugins.Enchantments.Calculations.Expressions
{
    public sealed class Plugin : IEnchantmentPlugin
    {
        public Plugin(IPluginArgs pluginArgs)
        {
            var valueMappers = pluginArgs
                .GetFirst<IValueMapperRepository>()
                .GetValueMappers()
                .ToArray();

            var contextToTermValueMappingConverter = new ContextToTermValueMappingConverter(valueMappers);
            var valueMappingExpressionInterceptorFactory = new ValueMappingExpressionInterceptorFactory(2);

            ContextToExpressionInterceptorConverter = new ContextToExpressionInterceptorConverter(
                contextToTermValueMappingConverter,
                valueMappingExpressionInterceptorFactory);
        }

        public IContextToExpressionInterceptorConverter ContextToExpressionInterceptorConverter { get; }
    }
}
