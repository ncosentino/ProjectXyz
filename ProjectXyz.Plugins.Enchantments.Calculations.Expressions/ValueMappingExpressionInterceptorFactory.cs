using System.Collections.Generic;
using ProjectXyz.Application.Enchantments.Api.Calculations;

namespace ProjectXyz.Plugins.Enchantments.Calculations.Expressions
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