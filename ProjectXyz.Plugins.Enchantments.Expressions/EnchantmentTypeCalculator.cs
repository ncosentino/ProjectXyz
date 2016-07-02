using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Application.Interface.Weather;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;

namespace ProjectXyz.Plugins.Enchantments.Expressions
{
    public sealed class EnchantmentTypeCalculator : IEnchantmentTypeCalculator
    {
        #region Fields
        private readonly IStatFactory _statFactory;
        private readonly IExpressionEvaluator _expressionEvaluator;
        private readonly IEnchantmentTypeCalculatorResultFactory _enchantmentTypeCalculatorResultFactory;
        private readonly IStatCollectionFactory _statCollectionFactory;
        private readonly IWeatherManager _weatherManager;
        #endregion

        #region Constructors
        public EnchantmentTypeCalculator(
            IStatFactory statFactory,
            IExpressionEvaluator expressionEvaluator,
            IEnchantmentTypeCalculatorResultFactory enchantmentTypeCalculatorResultFactory,
            IStatCollectionFactory statCollectionFactory,
            IWeatherManager weatherManager)
        {
            _statFactory = statFactory;
            _expressionEvaluator = expressionEvaluator;
            _enchantmentTypeCalculatorResultFactory = enchantmentTypeCalculatorResultFactory;
            _statCollectionFactory = statCollectionFactory;
            _weatherManager = weatherManager;
        }
        #endregion

        #region Methods
        public IEnchantmentTypeCalculatorResult Calculate(
            IEnchantmentContext enchantmentContext,
            IStatCollection stats,
            IEnumerable<IEnchantment> enchantments)
        {
            var processResult = ProcessEnchantments(
                enchantmentContext,
                enchantments,
                stats);
            var newStats = _statCollectionFactory.Create(processResult.Item2);
            var processedEnchantments = processResult.Item1;

            var result = _enchantmentTypeCalculatorResultFactory.Create(
                new IEnchantment[0],
                new IEnchantment[0],
                processedEnchantments,
                newStats);
            return result;
        }

        private Tuple<List<IEnchantment>, List<IStat>>  ProcessEnchantments(
            IEnchantmentContext enchantmentContext,
            IEnumerable<IEnchantment> enchantments,
            IStatCollection stats)
        {
            var processedEnchantments = new List<IEnchantment>();
            var newStats = stats.ToDictionary();

            var activeEnchantments = GetActiveExpressionEnchantments(
                enchantmentContext,
                enchantments);

            foreach (var enchantment in activeEnchantments)
            {
                var newValue = _expressionEvaluator.Evaluate(
                    enchantment,
                    newStats);
                var newStat = _statFactory.Create(
                    enchantment.StatDefinitionId,
                    newValue);

                newStats[newStat.StatDefinitionId] = newStat;
                processedEnchantments.Add(enchantment);
            }

            return new Tuple<List<IEnchantment>, List<IStat>>(
                processedEnchantments,
                newStats.Values.ToList());
        }

        private IEnumerable<IExpressionEnchantment> GetActiveExpressionEnchantments(
            IEnchantmentContext enchantmentContext,
            IEnumerable<IEnchantment> enchantments)
        {
            return enchantments
                .TakeTypes<IExpressionEnchantment>()
                .Where(x => _weatherManager.WeatherGroupingContainsWeatherDefinition(
                    x.WeatherGroupingId,
                    enchantmentContext.ActiveWeatherDefinitionId))
                .OrderBy(x => x.CalculationPriority);
        }
        #endregion
    }
}
