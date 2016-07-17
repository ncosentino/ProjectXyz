using System.Collections.Generic;

namespace ProjectXyz.Application.Interface.Enchantments.Calculations
{
    public interface IValueMappingExpressionInterceptorFactory
    {
        IEnchantmentExpressionInterceptor Create(IReadOnlyDictionary<string, double> termToValueMapping);
    }
}