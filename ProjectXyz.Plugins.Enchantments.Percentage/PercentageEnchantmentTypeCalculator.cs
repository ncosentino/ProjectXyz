using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Core.Enchantments.Calculations;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Stats.ExtensionMethods;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    public sealed class PercentageEnchantmentTypeCalculator : IEnchantmentTypeCalculator
    {
        #region Fields
        private readonly IStatFactory _statFactory;
        #endregion

        #region Constructors
        private PercentageEnchantmentTypeCalculator(IStatFactory statFactory)
        {
            _statFactory = statFactory;
        }
        #endregion

        #region Methods
        public static IEnchantmentTypeCalculator Create(IStatFactory statFactory)
        {
            var enchantmentTypeCalculator = new PercentageEnchantmentTypeCalculator(statFactory);
            return enchantmentTypeCalculator;
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
            foreach (var enchantment in enchantments.Where(x => x is IPercentageEnchantment))
            {
                var additiveEnchantment = (IPercentageEnchantment)enchantment;

                var oldValue = stats.GetValueOrDefault(additiveEnchantment.StatId, 0);
                var newValue = oldValue * (1 + additiveEnchantment.Value);
                var newStat = _statFactory.CreateStat(additiveEnchantment.StatId, newValue);
                stats[additiveEnchantment.StatId] = newStat;

                yield return enchantment;
            }
        }
        #endregion
    }
}
