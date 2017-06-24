using System.Collections.Generic;
using ProjectXyz.Api.Enchantments.Calculations;

namespace ProjectXyz.Plugins.Enchantments.Calculations.Expressions
{
    public interface IValueMappingExpressionInterceptorFactory
    {
        IEnchantmentExpressionInterceptor Create(IReadOnlyDictionary<string, double> termToValueMapping);
    }
}