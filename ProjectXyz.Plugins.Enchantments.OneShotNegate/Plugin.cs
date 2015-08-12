using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Sql;
using ProjectXyz.Data.Sql.Enchantments;
using ProjectXyz.Plugins.Enchantments.OneShotNegate.Sql;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public sealed class Plugin : IEnchantmentPlugin
    {
        #region Fields
        private readonly IEnchantmentStoreRepository<IOneShotNegateEnchantmentStore> _enchantmentStoreRepository;
        private readonly IEnchantmentDefinitionRepository<IOneShotNegateEnchantmentDefinition> _enchantmentDefinitioneRepository;
        private readonly IEnchantmentTypeCalculator _enchantmentTypeCalculator;
        private readonly IOneShotNegateEnchantmentGenerator _oneShotNegateEnchantmentGenerator;
        private readonly IOneShotNegateEnchantmentStoreFactory _oneShotNegateEnchantmentStoreFactory;
        private readonly IOneShotNegateEnchantmentFactory _oneShotNegateEnchantmentFactory;
        #endregion

        #region Constructors
        public Plugin(
            IDatabase database,
            IEnchantmentStoreRepository<IEnchantmentStore> enchantmentStoreRepository)
        {
            var statusNegationRepository = StatusNegationRepository.Create(database);
            _enchantmentTypeCalculator = OneShotNegateEnchantmentTypeCalculator.Create(statusNegationRepository);

            var enchantmentFactory = OneShotNegateEnchantmentFactory.Create();
            _oneShotNegateEnchantmentGenerator = OneShotNegateEnchantmentGenerator.Create(enchantmentFactory);

            // FIXME: this should be a constant value defined somewhere
            var enchantmentTypeId = Guid.NewGuid();
            _oneShotNegateEnchantmentStoreFactory = OneShotNegateEnchantmentStoreFactory.Create(enchantmentTypeId);

            _enchantmentStoreRepository = OneShotNegateEnchantmentStoreRepository.Create(
                database,
                enchantmentStoreRepository,
                _oneShotNegateEnchantmentStoreFactory);

            _oneShotNegateEnchantmentFactory = OneShotNegateEnchantmentFactory.Create();
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
            var definition = _enchantmentDefinitioneRepository.GetById(enchantmentDefinitionId);
            var enchantment = _oneShotNegateEnchantmentGenerator.Generate(
                randomizer,
                definition);
            return enchantment;
        }

        private IEnchantmentStore SaveEnchantment(IEnchantment enchantment)
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
                oneShotNegateEnchantment.StatId,
                oneShotNegateEnchantment.TriggerId,
                oneShotNegateEnchantment.StatusTypeId);
            
            // FIXME: we need add or update?
            _enchantmentStoreRepository.Add(enchantmentStore);

            return enchantmentStore;
        }

        private IEnchantment CreateEnchantment(IEnchantmentStore enchantmentStore)
        {
            if (!(enchantmentStore is IOneShotNegateEnchantmentStore))
            {
                throw new InvalidOperationException(string.Format(
                    "Cannot create oneShotNegate enchantment from '{0}'.",
                    enchantmentStore.GetType()));
            }

            var oneShotNegateEnchantmentStore = (IOneShotNegateEnchantmentStore)enchantmentStore;
            var oneShotNegateEnchantment = _oneShotNegateEnchantmentFactory.Create(
                oneShotNegateEnchantmentStore.Id,
                oneShotNegateEnchantmentStore.StatusTypeId,
                oneShotNegateEnchantmentStore.TriggerId,
                oneShotNegateEnchantmentStore.RemainingDuration,
                oneShotNegateEnchantmentStore.StatId);
            
            return oneShotNegateEnchantment;
        }
        #endregion
    }
}
