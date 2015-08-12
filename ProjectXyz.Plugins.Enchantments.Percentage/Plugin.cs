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
        private readonly IEnchantmentStoreRepository<IPercentageEnchantmentStore> _enchantmentStoreRepository;
        private readonly IEnchantmentDefinitionRepository<IPercentageEnchantmentDefinition> _enchantmentDefinitioneRepository;
        private readonly IEnchantmentTypeCalculator _enchantmentTypeCalculator;
        private readonly IPercentageEnchantmentGenerator _percentageEnchantmentGenerator;
        private readonly IPercentageEnchantmentStoreFactory _percentageEnchantmentStoreFactory;
        private readonly IPercentageEnchantmentFactory _percentageEnchantmentFactory;
        #endregion

        #region Constructors
        public Plugin(
            IDatabase database,
            IEnchantmentStoreRepository<IEnchantmentStore> enchantmentStoreRepository)
        {
            var statFactory = StatFactory.Create();
            _enchantmentTypeCalculator = PercentageEnchantmentTypeCalculator.Create(statFactory);

            var enchantmentFactory = PercentageEnchantmentFactory.Create();
            _percentageEnchantmentGenerator = PercentageEnchantmentGenerator.Create(enchantmentFactory);

            // FIXME: this should be a constant value defined somewhere
            var enchantmentTypeId = Guid.NewGuid();
            _percentageEnchantmentStoreFactory = PercentageEnchantmentStoreFactory.Create(enchantmentTypeId);

            _enchantmentStoreRepository = PercentageEnchantmentStoreRepository.Create(
                database,
                enchantmentStoreRepository,
                _percentageEnchantmentStoreFactory);

            _percentageEnchantmentFactory = PercentageEnchantmentFactory.Create();
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
            var definition = _enchantmentDefinitioneRepository.GetById(enchantmentDefinitionId);
            var enchantment = _percentageEnchantmentGenerator.Generate(
                randomizer,
                definition);
            return enchantment;
        }

        private IEnchantmentStore SaveEnchantment(IEnchantment enchantment)
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
                percentageEnchantment.TriggerId,
                percentageEnchantment.StatusTypeId,
                percentageEnchantment.RemainingDuration,
                percentageEnchantment.Value);
            
            // FIXME: we need add or update?
            _enchantmentStoreRepository.Add(enchantmentStore);

            return enchantmentStore;
        }

        private IEnchantment CreateEnchantment(IEnchantmentStore enchantmentStore)
        {
            if (!(enchantmentStore is IPercentageEnchantmentStore))
            {
                throw new InvalidOperationException(string.Format(
                    "Cannot create percentage enchantment from '{0}'.",
                    enchantmentStore.GetType()));
            }

            var percentageEnchantmentStore = (IPercentageEnchantmentStore)enchantmentStore;
            var percentageEnchantment = _percentageEnchantmentFactory.Create(
                percentageEnchantmentStore.Id,
                percentageEnchantmentStore.StatusTypeId,
                percentageEnchantmentStore.TriggerId,
                percentageEnchantmentStore.RemainingDuration,
                percentageEnchantmentStore.StatId,
                percentageEnchantmentStore.Value);
            
            return percentageEnchantment;
        }
        #endregion
    }
}
