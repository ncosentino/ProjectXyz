using System.Collections.Generic;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Application.Enchantments.Interface.Calculations;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Enchantments.Core.Calculations
{
    public sealed class EnchantmentCalculator : IEnchantmentCalculator
    {
        private readonly IEnchantmentStatCalculator _enchantmentStatCalculator;
        private readonly IConvert<IEnchantmentCalculatorContext, IReadOnlyCollection<IEnchantmentExpressionInterceptor>> _contextToInterceptorsConverter;

        public EnchantmentCalculator(
            IEnchantmentStatCalculator enchantmentStatCalculator,
            IConvert<IEnchantmentCalculatorContext, IReadOnlyCollection<IEnchantmentExpressionInterceptor>> contextToInterceptorsConverter)
        {
            _enchantmentStatCalculator = enchantmentStatCalculator;
            _contextToInterceptorsConverter = contextToInterceptorsConverter;
        }

        public double Calculate(
            IEnchantmentCalculatorContext enchantmentCalculatorContext,
            IReadOnlyDictionary<IIdentifier, double> baseStats,
            IIdentifier statDefinitionId)
        {
            var enchantmentExpressionInterceptors = _contextToInterceptorsConverter.Convert(enchantmentCalculatorContext);
            var value = _enchantmentStatCalculator.Calculate(
                enchantmentExpressionInterceptors,
                baseStats,
                statDefinitionId);
            return value;
        }
    }
}