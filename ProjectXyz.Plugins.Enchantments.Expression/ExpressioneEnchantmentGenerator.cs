using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public sealed class ExpressioneEnchantmentGenerator : IExpressionEnchantmentGenerator
    {
        #region Fields
        private readonly IExpressionEnchantmentFactory _expressionEnchantmentFactory;
        private readonly IExpressionEnchantmentDefinitionRepository _expressionEnchantmentDefinitionRepository;
        private readonly IExpressionEnchantmentValueDefinitionRepository _expressionEnchantmentValueDefinitionRepository;
        private readonly IExpressionEnchantmentStatDefinitionRepository _expressionEnchantmentStatDefinitionRepository;
        private readonly IEnchantmentWeatherRepository _enchantmentWeatherRepository;
        #endregion

        #region Constructors
        private ExpressioneEnchantmentGenerator(
            IExpressionEnchantmentFactory expressionEnchantmentFactory,
            IExpressionEnchantmentDefinitionRepository expressionEnchantmentDefinitionRepository,
            IExpressionEnchantmentValueDefinitionRepository expressionEnchantmentValueDefinitionRepository,
            IExpressionEnchantmentStatDefinitionRepository expressionEnchantmentStatDefinitionRepository,
            IEnchantmentWeatherRepository enchantmentWeatherRepository)
        {
            _expressionEnchantmentFactory = expressionEnchantmentFactory;
            _expressionEnchantmentDefinitionRepository = expressionEnchantmentDefinitionRepository;
            _expressionEnchantmentValueDefinitionRepository = expressionEnchantmentValueDefinitionRepository;
            _expressionEnchantmentStatDefinitionRepository = expressionEnchantmentStatDefinitionRepository;
            _enchantmentWeatherRepository = enchantmentWeatherRepository;
        }
        #endregion

        #region Methods
        public static IExpressionEnchantmentGenerator Create(
            IExpressionEnchantmentFactory expressionEnchantmentFactory,
            IExpressionEnchantmentDefinitionRepository expressionEnchantmentDefinitionRepository,
            IExpressionEnchantmentValueDefinitionRepository expressionEnchantmentValueDefinitionRepository,
            IExpressionEnchantmentStatDefinitionRepository expressionEnchantmentStatDefinitionRepository,
            IEnchantmentWeatherRepository enchantmentWeatherRepository)
        {
            var generator = new ExpressioneEnchantmentGenerator(
                expressionEnchantmentFactory,
                expressionEnchantmentDefinitionRepository,
                expressionEnchantmentValueDefinitionRepository,
                expressionEnchantmentStatDefinitionRepository,
                enchantmentWeatherRepository);
            return generator;
        }

        public IExpressionEnchantment Generate(
            IRandom randomizer, 
            IEnchantmentDefinition enchantmentDefinition)
        {
            var expressionEnchantmentDefinition = _expressionEnchantmentDefinitionRepository.GetByEnchantmentDefinitionId(enchantmentDefinition.Id);
            var expressionEnchantmentValueDefinitions = _expressionEnchantmentValueDefinitionRepository.GetByEnchantmentDefinitionId(enchantmentDefinition.Id);
            var expressionEnchantmentStatDefinitions = _expressionEnchantmentStatDefinitionRepository.GetByEnchantmentDefinitionId(enchantmentDefinition.Id);
            var enchantmentWeather = _enchantmentWeatherRepository.GetById(enchantmentDefinition.EnchantmentWeatherId);

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
                enchantmentWeather.WeatherIds,
                duration,
                expressionEnchantmentDefinition.StatId,
                expressionEnchantmentDefinition.Expression,
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
