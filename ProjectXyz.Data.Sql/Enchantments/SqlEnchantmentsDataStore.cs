using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Sql.Enchantments
{
    public sealed class SqlEnchantmentsDataStore : IEnchantmentsDataStore
    {
        #region Fields
        private readonly IEnchantmentDefinitionRepository _enchantmentDefinitionRepository;
        private readonly IEnchantmentStoreRepository _enchantmentStoreRepository;
        private readonly IEnchantmentTriggerRepository _enchantmentTriggerRepository;
        private readonly IEnchantmentDefinitionWeatherTypeGroupingRepository _enchantmentDefinitionWeatherGroupingRepository;
        private readonly IStatusNegationRepository _statusNegationRepository;
        private readonly IEnchantmentTypeRepository _enchantmentTypeRepository;
        #endregion

        #region Constructors
        private SqlEnchantmentsDataStore(IDatabase database)
        {
            var enchantmentTriggerFactory = EnchantmentTriggerFactory.Create();
            _enchantmentTriggerRepository = EnchantmentTriggerRepository.Create(
                database,
                enchantmentTriggerFactory);

            var enchantmentDefinitionFactory = EnchantmentDefinitionFactory.Create();
            _enchantmentDefinitionRepository = EnchantmentDefinitionRepository.Create(
                database,
                enchantmentDefinitionFactory);

            var enchantmentStoreFactory = EnchantmentStoreFactory.Create();
            _enchantmentStoreRepository = EnchantmentStoreRepository.Create(
                database,
                enchantmentStoreFactory);

            var statusNegationFactory = StatusNegationFactory.Create();
            _statusNegationRepository = StatusNegationRepository.Create(
                database,
                statusNegationFactory);

            var enchantmentDefinitionWeatherGroupingFactory = EnchantmentDefinitionWeatherGroupingFactory.Create();
            _enchantmentDefinitionWeatherGroupingRepository = EnchantmentDefinitionWeatherTypeGroupingRepository.Create(
                database,
                enchantmentDefinitionWeatherGroupingFactory);

            _enchantmentTypeRepository = EnchantmentTypeRepository.Create(database);
        }
        #endregion

        #region Properties
        public IEnchantmentDefinitionRepository EnchantmentDefinitions { get { return _enchantmentDefinitionRepository; } }

        public IEnchantmentStoreRepository EnchantmentStores { get { return _enchantmentStoreRepository;} }

        public IEnchantmentTriggerRepository EnchantmentTriggers { get { return _enchantmentTriggerRepository; } }

        public IEnchantmentDefinitionWeatherTypeGroupingRepository EnchantmentWeather { get { return _enchantmentDefinitionWeatherGroupingRepository; } }

        public IStatusNegationRepository StatusNegations { get { return _statusNegationRepository; } }

        public IEnchantmentTypeRepository EnchantmentTypes { get { return _enchantmentTypeRepository; } }
        #endregion

        #region Methods
        public static IEnchantmentsDataStore Create(IDatabase database)
        {
            var dataStore = new SqlEnchantmentsDataStore(database);
            return dataStore;
        }
        #endregion
    }
}
