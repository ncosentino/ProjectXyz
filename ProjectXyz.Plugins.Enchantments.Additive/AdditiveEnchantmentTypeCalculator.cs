using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Core.Enchantments.Calculations;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Stats.ExtensionMethods;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
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

        public IEnchantmentTypeCalculatorResult Calculate(
            IEnchantmentContext enchantmentContext,
            IStatCollection stats,
            IEnumerable<IEnchantment> enchantments)
        {
            var newStats = StatCollection.Create();
            newStats.Add(stats);

            var processedEnchantments = new List<IEnchantment>(ProcessEnchantments(
                enchantmentContext,
                enchantments,
                newStats));

            var result = EnchantmentTypeCalculatorResult.Create(
                Enumerable.Empty<IEnchantment>(),
                processedEnchantments,
                newStats);

            return result;
        }

        private IEnumerable<IEnchantment> ProcessEnchantments(
            IEnchantmentContext enchantmentContext, 
            IEnumerable<IEnchantment> enchantments, 
            IMutableStatCollection stats)
        {
            foreach (var enchantment in enchantments
                .Where(x => x is IAdditiveEnchantment && (!x.WeatherIds.Any() || x.WeatherIds.Any(e => e == enchantmentContext.ActiveWeatherId)))
                .Cast<IAdditiveEnchantment>())
            {
                var oldValue = stats.GetValueOrDefault(enchantment.StatId, 0);
                var newValue = oldValue + enchantment.Value;
                var newStat = _statFactory.CreateStat(enchantment.StatId, newValue);
                stats[enchantment.StatId] = newStat;

                yield return enchantment;
            }
        }
        #endregion
    }
}
