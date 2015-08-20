using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Sql;
using ProjectXyz.Plugins.Enchantments.Additive.Sql;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
    public sealed class Plugin : IEnchantmentPlugin
    {
        #region Fields
        private readonly IEnchantmentWeatherRepository _enchantmentWeatherRepository;
        private readonly IAdditiveEnchantmentStoreRepository _additiveEnchantmentStoreRepository;
        private readonly IEnchantmentDefinitionRepository _enchantmentDefinitionRepository;
        private readonly IAdditiveEnchantmentDefinitionRepository _additiveEnchantmentDefinitioneRepository;
        private readonly IEnchantmentTypeCalculator _enchantmentTypeCalculator;
        private readonly IAdditiveEnchantmentGenerator _additiveEnchantmentGenerator;
        private readonly IAdditiveEnchantmentStoreFactory _additiveEnchantmentStoreFactory;
        private readonly IAdditiveEnchantmentFactory _additiveEnchantmentFactory;
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
            _enchantmentTypeCalculator = AdditiveEnchantmentTypeCalculator.Create(statFactory);

            var enchantmentFactory = AdditiveEnchantmentFactory.Create();
            _additiveEnchantmentGenerator = AdditiveEnchantmentGenerator.Create(
                enchantmentFactory,
                enchantmentWeatherRepository);

            _additiveEnchantmentStoreFactory = AdditiveEnchantmentStoreFactory.Create();

            _additiveEnchantmentStoreRepository = AdditiveEnchantmentStoreRepository.Create(
                database,
                _additiveEnchantmentStoreFactory);

            _additiveEnchantmentFactory = AdditiveEnchantmentFactory.Create();

            var additiveEnchantmentDefinitionFactory = AdditiveEnchantmentDefinitionFactory.Create();
            _additiveEnchantmentDefinitioneRepository = AdditiveEnchantmentDefinitionRepository.Create(database, additiveEnchantmentDefinitionFactory);
        }
        #endregion

        #region Properties
        public Type EnchantmentStoreType
        {
            get { return typeof(IAdditiveEnchantmentStore); }
        }

        public Type EnchantmentDefinitionType
        {
            get { return typeof(IAdditiveEnchantmentDefinition); }
        }

        public Type EnchantmentType
        {
            get { return typeof(IAdditiveEnchantment); }
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
            var additiveEnchantmentDefinition = _additiveEnchantmentDefinitioneRepository.GetById(enchantmentDefinitionId);
            var enchantment = _additiveEnchantmentGenerator.Generate(
                randomizer,
                enchantmentDefinition,
                additiveEnchantmentDefinition);
            return enchantment;
        }

        private void SaveEnchantment(IEnchantment enchantment)
        {
            if (!(enchantment is IAdditiveEnchantment))
            {
                throw new InvalidOperationException(string.Format(
                    "Cannot save '{0}' to an additive enchantment store.",
                    enchantment.GetType()));
            }

            var additiveEnchantment = (IAdditiveEnchantment)enchantment;

            var enchantmentStore = _additiveEnchantmentStoreFactory.CreateEnchantmentStore(
                additiveEnchantment.Id,
                additiveEnchantment.StatId,
                additiveEnchantment.Value,
                additiveEnchantment.RemainingDuration);
            
            // FIXME: we need add or update?
            _additiveEnchantmentStoreRepository.Add(enchantmentStore);
        }

        private IEnchantment CreateEnchantment(IEnchantmentStore enchantmentStore)
        {
            var additiveEnchantmentStore = _additiveEnchantmentStoreRepository.GetById(enchantmentStore.Id);
            if (additiveEnchantmentStore == null)
            {
                throw new InvalidOperationException(string.Format(
                    "Cannot create additive enchantment for enchantment store with id '{0}'.",
                    enchantmentStore.Id));
            }

            var enchantmentWeather = _enchantmentWeatherRepository.GetById(enchantmentStore.EnchantmentWeatherId);
            if (enchantmentWeather == null)
            {
                throw new InvalidOperationException(string.Format(
                    "Could not find enchantment weather for id '{0}'.",
                    enchantmentStore.EnchantmentWeatherId));
            }

            var additiveEnchantment = _additiveEnchantmentFactory.Create(
                enchantmentStore.Id,
                enchantmentStore.StatusTypeId,
                enchantmentStore.TriggerId,
                enchantmentWeather.WeatherIds,
                additiveEnchantmentStore.RemainingDuration,
                additiveEnchantmentStore.StatId,
                additiveEnchantmentStore.Value);
            
            return additiveEnchantment;
        }
        #endregion
    }
}
