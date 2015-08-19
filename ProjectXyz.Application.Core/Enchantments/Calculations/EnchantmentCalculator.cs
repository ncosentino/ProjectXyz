using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Core.Enchantments.Calculations
{
    public sealed class EnchantmentCalculator : IEnchantmentCalculator
    {
        #region Fields
        private readonly IEnchantmentContext _enchantmentContext;
        private readonly IEnchantmentCalculatorResultFactory _enchantmentCalculatorResultFactory;
        private readonly List<IEnchantmentTypeCalculator> _enchantmentTypeCalculators;
        #endregion

        #region Constructors
        private EnchantmentCalculator(
            IEnchantmentContext enchantmentContext,
            IEnchantmentCalculatorResultFactory enchantmentCalculatorResultFactory,
            IEnumerable<IEnchantmentTypeCalculator> enchantmentTypeCalculators)
        {
            Contract.Requires<ArgumentNullException>(enchantmentContext != null);
            Contract.Requires<ArgumentNullException>(enchantmentCalculatorResultFactory != null);
            Contract.Requires<ArgumentNullException>(enchantmentTypeCalculators != null);

            _enchantmentContext = enchantmentContext;
            _enchantmentCalculatorResultFactory = enchantmentCalculatorResultFactory;
            _enchantmentTypeCalculators = new List<IEnchantmentTypeCalculator>(enchantmentTypeCalculators);
        }
        #endregion

        #region Methods
        public static IEnchantmentCalculator Create(
            IEnchantmentContext enchantmentContext,
            IEnchantmentCalculatorResultFactory enchantmentCalculatorResultFactory,
            IEnumerable<IEnchantmentTypeCalculator> enchantmentTypeCalculators)
        {
            Contract.Requires<ArgumentNullException>(enchantmentContext != null);
            Contract.Requires<ArgumentNullException>(enchantmentCalculatorResultFactory != null);
            Contract.Requires<ArgumentNullException>(enchantmentTypeCalculators != null);
            Contract.Ensures(Contract.Result<IEnchantmentCalculator>() != null);

            return new EnchantmentCalculator(
                enchantmentContext,
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
                    _enchantmentContext,
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
