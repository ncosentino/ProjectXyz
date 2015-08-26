using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Core.Enchantments.Calculations;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Stats.ExtensionMethods;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public sealed class ExpressionEnchantmentTypeCalculator : IEnchantmentTypeCalculator
    {
        #region Fields
        private readonly IStatFactory _statFactory;
        private readonly IExpressionEvaluator _expressioNEvaluator;
        #endregion

        #region Constructors
        private ExpressionEnchantmentTypeCalculator(
            IStatFactory statFactory,
            IExpressionEvaluator expressioNEvaluator)
        {
            _statFactory = statFactory;
            _expressioNEvaluator = expressioNEvaluator;
        }
        #endregion

        #region Methods
        public static IEnchantmentTypeCalculator Create(
            IStatFactory statFactory,
            IExpressionEvaluator expressionEvaluator)
        {
            return new ExpressionEnchantmentTypeCalculator(
                statFactory,
                expressionEvaluator);
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
            foreach (var enchantment in GetActiveExpressionEnchantments(enchantmentContext, enchantments))
            {
                var newValue = _expressioNEvaluator.Evaluate(
                    enchantment,
                    stats);
                var newStat = _statFactory.CreateStat(enchantment.StatId, newValue);
                stats[enchantment.StatId] = newStat;

                yield return enchantment;
            }
        }

        private IEnumerable<IExpressionEnchantment> GetActiveExpressionEnchantments(
            IEnchantmentContext enchantmentContext,
            IEnumerable<IEnchantment> enchantments)
        {
            return enchantments
                .Where(x => x is IExpressionEnchantment && (!x.WeatherIds.Any() || x.WeatherIds.Any(e => e == enchantmentContext.ActiveWeatherId)))
                .Cast<IExpressionEnchantment>()
                .OrderBy(x => x.CalculationPriority);
        }
        #endregion
    }
}
