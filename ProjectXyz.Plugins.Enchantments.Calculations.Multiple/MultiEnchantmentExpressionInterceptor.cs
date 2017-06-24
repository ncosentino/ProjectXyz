using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Enchantments.Api.Calculations;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Plugins.Enchantments.Calculations.Multiple
{
    public sealed class MultiEnchantmentExpressionInterceptor : IEnchantmentExpressionInterceptor
    {
        private readonly IReadOnlyCollection<IEnchantmentExpressionInterceptor> _enchantmentExpressionInterceptors;

        public MultiEnchantmentExpressionInterceptor(IEnumerable<IEnchantmentExpressionInterceptor> enchantmentExpressionInterceptors)
            : this(enchantmentExpressionInterceptors.ToArray())
        {
        }

        public MultiEnchantmentExpressionInterceptor(IReadOnlyCollection<IEnchantmentExpressionInterceptor> enchantmentExpressionInterceptors)
        {
            _enchantmentExpressionInterceptors = enchantmentExpressionInterceptors;
        }

        public string Intercept(IIdentifier statDefinitionId, string expression)
        {
            return _enchantmentExpressionInterceptors.Aggregate(
                expression, 
                (c, interceptor) => interceptor.Intercept(statDefinitionId, c));
        }
    }
}