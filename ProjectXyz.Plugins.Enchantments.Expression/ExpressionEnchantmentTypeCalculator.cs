using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Core.Enchantments.Calculations;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Stats.ExtensionMethods;
using ProjectXyz.Data.Interface.Weather;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public sealed class ExpressionEnchantmentTypeCalculator : IEnchantmentTypeCalculator
    {
        #region Fields
        private readonly IStatFactory _statFactory;
        private readonly IExpressionEvaluator _expressioNEvaluator;
        private readonly IWeatherGroupingRepository _weatherGroupingRepository;
        #endregion

        #region Constructors
        private ExpressionEnchantmentTypeCalculator(
            IStatFactory statFactory,
            IExpressionEvaluator expressionEvaluator,
            IWeatherGroupingRepository weatherGroupingRepository)
        {
            _statFactory = statFactory;
            _expressioNEvaluator = expressionEvaluator;
            _weatherGroupingRepository = weatherGroupingRepository;
        }
        #endregion

        #region Methods
        public static IEnchantmentTypeCalculator Create(
            IStatFactory statFactory,
            IExpressionEvaluator expressionEvaluator,
            IWeatherGroupingRepository weatherGroupingRepository)
        {
            return new ExpressionEnchantmentTypeCalculator(
                statFactory,
                expressionEvaluator,
                weatherGroupingRepository);
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
            var activeEnchantments = GetActiveExpressionEnchantments(
                _weatherGroupingRepository,
                enchantmentContext,
                enchantments);

            foreach (var enchantment in activeEnchantments)
            {
                var newValue = _expressioNEvaluator.Evaluate(
                    enchantment,
                    stats);
                var newStat = _statFactory.Create(
                    Guid.NewGuid(), 
                    enchantment.StatId, 
                    newValue);
                stats[enchantment.StatId] = newStat;

                yield return enchantment;
            }
        }

        private IEnumerable<IExpressionEnchantment> GetActiveExpressionEnchantments(
            IWeatherGroupingRepository weatherGroupingRepository,
            IEnchantmentContext enchantmentContext,
            IEnumerable<IEnchantment> enchantments)
        {
            return enchantments
                .Where(x =>
                {
                    var weatherGroupings = weatherGroupingRepository
                        .GetByGroupingId(x.WeatherGroupingId)
                        .ToArray();
                    return x is IExpressionEnchantment && (!weatherGroupings.Any() || weatherGroupings.Any(e => e.WeatherId == enchantmentContext.ActiveWeatherId));
                })
                .Cast<IExpressionEnchantment>()
                .OrderBy(x => x.CalculationPriority);
        }
        #endregion
    }
}
