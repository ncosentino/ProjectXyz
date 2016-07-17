using System.Collections.Generic;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;

namespace ProjectXyz.Application.Core.Enchantments.Calculations
{
    public sealed class ValueMappingExpressionInterceptorFactory : IValueMappingExpressionInterceptorFactory
    {
        public IEnchantmentExpressionInterceptor Create(IReadOnlyDictionary<string, double> termToValueMapping)
        {
            return new ValueMappingExpressionInterceptor(termToValueMapping);
        }
    }
}