using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Stats.ExtensionMethods;

namespace ProjectXyz.Application.Core.Enchantments
{
    public class EnchantmentCalculator : IEnchantmentCalculator
    {
        #region Constants
        private static readonly IEnumerable<Guid> CALCULATION_ORDER = new[]
        {
            EnchantmentCalculationTypes.Value,
            EnchantmentCalculationTypes.Percent,
        };
        #endregion

        #region Fields
        private readonly Dictionary<Guid, Func<double, double, double>> _calculationMappings;
        private readonly IStatFactory _statFactory;
        private readonly IStatusNegationRepository _statusNegationRepository;
        #endregion

        #region Constructors
        private EnchantmentCalculator(
            IStatFactory statFactory,
            IStatusNegationRepository statusNegationRepository)
        {
            Contract.Requires<ArgumentNullException>(statFactory != null);
            Contract.Requires<ArgumentNullException>(statusNegationRepository != null);
            
            _statFactory = statFactory;
            _statusNegationRepository = statusNegationRepository;

            _calculationMappings = new Dictionary<Guid, Func<double, double, double>>();
            _calculationMappings[EnchantmentCalculationTypes.Value] = CalculateValue;
            _calculationMappings[EnchantmentCalculationTypes.Percent] = CalculatePercent;
        }
        #endregion

        #region Methods
        public static IEnchantmentCalculator Create(IStatFactory statFactory,
            IStatusNegationRepository statusNegationRepository)
        {
            Contract.Requires<ArgumentNullException>(statFactory != null);
            Contract.Requires<ArgumentNullException>(statusNegationRepository != null);
            Contract.Ensures(Contract.Result<IEnchantmentCalculator>() != null);

            return new EnchantmentCalculator(
                statFactory,
                statusNegationRepository);
        }

        public IStatCollection Calculate(IStatCollection stats, IEnumerable<IEnchantment> enchantments) 
        {
            var newStats = StatCollection.Create();
            newStats.Add(stats);

            var activeNegations = new Dictionary<Guid, bool>();
            foreach (var statusNegation in _statusNegationRepository.GetAll())
            {
                activeNegations[statusNegation.EnchantmentStatusId] = enchantments.Any(x => x.StatId == statusNegation.StatId);
            }

            foreach (var calculationType in CALCULATION_ORDER)
            {
                foreach (var enchantment in enchantments.CalculatedBy(calculationType))
                {
                    if (activeNegations.ContainsKey(enchantment.StatusTypeId) &&
                        activeNegations[enchantment.StatusTypeId])
                    {
                        continue;
                    }

                    var oldValue = newStats.GetValueOrDefault(enchantment.StatId, 0);
                    var newValue = _calculationMappings[enchantment.CalculationId](
                        oldValue,
                        enchantment.Value);
                    newStats.Set(_statFactory.CreateStat(
                        enchantment.StatId, 
                        newValue));
                }
            }

            return newStats;
        }
        
        private double CalculateValue(double statValue, double enchantmentValue)
        {
            return statValue + enchantmentValue;
        }

        private double CalculatePercent(double statValue, double enchantmentValue)
        {
            return statValue * (1 + enchantmentValue);
        }
        #endregion
    }
}
