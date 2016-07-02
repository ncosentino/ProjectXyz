using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class EnchantmentExpressionInterceptorFactory : IEnchantmentExpressionInterceptorFactory
    {
        private readonly IReadOnlyDictionary<IIdentifier, string> _statDefinitionIdToTermMapping;

        public EnchantmentExpressionInterceptorFactory(IReadOnlyDictionary<IIdentifier, string> statDefinitionIdToTermMapping)
        {
            _statDefinitionIdToTermMapping = statDefinitionIdToTermMapping;
        }

        public IEnchantmentExpressionInterceptor Create(IReadOnlyCollection<IEnchantment> enchantments)
        {
            var statDefinitionToEnchantmentMapping = enchantments
                .TakeTypes<IExpressionEnchantment>()
                .GroupBy(
                    x => x.StatDefinitionId,
                    x => x)
                .ToDictionary(
                    x => x.Key,
                    x => (IReadOnlyCollection<IExpressionEnchantment>)x.Select(g => g).OrderBy(e => e.CalculationPriority).ToArray());

            var interceptor = new ExpressionEnchantmentExpressionInterceptor(
                _statDefinitionIdToTermMapping,
                statDefinitionToEnchantmentMapping);
            return interceptor;
        }
    }
}