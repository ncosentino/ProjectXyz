using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Stats.ExtensionMethods;

namespace ProjectXyz.Application.Core.Enchantments.Calculations
{
    public sealed class EnchantmentCalculator : IEnchantmentCalculator
    {
        #region Fields
        private readonly IEnchantmentCalculatorResultFactory _enchantmentCalculatorResultFactory;
        private readonly List<IEnchantmentTypeCalculator> _enchantmentTypeCalculators;
        #endregion

        #region Constructors
        private EnchantmentCalculator(
            IEnchantmentCalculatorResultFactory enchantmentCalculatorResultFactory,
            IEnumerable<IEnchantmentTypeCalculator> enchantmentTypeCalculators)
        {
            Contract.Requires<ArgumentNullException>(enchantmentCalculatorResultFactory != null);
            Contract.Requires<ArgumentNullException>(enchantmentTypeCalculators != null);

            _enchantmentCalculatorResultFactory = enchantmentCalculatorResultFactory;
            _enchantmentTypeCalculators = new List<IEnchantmentTypeCalculator>(enchantmentTypeCalculators);
        }
        #endregion

        #region Methods
        public static IEnchantmentCalculator Create(
            IEnchantmentCalculatorResultFactory enchantmentCalculatorResultFactory,
            IEnumerable<IEnchantmentTypeCalculator> enchantmentTypeCalculators)
        {
            Contract.Requires<ArgumentNullException>(enchantmentCalculatorResultFactory != null);
            Contract.Requires<ArgumentNullException>(enchantmentTypeCalculators != null);
            Contract.Ensures(Contract.Result<IEnchantmentCalculator>() != null);

            return new EnchantmentCalculator(
                enchantmentCalculatorResultFactory, 
                enchantmentTypeCalculators);
        }

        public IEnchantmentCalculatorResult Calculate(IStatCollection stats, IEnumerable<IEnchantment> enchantments) 
        {
            var statsSeed = StatCollection.Create();
            statsSeed.Add(stats);
            IStatCollection newStats = statsSeed;

            var enchantmentsToBeProcessed = enchantments.ToArray();
            var activeEnchantments = enchantmentsToBeProcessed.ToArray();

            foreach (var enchantmentTypeCalculator in _enchantmentTypeCalculators)
            {
                var result = enchantmentTypeCalculator.Calculate(
                    newStats, 
                    activeEnchantments);

                enchantmentsToBeProcessed = enchantmentsToBeProcessed
                    .Except(result.ProcessedEnchantments)
                    .Except(result.RemovedEnchantments)
                    .ToArray();

                activeEnchantments = activeEnchantments
                    .Except(result.RemovedEnchantments)
                    .ToArray();

                newStats = result.Stats;
            }

            var calculationResult = _enchantmentCalculatorResultFactory.Create(
                activeEnchantments,
                newStats);
            return calculationResult;
        }
        #endregion
    }
}
