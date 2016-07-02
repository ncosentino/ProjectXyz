using System.Collections.Generic;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Game.Interface.Enchantments;

namespace ProjectXyz.Game.Core.Enchantments
{
    public sealed class StatCalculatorWrapper : IEnchantmentStatCalculator
    {
        private readonly IStatCalculator _statCalculator;
        private readonly IEnchantmentExpressionInterceptorConverter _enchantmentExpressionInterceptorConverter;

        public StatCalculatorWrapper(
            IStatCalculator statCalculator,
            IEnchantmentExpressionInterceptorConverter enchantmentExpressionInterceptorConverter)
        {
            _statCalculator = statCalculator;
            _enchantmentExpressionInterceptorConverter = enchantmentExpressionInterceptorConverter;
        }

        public double Calculate(
            IEnchantmentExpressionInterceptor enchantmentExpressionInterceptor,
            IReadOnlyDictionary<IIdentifier, IStat> baseStats,
            IIdentifier statDefinitionId)
        {
            var statExpressionInterceptor = _enchantmentExpressionInterceptorConverter.Convert(enchantmentExpressionInterceptor);
            var value = _statCalculator.Calculate(
                statExpressionInterceptor,
                baseStats,
                statDefinitionId);
            return value;
        }
    }
}