using System.Collections.Generic;
using ProjectXyz.Application.Enchantments.Api.Calculations;

namespace ProjectXyz.Plugins.Enchantments.Calculations.Expressions
{
    public interface IValueMappingExpressionInterceptorFactory
    {
        IEnchantmentExpressionInterceptor Create(IReadOnlyDictionary<string, double> termToValueMapping);
    }
}