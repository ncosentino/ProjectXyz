using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Interface.Enchantments;
using ProjectXyz.Interface.Stats;
using ProjectXyz.Core.Stats;
using ProjectXyz.Core.Enchantments;

namespace ProjectXyz.Core.Enchantments
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
            return new EnchantmentCalculator();
        }

        public IStatCollection<IStat> Calculate<TStat>(
            IStatCollection<TStat> stats, 
            IEnchantmentCollection enchantments) 
            where TStat : IStat
        {
            var newStats = MutableStatCollection<IStat>.Create();
            foreach (var stat in stats)
            {
                newStats.Add(stat);
            }

            foreach (var calculationType in CALCULATION_ORDER)
            {
                PerEnchantment(
                    stats,
                    newStats,
                    enchantments.EnchantmentsCalculatedBy(calculationType));
            }

            return newStats;
        }

        private void PerEnchantment<TStat>(
            IStatCollection<TStat> stats,
            IMutableStatCollection<IStat> newStats, 
            IEnumerable<IEnchantment> enchantments) where TStat : IStat
        {
            foreach (var enchantment in enchantments)
            {
                if (!newStats.Contains(enchantment.StatId))
                {
                    continue;
                }

                var oldValue = newStats[enchantment.StatId].Value;
                var newValue = _calculationMappings[enchantment.CalculationId](
                    oldValue,
                    enchantment.Value);
                newStats.Set(ReadonlyStat.Create(enchantment.StatId, newValue));
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
