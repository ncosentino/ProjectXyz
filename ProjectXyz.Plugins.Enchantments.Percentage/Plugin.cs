using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Sql;
using ProjectXyz.Plugins.Enchantments.Percentage.Sql;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    public sealed class Plugin : IEnchantmentPlugin
    {
        #region Fields
        private readonly IEnchantmentWeatherRepository _enchantmentWeatherRepository;
        private readonly IPercentageEnchantmentStoreRepository _percentageEnchantmentStoreRepository;
        private readonly IEnchantmentDefinitionRepository _enchantmentDefinitionRepository;
        private readonly IPercentageEnchantmentDefinitionRepository _percentageEnchantmentDefinitioneRepository;
        private readonly IEnchantmentTypeCalculator _enchantmentTypeCalculator;
        private readonly IPercentageEnchantmentGenerator _percentageEnchantmentGenerator;
        private readonly IPercentageEnchantmentStoreFactory _percentageEnchantmentStoreFactory;
        private readonly IPercentageEnchantmentFactory _percentageEnchantmentFactory;
        #endregion

        #region Constructors
        public Plugin(
            IDatabase database,
            IEnchantmentDefinitionRepository enchantmentDefinitionRepository,
            IEnchantmentWeatherRepository enchantmentWeatherRepository)
        {

            _enchantmentDefinitionRepository = enchantmentDefinitionRepository;
            _enchantmentWeatherRepository = enchantmentWeatherRepository;

            var statFactory = StatFactory.Create();
            _enchantmentTypeCalculator = PercentageEnchantmentTypeCalculator.Create(statFactory);

            var enchantmentFactory = PercentageEnchantmentFactory.Create();
            _percentageEnchantmentGenerator = PercentageEnchantmentGenerator.Create(
                enchantmentFactory,
                enchantmentWeatherRepository);

            _percentageEnchantmentStoreFactory = PercentageEnchantmentStoreFactory.Create();

            _percentageEnchantmentStoreRepository = PercentageEnchantmentStoreRepository.Create(
                database,
                _percentageEnchantmentStoreFactory);

            _percentageEnchantmentFactory = PercentageEnchantmentFactory.Create();

            var percentageEnchantmentDefinitionFactory = PercentageEnchantmentDefinitionFactory.Create();
            _percentageEnchantmentDefinitioneRepository = PercentageEnchantmentDefinitionRepository.Create(database, percentageEnchantmentDefinitionFactory);
        }
        #endregion

        #region Properties
        public Type EnchantmentStoreType
        {
            get { return typeof(IPercentageEnchantmentStore); }
        }

        public Type EnchantmentDefinitionType
        {
            get { return typeof(IPercentageEnchantmentDefinition); }
        }

        public Type EnchantmentType
        {
            get { return typeof(IPercentageEnchantment); }
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
            var percentageEnchantmentDefinition = _percentageEnchantmentDefinitioneRepository.GetById(enchantmentDefinitionId);
            var enchantment = _percentageEnchantmentGenerator.Generate(
                randomizer,
                enchantmentDefinition,
                percentageEnchantmentDefinition);
            return enchantment;
        }

        private void SaveEnchantment(IEnchantment enchantment)
        {
            if (!(enchantment is IPercentageEnchantment))
            {
                throw new InvalidOperationException(string.Format(
                    "Cannot save '{0}' to an percentage enchantment store.",
                    enchantment.GetType()));
            }

            var percentageEnchantment = (IPercentageEnchantment)enchantment;

            var enchantmentStore = _percentageEnchantmentStoreFactory.CreateEnchantmentStore(
                percentageEnchantment.Id,
                percentageEnchantment.StatId,
                percentageEnchantment.Value);
            
            // FIXME: we need add or update?
            _percentageEnchantmentStoreRepository.Add(enchantmentStore);
        }

        private IEnchantment CreateEnchantment(IEnchantmentStore enchantmentStore)
        {
            var percentageEnchantmentStore = _percentageEnchantmentStoreRepository.GetById(enchantmentStore.Id);
            if (percentageEnchantmentStore == null)
            {
                throw new InvalidOperationException(string.Format(
                    "Cannot create percentage enchantment for enchantment store with id '{0}'.",
                    enchantmentStore.Id));
            }

            var enchantmentWeather = _enchantmentWeatherRepository.GetById(enchantmentStore.EnchantmentWeatherId);
            if (enchantmentWeather == null)
            {
                throw new InvalidOperationException(string.Format(
                    "Could not find enchantment weather for id '{0}'.",
                    enchantmentStore.EnchantmentWeatherId));
            }

            var percentageEnchantment = _percentageEnchantmentFactory.Create(
                enchantmentStore.Id,
                enchantmentStore.StatusTypeId,
                enchantmentStore.TriggerId,
                enchantmentWeather.WeatherIds,
                enchantmentStore.RemainingDuration,
                percentageEnchantmentStore.StatId,
                percentageEnchantmentStore.Value);
            
            return percentageEnchantment;
        }
        #endregion
    }
}
