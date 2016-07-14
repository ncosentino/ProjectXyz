using System.Collections.Generic;
using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class ValueMappingExpressionInterceptorFactory : IValueMappingExpressionInterceptorFactory
    {
        public IEnchantmentExpressionInterceptor Create(IReadOnlyDictionary<string, double> termToValueMapping)
        {
            return new ValueMappingExpressionInterceptor(termToValueMapping);
        }
    }
}