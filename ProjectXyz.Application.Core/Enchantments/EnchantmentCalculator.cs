using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class EnchantmentCalculator : IEnchantmentCalculator
    {
        private readonly ITry _try;
        private readonly IEnchantmentCalculatorResultFactory _enchantmentCalculatorResultFactory;
        private readonly IEnchantmentContext _enchantmentContext;
        private readonly IReadOnlyCollection<IEnchantmentTypeCalculator> _calculators;

        public EnchantmentCalculator(
            ITry @try,
            IEnchantmentCalculatorResultFactory enchantmentCalculatorResultFactory,
            IEnchantmentContext enchantmentContext,
            IReadOnlyCollection<IEnchantmentTypeCalculator> calculators)
        {
            _try = @try;
            _enchantmentCalculatorResultFactory = enchantmentCalculatorResultFactory;
            _enchantmentContext = enchantmentContext;
            _calculators = calculators;
        }

        public IEnchantmentCalculatorResult Calculate(
            IStatCollection stats, 
            IEnumerable<IEnchantment> enchantments)
        {
            var newStats = stats;

            var enchantmentsToBeProcessed = enchantments.ToArray();
            var activeEnchantments = enchantmentsToBeProcessed.ToArray();

            foreach (var enchantmentTypeCalculator in _calculators)
            {
                var result = enchantmentTypeCalculator.Calculate(
                    _enchantmentContext,
                    newStats,
                    activeEnchantments);

                enchantmentsToBeProcessed = enchantmentsToBeProcessed
                    .Except(result.ProcessedEnchantments)
                    .Except(result.RemovedEnchantments)
                    .Concat(result.AddedEnchantments)
                    .ToArray();

                activeEnchantments = activeEnchantments
                    .Where(x => !result.RemovedEnchantments.Contains(x))
                    .ToArray();

                newStats = result.Stats;
            }

            _try.Dangerous(() =>
            {
                if (enchantmentsToBeProcessed.Any())
                {
                    throw new InvalidOperationException("There were enchantments that were not processed by any enchantment type calculators.");
                }
            });

            var calculationResult = _enchantmentCalculatorResultFactory.Create(
                activeEnchantments,
                newStats);
            return calculationResult;
        }
    }
}