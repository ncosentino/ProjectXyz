using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
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
            IReadOnlyCollection<IEnchantmentExpressionInterceptor> enchantmentExpressionInterceptors,
            IReadOnlyDictionary<IIdentifier, double> baseStats,
            IIdentifier statDefinitionId)
        {
            var statExpressionInterceptors = enchantmentExpressionInterceptors
                .Select(_enchantmentExpressionInterceptorConverter.Convert)
                .ToArray();
            var value = _statCalculator.Calculate(
                statExpressionInterceptors,
                baseStats,
                statDefinitionId);
            return value;
        }
    }
}