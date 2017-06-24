using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Plugins.Enchantments.Calculations.Multiple
{
    public sealed class MultiEnchantmentExpressionInterceptor : IEnchantmentExpressionInterceptor
    {
        private readonly IReadOnlyCollection<IEnchantmentExpressionInterceptor> _enchantmentExpressionInterceptors;

        public MultiEnchantmentExpressionInterceptor(IEnumerable<IEnchantmentExpressionInterceptor> enchantmentExpressionInterceptors, int priority)
            : this(enchantmentExpressionInterceptors.ToArray(), priority)
        {
        }

        public MultiEnchantmentExpressionInterceptor(
            IReadOnlyCollection<IEnchantmentExpressionInterceptor> enchantmentExpressionInterceptors,
            int priority)
        {
            _enchantmentExpressionInterceptors = enchantmentExpressionInterceptors;
            Priority = priority;
        }

        public int Priority { get; }

        public string Intercept(IIdentifier statDefinitionId, string expression)
        {
            return _enchantmentExpressionInterceptors.Aggregate(
                expression, 
                (c, interceptor) => interceptor.Intercept(statDefinitionId, c));
        }
    }
}