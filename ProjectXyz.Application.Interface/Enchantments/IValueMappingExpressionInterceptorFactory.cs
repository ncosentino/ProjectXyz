using System.Collections.Generic;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IValueMappingExpressionInterceptorFactory
    {
        IEnchantmentExpressionInterceptor Create(IReadOnlyDictionary<string, double> termToValueMapping);
    }
}