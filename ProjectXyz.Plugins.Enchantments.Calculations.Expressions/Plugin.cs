using System.Linq;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Plugins.Api;
using ProjectXyz.Plugins.Enchantments.Calculations.Api;

namespace ProjectXyz.Plugins.Enchantments.Calculations.Expressions
{
    public sealed class Plugin : IEnchantmentPlugin
    {
        public Plugin(IPluginArgs pluginArgs)
        {
            var valueMappers = pluginArgs
                .GetFirst<IComponent<IValueMapperRepository>>()
                .Value
                .GetValueMappers()
                .ToArray();

            var contextToTermValueMappingConverter = new ContextToTermValueMappingConverter(valueMappers);
            var valueMappingExpressionInterceptorFactory = new ValueMappingExpressionInterceptorFactory(2);;

            SharedComponents = new ComponentCollection(new[]
            {
                new GenericComponent<IContextToExpressionInterceptorConverter>(new ContextToExpressionInterceptorConverter(
                    contextToTermValueMappingConverter,
                    valueMappingExpressionInterceptorFactory)), 
            });
        }

        public IComponentCollection SharedComponents { get; }
    }
}
