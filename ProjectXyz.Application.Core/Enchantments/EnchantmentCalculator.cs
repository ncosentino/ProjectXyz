using System.Collections.Generic;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class EnchantmentCalculator : IEnchantmentCalculator
    {
        private readonly IEnchantmentExpressionInterceptorFactory _enchantmentExpressionInterceptorFactory;
        private readonly IEnchantmentStatCalculator _enchantmentStatCalculator;

        public EnchantmentCalculator(
            IEnchantmentExpressionInterceptorFactory enchantmentExpressionInterceptorFactory,
            IEnchantmentStatCalculator enchantmentStatCalculator)
        {
            _enchantmentExpressionInterceptorFactory = enchantmentExpressionInterceptorFactory;
            _enchantmentStatCalculator = enchantmentStatCalculator;
        }

        public double Calculate(
            IReadOnlyDictionary<IIdentifier, IStat> baseStats,
            IReadOnlyCollection<IEnchantment> enchantments,
            IIdentifier statDefinitionId)
        {
            var interceptor = _enchantmentExpressionInterceptorFactory.Create(enchantments);
            var value = _enchantmentStatCalculator.Calculate(
                interceptor,
                baseStats,
                statDefinitionId);
            return value;
        }
    }
}