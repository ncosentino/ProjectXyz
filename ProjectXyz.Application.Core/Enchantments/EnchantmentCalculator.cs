using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Stats.ExtensionMethods;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Core.Enchantments;

namespace ProjectXyz.Application.Core.Enchantments
{
    public class EnchantmentCalculator : IEnchantmentCalculator
    {
        #region Constants
        private static readonly IEnumerable<string> CALCULATION_ORDER = new[]
        {
            EnchantmentCalculationTypes.Value,
            EnchantmentCalculationTypes.Percent,
        };

        private static readonly Dictionary<string, string> STATUS_NEGATIONS = new Dictionary<string,string>()
        {
            { ActorStats.Bless, EnchantmentStatuses.Curse },
            { ActorStats.Cure, EnchantmentStatuses.Disease },
        };
        #endregion

        #region Fields
        private readonly Dictionary<string, Func<double, double, double>> _calculationMappings;
        #endregion

        #region Constructors
        private EnchantmentCalculator()
        {
            _calculationMappings = new Dictionary<string, Func<double, double, double>>();
            _calculationMappings[EnchantmentCalculationTypes.Value] = CalculateValue;
            _calculationMappings[EnchantmentCalculationTypes.Percent] = CalculatePercent;
        }
        #endregion

        #region Methods
        public static IEnchantmentCalculator Create()
        {
            Contract.Ensures(Contract.Result<IEnchantmentCalculator>() != null);
            return new EnchantmentCalculator();
        }

        public IStatCollection Calculate(IStatCollection stats, IEnumerable<IEnchantment> enchantments) 
        {
            var newStats = StatCollection.Create();
            newStats.Add(stats);

            var activeNegations = new Dictionary<string, bool>();
            foreach (var kvp in STATUS_NEGATIONS)
            {
                activeNegations[kvp.Value] = enchantments.Any(x => x.StatId == kvp.Key);
            }

            foreach (var calculationType in CALCULATION_ORDER)
            {
                foreach (var enchantment in enchantments.CalculatedBy(calculationType))
                {
                    if (activeNegations.ContainsKey(enchantment.StatusType) &&
                        activeNegations[enchantment.StatusType])
                    {
                        continue;
                    }

                    var oldValue = newStats.GetValueOrDefault(enchantment.StatId, 0);
                    var newValue = _calculationMappings[enchantment.CalculationId](
                        oldValue,
                        enchantment.Value);
                    newStats.Set(Stat.Create(
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
