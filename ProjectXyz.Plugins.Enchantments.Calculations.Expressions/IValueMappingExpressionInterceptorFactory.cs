using System.Collections.Generic;
using ProjectXyz.Api.Enchantments.Calculations;

namespace ProjectXyz.Plugins.Features.ExpressionEnchantments
{
    public interface IValueMappingExpressionInterceptorFactory
    {
        IEnchantmentExpressionInterceptor Create(IReadOnlyDictionary<string, double> termToValueMapping);
    }
}