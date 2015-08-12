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
        private readonly IEnchantmentStoreRepository<IAdditiveEnchantmentStore> _enchantmentStoreRepository;
        private readonly IEnchantmentDefinitionRepository<IAdditiveEnchantmentDefinition> _enchantmentDefinitioneRepository;
        private readonly IEnchantmentTypeCalculator _enchantmentTypeCalculator;
        private readonly IAdditiveEnchantmentGenerator _additiveEnchantmentGenerator;
        private readonly IAdditiveEnchantmentStoreFactory _additiveEnchantmentStoreFactory;
        private readonly IAdditiveEnchantmentFactory _additiveEnchantmentFactory;
        #endregion

        #region Constructors
        public Plugin(
            IDatabase database,
            IEnchantmentStoreRepository<IEnchantmentStore> enchantmentStoreRepository)
        {
            var statFactory = StatFactory.Create();
            _enchantmentTypeCalculator = AdditiveEnchantmentTypeCalculator.Create(statFactory);

            var enchantmentFactory = AdditiveEnchantmentFactory.Create();
            _additiveEnchantmentGenerator = AdditiveEnchantmentGenerator.Create(enchantmentFactory);

            // FIXME: this should be a constant value defined somewhere
            var enchantmentTypeId = Guid.NewGuid();
            _additiveEnchantmentStoreFactory = AdditiveEnchantmentStoreFactory.Create(enchantmentTypeId);

            _enchantmentStoreRepository = AdditiveEnchantmentStoreRepository.Create(
                database,
                enchantmentStoreRepository,
                _additiveEnchantmentStoreFactory);

            _additiveEnchantmentFactory = AdditiveEnchantmentFactory.Create();
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
            var definition = _enchantmentDefinitioneRepository.GetById(enchantmentDefinitionId);
            var enchantment = _additiveEnchantmentGenerator.Generate(
                randomizer,
                definition);
            return enchantment;
        }

        private IEnchantmentStore SaveEnchantment(IEnchantment enchantment)
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
                additiveEnchantment.TriggerId,
                additiveEnchantment.StatusTypeId,
                additiveEnchantment.RemainingDuration,
                additiveEnchantment.Value);
            
            // FIXME: we need add or update?
            _enchantmentStoreRepository.Add(enchantmentStore);

            return enchantmentStore;
        }

        private IEnchantment CreateEnchantment(IEnchantmentStore enchantmentStore)
        {
            if (!(enchantmentStore is IAdditiveEnchantmentStore))
            {
                throw new InvalidOperationException(string.Format(
                    "Cannot create additive enchantment from '{0}'.",
                    enchantmentStore.GetType()));
            }

            var additiveEnchantmentStore = (IAdditiveEnchantmentStore)enchantmentStore;
            var additiveEnchantment = _additiveEnchantmentFactory.Create(
                additiveEnchantmentStore.Id,
                additiveEnchantmentStore.StatusTypeId,
                additiveEnchantmentStore.TriggerId,
                additiveEnchantmentStore.RemainingDuration,
                additiveEnchantmentStore.StatId,
                additiveEnchantmentStore.Value);
            
            return additiveEnchantment;
        }
        #endregion
    }
}
