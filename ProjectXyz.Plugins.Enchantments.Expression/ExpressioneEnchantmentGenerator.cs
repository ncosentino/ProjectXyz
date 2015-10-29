using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Weather;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public sealed class ExpressioneEnchantmentGenerator : IExpressionEnchantmentGenerator
    {
        #region Fields
        private readonly IExpressionEnchantmentFactory _expressionEnchantmentFactory;
        private readonly IExpressionEnchantmentDefinitionRepository _expressionEnchantmentDefinitionRepository;
        private readonly IExpressionEnchantmentValueDefinitionRepository _expressionEnchantmentValueDefinitionRepository;
        private readonly IExpressionEnchantmentStatDefinitionRepository _expressionEnchantmentStatDefinitionRepository;
        private readonly IEnchantmentDefinitionWeatherTypeGroupingRepository _enchantmentDefinitionWeatherGroupingRepository;
        private readonly IExpressionDefinitionRepository _expressionDefinitionRepository;
        #endregion

        #region Constructors
        private ExpressioneEnchantmentGenerator(
            IExpressionEnchantmentFactory expressionEnchantmentFactory,
            IExpressionEnchantmentDefinitionRepository expressionEnchantmentDefinitionRepository,
            IExpressionEnchantmentValueDefinitionRepository expressionEnchantmentValueDefinitionRepository,
            IExpressionEnchantmentStatDefinitionRepository expressionEnchantmentStatDefinitionRepository,
            IExpressionDefinitionRepository expressionDefinitionRepository,
            IEnchantmentDefinitionWeatherTypeGroupingRepository enchantmentDefinitionWeatherGroupingRepository)
        {
            _expressionEnchantmentFactory = expressionEnchantmentFactory;
            _expressionEnchantmentDefinitionRepository = expressionEnchantmentDefinitionRepository;
            _expressionEnchantmentValueDefinitionRepository = expressionEnchantmentValueDefinitionRepository;
            _expressionEnchantmentStatDefinitionRepository = expressionEnchantmentStatDefinitionRepository;
            _expressionDefinitionRepository = expressionDefinitionRepository;
            _enchantmentDefinitionWeatherGroupingRepository = enchantmentDefinitionWeatherGroupingRepository;
        }
        #endregion

        #region Methods
        public static IExpressionEnchantmentGenerator Create(
            IExpressionEnchantmentFactory expressionEnchantmentFactory,
            IExpressionEnchantmentDefinitionRepository expressionEnchantmentDefinitionRepository,
            IExpressionEnchantmentValueDefinitionRepository expressionEnchantmentValueDefinitionRepository,
            IExpressionEnchantmentStatDefinitionRepository expressionEnchantmentStatDefinitionRepository,
            IExpressionDefinitionRepository expressionDefinitionRepository,
            IEnchantmentDefinitionWeatherTypeGroupingRepository enchantmentDefinitionWeatherGroupingRepository)
        {
            var generator = new ExpressioneEnchantmentGenerator(
                expressionEnchantmentFactory,
                expressionEnchantmentDefinitionRepository,
                expressionEnchantmentValueDefinitionRepository,
                expressionEnchantmentStatDefinitionRepository,
                expressionDefinitionRepository,
                enchantmentDefinitionWeatherGroupingRepository);
            return generator;
        }

        public IExpressionEnchantment Generate(
            IRandom randomizer, 
            IEnchantmentDefinition enchantmentDefinition)
        {
            var expressionEnchantmentDefinition = _expressionEnchantmentDefinitionRepository.GetByEnchantmentDefinitionId(enchantmentDefinition.Id);
            var expressionEnchantmentValueDefinitions = _expressionEnchantmentValueDefinitionRepository.GetByEnchantmentDefinitionId(enchantmentDefinition.Id);
            var expressionEnchantmentStatDefinitions = _expressionEnchantmentStatDefinitionRepository.GetByEnchantmentDefinitionId(enchantmentDefinition.Id);
            var expressionDefinition = _expressionDefinitionRepository.GetById(expressionEnchantmentDefinition.ExpressionId);
            var enchantmentWeather = _enchantmentDefinitionWeatherGroupingRepository.GetByEnchantmentDefinitionId(enchantmentDefinition.Id);
            
            var duration = GenerateDuration(
                randomizer,
                expressionEnchantmentDefinition);

            var expressionValues = GenerateEnchantmentValues(
                randomizer,
                expressionEnchantmentValueDefinitions);

            var expressionStats = GenerateEnchantmentStats(
                randomizer,
                expressionEnchantmentStatDefinitions);

            return _expressionEnchantmentFactory.Create(
                Guid.NewGuid(),
                enchantmentDefinition.StatusTypeId,
                enchantmentDefinition.TriggerId,
                enchantmentDefinition.EnchantmentTypeId,
                enchantmentWeather.WeatherTypeGroupingId,
                duration,
                expressionEnchantmentDefinition.StatId,
                expressionDefinition.Expression,
                expressionDefinition.CalculationPriority,
                expressionStats,
                expressionValues);
        }
        
        private TimeSpan GenerateDuration(
            IRandom randomizer,
            IExpressionEnchantmentDefinition expressionEnchantmentDefinition)
        {
            var duration = TimeSpan.FromMilliseconds(
                expressionEnchantmentDefinition.MinimumDuration.TotalMilliseconds +
                randomizer.NextDouble() * (expressionEnchantmentDefinition.MaximumDuration.TotalMilliseconds - expressionEnchantmentDefinition.MinimumDuration.TotalMilliseconds));
            return duration;
        }

        private IEnumerable<KeyValuePair<string, Guid>> GenerateEnchantmentStats(
            IRandom randomizer,
            IEnumerable<IExpressionEnchantmentStatDefinition> expressionEnchantmentStatDefinitions)
        {
            // TODO: do we ever want to do any randomization here?
            return expressionEnchantmentStatDefinitions
                .Select(x =>
                {
                    var kvp = new KeyValuePair<string, Guid>(
                        x.IdForExpression, 
                        x.StatId);
                    return kvp;
                });
        }

        private IEnumerable<KeyValuePair<string, double>> GenerateEnchantmentValues(
            IRandom randomizer,
            IEnumerable<IExpressionEnchantmentValueDefinition> expressionEnchantmentValueDefinitions)
        {
            return expressionEnchantmentValueDefinitions
                .Select(x =>
                {
                    var value =
                        x.MinimumValue +
                        randomizer.NextDouble() * (x.MaximumValue - x.MinimumValue);
                    var kvp = new KeyValuePair<string, double>(
                        x.IdForExpression, 
                        value);
                    return kvp;
                });
        }
        #endregion
    }
}
