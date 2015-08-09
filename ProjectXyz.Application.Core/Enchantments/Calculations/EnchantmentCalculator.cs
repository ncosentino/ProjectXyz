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



    public interface IEnchantmentTypeCalculator
    {
        #region Methods
        IEnchantmentTypeCalculatorResult Calculate(IStatCollection stats, IEnumerable<IEnchantment> enchantments);
        #endregion
    }

    public sealed class AdditiveEnchantmentTypeCalculator : IEnchantmentTypeCalculator
    {
        #region Fields
        private readonly IStatFactory _statFactory;
        #endregion

        #region Constructors
        private AdditiveEnchantmentTypeCalculator(IStatFactory statFactory)
        {
            _statFactory = statFactory;
        }
        #endregion

        #region Methods
        public static IEnchantmentTypeCalculator Create(IStatFactory statFactory)
        {
            return new AdditiveEnchantmentTypeCalculator(statFactory);
        }

        public IEnchantmentTypeCalculatorResult Calculate(IStatCollection stats, IEnumerable<IEnchantment> enchantments)
        {
            var newStats = StatCollection.Create();
            newStats.Add(stats);

            var processedEnchantments = new List<IEnchantment>(ProcessEnchantments(
                enchantments, 
                newStats));

            var result = EnchantmentTypeCalculatorResult.Create(
                Enumerable.Empty<IEnchantment>(),
                processedEnchantments,
                newStats);
            
            return result;
        }

        private IEnumerable<IEnchantment> ProcessEnchantments(IEnumerable<IEnchantment> enchantments, IMutableStatCollection stats)
        {
            foreach (var enchantment in enchantments.Where(x => x is IAdditiveEnchantment))
            {
                var additiveEnchantment = (IAdditiveEnchantment)enchantment;

                var oldValue = stats.GetValueOrDefault(additiveEnchantment.StatId, 0);
                var newValue = oldValue + additiveEnchantment.Value;
                var newStat = _statFactory.CreateStat(additiveEnchantment.StatId, newValue);
                stats[additiveEnchantment.StatId] = newStat;

                yield return enchantment;
            }
        }
        #endregion
    }
}
