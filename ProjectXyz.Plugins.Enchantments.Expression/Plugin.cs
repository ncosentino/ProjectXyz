using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Weather;
using ProjectXyz.Data.Sql;
using ProjectXyz.Plugins.Enchantments.Expression.Sql;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public sealed class Plugin : IEnchantmentPlugin
    {
        #region Fields
        private readonly IExpressionEnchantmentStoreRepository _expressionEnchantmentStoreRepository;
        private readonly IExpressionEnchantmentStatRepository _expressionEnchantmentStatRepository;
        private readonly IExpressionEnchantmentValueRepository _expressionEnchantmentValueRepository;
        private readonly IEnchantmentDefinitionRepository _enchantmentDefinitionRepository;
        private readonly IEnchantmentTypeCalculator _enchantmentTypeCalculator;
        private readonly IExpressionEnchantmentGenerator _expressionEnchantmentGenerator;
        private readonly IExpressionEnchantmentStoreFactory _expressionEnchantmentStoreFactory;
        private readonly IExpressionEnchantmentStatFactory _expressionEnchantmentStatFactory;
        private readonly IExpressionEnchantmentValueFactory _expressionEnchantmentValueFactory;
        private readonly IExpressionEnchantmentFactory _expressionEnchantmentFactory;
        private readonly IExpressionDefinitionFactory _expressionDefinitionFactory;
        private readonly IExpressionDefinitionRepository _expressionDefinitionRepository;
        #endregion

        #region Constructors
        public Plugin(
            IDatabase database,
            IDataManager dataManager)
        {
            _enchantmentDefinitionRepository = dataManager.Enchantments.EnchantmentDefinitions;

            var stringExpressionEvaluator = DataTableExpressionEvaluator.Create();
            var expressionEvaluator = ExpressionEvaluator.Create(stringExpressionEvaluator.Evaluate);

            _expressionDefinitionFactory = ExpressionDefinitionFactory.Create();
            _expressionDefinitionRepository = ExpressionDefinitionRepository.Create(
                database,
                _expressionDefinitionFactory);

            _enchantmentTypeCalculator = ExpressionEnchantmentTypeCalculator.Create(
                dataManager.Stats.StatFactory,
                expressionEvaluator,
                dataManager.Weather.WeatherGroupings);

            _expressionEnchantmentStoreFactory = ExpressionEnchantmentStoreFactory.Create();

            _expressionEnchantmentStoreRepository = ExpressionEnchantmentStoreRepository.Create(
                database,
                _expressionEnchantmentStoreFactory);

            _expressionEnchantmentStatFactory = ExpressionEnchantmentStatFactory.Create();

            _expressionEnchantmentStatRepository = ExpressionEnchantmentStatRepository.Create(
                database,
                _expressionEnchantmentStatFactory);

            _expressionEnchantmentValueFactory = ExpressionEnchantmentValueFactory.Create();

            _expressionEnchantmentValueRepository = ExpressionEnchantmentValueRepository.Create(
                database,
                _expressionEnchantmentValueFactory);

            _expressionEnchantmentFactory = ExpressionEnchantmentFactory.Create();

            var enchantmentFactory = ExpressionEnchantmentFactory.Create();

            var expressionEnchantmentDefinitionFactory = ExpressionEnchantmentDefinitionFactory.Create();
            var expressionEnchantmentDefinitioneRepository = ExpressionEnchantmentDefinitionRepository.Create(
                database, 
                expressionEnchantmentDefinitionFactory);

            var expressionEnchantmentValueDefinitionFactory = ExpressionEnchantmentValueDefinitionFactory.Create();
            var expressionEnchantmentValueDefinitionRepository = ExpressionEnchantmentValueDefinitionRepository.Create(
                database, 
                expressionEnchantmentValueDefinitionFactory);

            var expressionEnchantmentStatDefinitionFactory = ExpressionEnchantmentStatDefinitionFactory.Create();
            var expressionEnchantmentStatDefinitionRepository = ExpressionEnchantmentStatDefinitionRepository.Create(
                database,
                expressionEnchantmentStatDefinitionFactory);

            var enchantmentDefinitionWeatherGroupingRepository = dataManager.Enchantments.EnchantmentWeather;
            _expressionEnchantmentGenerator = ExpressioneEnchantmentGenerator.Create(
                enchantmentFactory,
                expressionEnchantmentDefinitioneRepository,
                expressionEnchantmentValueDefinitionRepository,
                expressionEnchantmentStatDefinitionRepository,
                _expressionDefinitionRepository,
                enchantmentDefinitionWeatherGroupingRepository);
        }
        #endregion

        #region Properties
        public Type EnchantmentStoreType
        {
            get { return typeof(IExpressionEnchantmentStore); }
        }

        public Type EnchantmentDefinitionType
        {
            get { return typeof(IExpressionEnchantmentDefinition); }
        }

        public Type EnchantmentType
        {
            get { return typeof(IExpressionEnchantment); }
        }

        public IEnchantmentTypeCalculator EnchantmentTypeCalculator
        {
            get { return _enchantmentTypeCalculator; }
        }

        public CreateEnchantmentDelegate CreateEnchantmentCallback
        {
            get { return CreateEnchantment; }
        }

        public SaveEnchantmentDelegate SaveEnchantmentCallback
        {
            get { return SaveEnchantment; }
        }

        public GenerateEnchantmentDelegate GenerateEnchantmentCallback
        {
            get { return GenerateEnchantment; }
        }
        #endregion

        #region Methods
        private IEnchantment GenerateEnchantment(
            IRandom randomizer, 
            Guid enchantmentDefinitionId)
        {
            var enchantmentDefinition = _enchantmentDefinitionRepository.GetById(enchantmentDefinitionId);
            var enchantment = _expressionEnchantmentGenerator.Generate(
                randomizer,
                enchantmentDefinition);
            return enchantment;
        }

        private void SaveEnchantment(IEnchantment enchantment)
        {
            if (!(enchantment is IExpressionEnchantment))
            {
                throw new InvalidOperationException(string.Format(
                    "Cannot save '{0}' to an expression enchantment store.",
                    enchantment.GetType()));
            }

            var expressionEnchantment = (IExpressionEnchantment)enchantment;

            foreach (var statExpressionId in expressionEnchantment.StatExpressionIds)
            {
                var expressionEnchantmentStat = _expressionEnchantmentStatFactory.CreateExpressionEnchantmentStat(
                    Guid.NewGuid(), // FIXME: if we want to update... we need the original id??
                    expressionEnchantment.Id,
                    statExpressionId,
                    expressionEnchantment.GetStatIdForStatExpressionId(statExpressionId));


                    // FIXME: we need add or update?
                    _expressionEnchantmentStatRepository.Add(expressionEnchantmentStat);
            }

            foreach (var valueExpressionId in expressionEnchantment.ValueExpressionIds)
            {
                var expressionEnchantmentValue = _expressionEnchantmentValueFactory.CreateExpressionEnchantmentValue(
                    Guid.NewGuid(), // FIXME: if we want to update... we need the original id??
                    expressionEnchantment.Id,
                    valueExpressionId,
                    expressionEnchantment.GetValueForValueExpressionId(valueExpressionId));


                // FIXME: we need add or update?
                _expressionEnchantmentValueRepository.Add(expressionEnchantmentValue);
            }

            var expressionDefinition = _expressionDefinitionFactory.CreateExpressionDefinition(
                Guid.NewGuid(), // FIXME: if we want to update... we need the original id??
                expressionEnchantment.Expression,
                expressionEnchantment.CalculationPriority);
            _expressionDefinitionRepository.Add(expressionDefinition);

            var enchantmentStore = _expressionEnchantmentStoreFactory.CreateEnchantmentStore(
                expressionEnchantment.Id,
                expressionEnchantment.StatId,
                expressionDefinition.Id,
                expressionEnchantment.RemainingDuration);
            
            // FIXME: we need add or update?
            _expressionEnchantmentStoreRepository.Add(enchantmentStore);
        }

        private IEnchantment CreateEnchantment(IEnchantmentStore enchantmentStore)
        {
            var expressionEnchantmentStore = _expressionEnchantmentStoreRepository.GetById(enchantmentStore.Id);

            var expressionStatIds = _expressionEnchantmentStatRepository
                .GetByExpressionEnchantmentId(enchantmentStore.Id)
                .Select(x => new KeyValuePair<string, Guid>(x.IdForExpression, x.StatId));

            var expressionValues = _expressionEnchantmentValueRepository
                .GetByExpressionEnchantmentId(enchantmentStore.Id)
                .Select(x => new KeyValuePair<string, double>(x.IdForExpression, x.Value));

            var expressionDefinition = _expressionDefinitionRepository.GetById(expressionEnchantmentStore.ExpressionId);
            
            var expressionEnchantment = _expressionEnchantmentFactory.Create(
                enchantmentStore.Id,
                enchantmentStore.StatusTypeId,
                enchantmentStore.TriggerId,
                enchantmentStore.WeatherGroupingId,
                expressionEnchantmentStore.RemainingDuration,
                expressionEnchantmentStore.StatId,
                expressionDefinition.Expression,
                expressionDefinition.CalculationPriority,
                expressionStatIds,
                expressionValues);
            
            return expressionEnchantment;
        }
        #endregion
    }
}
