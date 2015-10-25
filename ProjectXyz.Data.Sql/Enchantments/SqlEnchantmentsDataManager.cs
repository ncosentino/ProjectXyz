using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Sql.Enchantments
{
    public sealed class SqlEnchantmentsDataManager : IEnchantmentsDataManager
    {
        #region Fields
        private readonly IEnchantmentDefinitionRepository _enchantmentDefinitionRepository;
        private readonly IEnchantmentStoreRepository _enchantmentStoreRepository;
        private readonly IEnchantmentTriggerRepository _enchantmentTriggerRepository;
        private readonly IEnchantmentStatusRepository _enchantmentStatusRepository;
        private readonly IEnchantmentDefinitionWeatherTypeGroupingRepository _enchantmentDefinitionWeatherGroupingRepository;
        private readonly IStatusNegationRepository _statusNegationRepository;
        private readonly IEnchantmentTypeRepository _enchantmentTypeRepository;
        private readonly IEnchantmentStoreFactory _enchantmentStoreFactory;
        #endregion

        #region Constructors
        private SqlEnchantmentsDataManager(IDatabase database)
        {
            var enchantmentTriggerFactory = EnchantmentTriggerFactory.Create();
            _enchantmentTriggerRepository = EnchantmentTriggerRepository.Create(
                database,
                enchantmentTriggerFactory);

            var enchantmentStatusFactory = EnchantmentStatusFactory.Create();
            _enchantmentStatusRepository = EnchantmentStatusRepository.Create(
                database,
                enchantmentStatusFactory);

            var enchantmentDefinitionFactory = EnchantmentDefinitionFactory.Create();
            _enchantmentDefinitionRepository = EnchantmentDefinitionRepository.Create(
                database,
                enchantmentDefinitionFactory);

            _enchantmentStoreFactory = Core.Enchantments.EnchantmentStoreFactory.Create();
            _enchantmentStoreRepository = EnchantmentStoreRepository.Create(
                database,
                _enchantmentStoreFactory);

            var statusNegationFactory = StatusNegationFactory.Create();
            _statusNegationRepository = StatusNegationRepository.Create(
                database,
                statusNegationFactory);

            var enchantmentDefinitionWeatherGroupingFactory = EnchantmentDefinitionWeatherGroupingFactory.Create();
            _enchantmentDefinitionWeatherGroupingRepository = EnchantmentDefinitionWeatherTypeGroupingRepository.Create(
                database,
                enchantmentDefinitionWeatherGroupingFactory);

            var enchantmentTypeFactory = EnchantmentTypeFactory.Create();
            _enchantmentTypeRepository = EnchantmentTypeRepository.Create(
                database,
                enchantmentTypeFactory);
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public IEnchantmentDefinitionRepository EnchantmentDefinitions { get { return _enchantmentDefinitionRepository; } }

        /// <inheritdoc />
        public IEnchantmentStoreRepository EnchantmentStores { get { return _enchantmentStoreRepository;} }

        /// <inheritdoc />
        public IEnchantmentTriggerRepository EnchantmentTriggers { get { return _enchantmentTriggerRepository; } }

        /// <inheritdoc />
        public IEnchantmentStatusRepository EnchantmentStatuses { get { return _enchantmentStatusRepository; } }

        /// <inheritdoc />
        public IEnchantmentDefinitionWeatherTypeGroupingRepository EnchantmentWeather { get { return _enchantmentDefinitionWeatherGroupingRepository; } }

        /// <inheritdoc />
        public IStatusNegationRepository StatusNegations { get { return _statusNegationRepository; } }

        /// <inheritdoc />
        public IEnchantmentTypeRepository EnchantmentTypes { get { return _enchantmentTypeRepository; } }

        /// <inheritdoc />
        public IEnchantmentStoreFactory EnchantmentStoreFactory { get { return _enchantmentStoreFactory; } }
        #endregion

        #region Methods
        public static IEnchantmentsDataManager Create(IDatabase database)
        {
            var dataManager = new SqlEnchantmentsDataManager(database);
            return dataManager;
        }
        #endregion
    }
}
