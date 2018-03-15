using System.Collections.Generic;
using ProjectXyz.Api.Enchantments.Calculations;

namespace ProjectXyz.Plugins.Features.ExpressionEnchantments
{
    public sealed class ValueMappingExpressionInterceptorFactory : IValueMappingExpressionInterceptorFactory
    {
        private readonly int _priority;

        public ValueMappingExpressionInterceptorFactory(int priority)
        {
            _priority = priority;
        }

        public IEnchantmentExpressionInterceptor Create(IReadOnlyDictionary<string, double> termToValueMapping)
        {
            return new ValueMappingExpressionInterceptor(
                termToValueMapping,
                _priority);
        }
    }
}