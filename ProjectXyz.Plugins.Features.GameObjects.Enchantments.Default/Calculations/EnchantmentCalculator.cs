using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations
{
    public sealed class EnchantmentCalculator : IEnchantmentCalculator
    {
        private readonly IEnchantmentCalculatorContextComponentHandler _enchantmentCalculatorContextComponentHandler;
        private readonly IEnchantmentStatCalculator _enchantmentStatCalculator;
        private readonly IConvert<IEnchantmentCalculatorContext, IReadOnlyCollection<IEnchantmentExpressionInterceptor>> _contextToInterceptorsConverter;

        public EnchantmentCalculator(
            IEnchantmentStatCalculator enchantmentStatCalculator,
            IConvert<IEnchantmentCalculatorContext, IReadOnlyCollection<IEnchantmentExpressionInterceptor>> contextToInterceptorsConverter,
            IEnchantmentCalculatorContextComponentHandler enchantmentCalculatorContextComponentHandler)
        {
            _enchantmentStatCalculator = enchantmentStatCalculator;
            _contextToInterceptorsConverter = contextToInterceptorsConverter;
            _enchantmentCalculatorContextComponentHandler = enchantmentCalculatorContextComponentHandler;
        }

        public double Calculate(
            IEnchantmentCalculatorContext enchantmentCalculatorContext,
            IReadOnlyDictionary<IIdentifier, double> baseStats,
            IIdentifier statDefinitionId)
        {
            var enchantmentExpressionInterceptors = _contextToInterceptorsConverter
                .Convert(enchantmentCalculatorContext);
            var updatedBaseStats = TransformBaseStats(
                enchantmentCalculatorContext,
                baseStats,
                statDefinitionId);
            var value = _enchantmentStatCalculator.Calculate(
                enchantmentExpressionInterceptors,
                updatedBaseStats,
                statDefinitionId);
            return value;
        }

        private IReadOnlyDictionary<IIdentifier, double> TransformBaseStats(
            IEnchantmentCalculatorContext enchantmentCalculatorContext,
            IReadOnlyDictionary<IIdentifier, double> baseStats,
            IIdentifier statDefinitionId)
        {
            IReadOnlyDictionary<IIdentifier, double> updatedBaseStats;
            if (_enchantmentCalculatorContextComponentHandler.CanHandle(
                baseStats,
                statDefinitionId,
                enchantmentCalculatorContext.Components))
            {
                var mutate = baseStats.ToDictionary();
                foreach (var overrideValue in _enchantmentCalculatorContextComponentHandler.OverrideBaseStat(
                    baseStats,
                    statDefinitionId,
                    enchantmentCalculatorContext.Components))
                {
                    mutate[overrideValue.Key] = overrideValue.Value;
                }

                updatedBaseStats = mutate;
            }
            else
            {
                updatedBaseStats = baseStats;
            }

            return updatedBaseStats;
        }
    }
}