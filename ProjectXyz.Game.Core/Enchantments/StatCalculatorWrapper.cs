using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.DomainConversions.EnchantmentsAndStats;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Stats.Calculations;
using ProjectXyz.Application.Enchantments.Interface.Calculations;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Game.Core.Enchantments
{
    public sealed class StatCalculatorWrapper : IEnchantmentStatCalculator
    {
        private readonly IStatCalculator _statCalculator;
        private readonly IReadOnlyCollection<IStatExpressionInterceptor> _statExpressionInterceptors;
        private readonly IReadOnlyCollection<IEnchantmentExpressionInterceptorConverter> _enchantmentExpressionInterceptorConverters;

        public StatCalculatorWrapper(
            IStatCalculator statCalculator,
            IEnumerable<IStatExpressionInterceptor> statExpressionInterceptors,
            IEnumerable<IEnchantmentExpressionInterceptorConverter> enchantmentExpressionInterceptorConverters)
        {
            _statCalculator = statCalculator;
            _statExpressionInterceptors = statExpressionInterceptors.ToArray();
            _enchantmentExpressionInterceptorConverters = enchantmentExpressionInterceptorConverters.ToArray();
        }

        public double Calculate(
            IReadOnlyCollection<IEnchantmentExpressionInterceptor> enchantmentExpressionInterceptors,
            IReadOnlyDictionary<IIdentifier, double> baseStats,
            IIdentifier statDefinitionId)
        {
            var statExpressionInterceptors = enchantmentExpressionInterceptors
                .Select(enchantmentExpressionInterceptor =>
                {
                    var foundConverter = _enchantmentExpressionInterceptorConverters
                        .FirstOrDefault(conv => conv.CanConvert(enchantmentExpressionInterceptor));
                    if (foundConverter == null)
                    {
                        throw new InvalidOperationException($"No converter found that can handle '{enchantmentExpressionInterceptor}'.");
                    }

                    var converted = foundConverter.Convert(enchantmentExpressionInterceptor);
                    return converted;
                })
                .Concat(_statExpressionInterceptors)
                .OrderBy(x => x.Priority)
                .ToArray();

            var value = _statCalculator.Calculate(
                statExpressionInterceptors,
                baseStats,
                statDefinitionId);
            return value;
        }
    }
}