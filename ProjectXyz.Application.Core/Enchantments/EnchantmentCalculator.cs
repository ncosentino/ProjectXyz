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
            foreach (var stat in stats)
            {
                if (stat == null)
                {
                    throw new NullReferenceException("Stats within the provided enumerable cannot be null.");
                }

                newStats.Add(stat);
            }

            foreach (var calculationType in CALCULATION_ORDER)
            {
                PerEnchantment(
                    stats,
                    newStats,
                    enchantments.CalculatedBy(calculationType));
            }

            return newStats;
        }

        private void PerEnchantment(IStatCollection stats, IMutableStatCollection newStats, IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);

            foreach (var enchantment in enchantments)
            {
                var oldValue = newStats.GetValueOrDefault(enchantment.StatId, 0);
                var newValue = _calculationMappings[enchantment.CalculationId](
                    oldValue,
                    enchantment.Value);
                newStats.Set(Stat.Create(
                    enchantment.StatId, 
                    newValue));
            }
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
