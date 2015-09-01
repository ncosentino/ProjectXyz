using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Sql;
using ProjectXyz.Plugins.Enchantments.OneShotNegate.Sql;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public sealed class Plugin : IEnchantmentPlugin
    {
        #region Fields
        private readonly IOneShotNegateEnchantmentStoreRepository _oneShotNegateEnchantmentStoreRepository;
        private readonly IEnchantmentDefinitionRepository _enchantmentDefinitionRepository;
        private readonly IOneShotNegateEnchantmentDefinitionRepository _oneShotNegateEnchantmentDefinitioneRepository;
        private readonly IEnchantmentTypeCalculator _enchantmentTypeCalculator;
        private readonly IOneShotNegateEnchantmentGenerator _oneShotNegateEnchantmentGenerator;
        private readonly IOneShotNegateEnchantmentStoreFactory _oneShotNegateEnchantmentStoreFactory;
        private readonly IOneShotNegateEnchantmentFactory _oneShotNegateEnchantmentFactory;
        #endregion

        #region Constructors
        public Plugin(
            IDatabase database,
            IDataStore dataStore)
        {
            _enchantmentDefinitionRepository = dataStore.Enchantments.EnchantmentDefinitions;

            _enchantmentTypeCalculator = OneShotNegateEnchantmentTypeCalculator.Create(
                dataStore.Enchantments.StatusNegations,
                dataStore.Weather.WeatherGroupings);

            var enchantmentDefinitionWeatherGroupingRepository = dataStore.Enchantments.EnchantmentWeather;
            var enchantmentFactory = OneShotNegateEnchantmentFactory.Create();
            _oneShotNegateEnchantmentGenerator = OneShotNegateEnchantmentGenerator.Create(
                enchantmentFactory,
                enchantmentDefinitionWeatherGroupingRepository);

            _oneShotNegateEnchantmentStoreFactory = OneShotNegateEnchantmentStoreFactory.Create();

            _oneShotNegateEnchantmentStoreRepository = OneShotNegateEnchantmentStoreRepository.Create(
                database,
                _oneShotNegateEnchantmentStoreFactory);

            _oneShotNegateEnchantmentFactory = OneShotNegateEnchantmentFactory.Create();

            var oneShotNegateEnchantmentDefinitionFactory = OneShotNegateEnchantmentDefinitionFactory.Create();
            _oneShotNegateEnchantmentDefinitioneRepository = OneShotNegateEnchantmentDefinitionRepository.Create(database, oneShotNegateEnchantmentDefinitionFactory);
        }
        #endregion

        #region Properties
        public Type EnchantmentStoreType
        {
            get { return typeof(IOneShotNegateEnchantmentStore); }
        }

        public Type EnchantmentDefinitionType
        {
            get { return typeof(IOneShotNegateEnchantmentDefinition); }
        }

        public Type EnchantmentType
        {
            get { return typeof(IOneShotNegateEnchantment); }
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
            var oneShotNegateEnchantmentDefinition = _oneShotNegateEnchantmentDefinitioneRepository.GetById(enchantmentDefinitionId);
            var enchantment = _oneShotNegateEnchantmentGenerator.Generate(
                randomizer,
                enchantmentDefinition,
                oneShotNegateEnchantmentDefinition);
            return enchantment;
        }

        private void SaveEnchantment(IEnchantment enchantment)
        {
            if (!(enchantment is IOneShotNegateEnchantment))
            {
                throw new InvalidOperationException(string.Format(
                    "Cannot save '{0}' to an oneShotNegate enchantment store.",
                    enchantment.GetType()));
            }

            var oneShotNegateEnchantment = (IOneShotNegateEnchantment)enchantment;

            var enchantmentStore = _oneShotNegateEnchantmentStoreFactory.CreateEnchantmentStore(
                oneShotNegateEnchantment.Id,
                oneShotNegateEnchantment.StatId);
            
            // FIXME: we need add or update?
            _oneShotNegateEnchantmentStoreRepository.Add(enchantmentStore);
        }

        private IEnchantment CreateEnchantment(IEnchantmentStore enchantmentStore)
        {
            var oneShotNegateEnchantmentStore = _oneShotNegateEnchantmentStoreRepository.GetById(enchantmentStore.Id);
            if (oneShotNegateEnchantmentStore == null)
            {
                throw new InvalidOperationException(string.Format(
                    "Cannot create oneShotNegate enchantment for enchantment store with id '{0}'.",
                    enchantmentStore.Id));
            }
            
            var oneShotNegateEnchantment = _oneShotNegateEnchantmentFactory.Create(
                enchantmentStore.Id,
                enchantmentStore.StatusTypeId,
                enchantmentStore.TriggerId,
                enchantmentStore.WeatherGroupingId,
                oneShotNegateEnchantmentStore.StatId);
            
            return oneShotNegateEnchantment;
        }
        #endregion
    }
}
