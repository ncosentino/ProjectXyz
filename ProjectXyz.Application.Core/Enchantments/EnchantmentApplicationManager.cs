using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Data.Interface;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class EnchantmentApplicationManager : IEnchantmentApplicationManager
    {
        #region Fields
        private readonly IEnchantmentGenerator _enchantmentGenerator;
        private readonly IEnchantmentCalculator _enchantmentCalculator;
        private readonly IEnchantmentSaver _enchantmentSaver;
        private readonly IEnchantmentApplicationFactoryManager _enchantmentApplicationFactoryManager;
        #endregion

        #region Constructors
        private EnchantmentApplicationManager(
            IDataManager dataManager,
            IEnchantmentApplicationFactoryManager enchantmentApplicationFactoryManager,
            IEnumerable<IEnchantmentTypeCalculator> enchantmentTypeCalculators)
        {
            _enchantmentApplicationFactoryManager = enchantmentApplicationFactoryManager;

            _enchantmentGenerator = Enchantments.EnchantmentGenerator.Create(
                dataManager.Enchantments.EnchantmentDefinitions,
                dataManager.Enchantments.EnchantmentPlugins,
                TypeLoader.Create());

            var enchantmentContext = EnchantmentContext.Create();
            _enchantmentCalculator = Calculations.EnchantmentCalculator.Create(
                enchantmentContext,
                enchantmentApplicationFactoryManager.EnchantmentCalculatorResults,
                enchantmentTypeCalculators);

            _enchantmentSaver = Enchantments.EnchantmentSaver.Create(
                dataManager.Enchantments.EnchantmentStoreFactory,
                dataManager.Enchantments.EnchantmentStores);
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public IEnchantmentGenerator EnchantmentGenerator
        {
            get { return _enchantmentGenerator; }
        }

        /// <inheritdoc />
        public IEnchantmentCalculator EnchantmentCalculator
        {
            get { return _enchantmentCalculator; }
        }

        /// <inheritdoc />
        public IEnchantmentSaver EnchantmentSaver
        {
            get { return _enchantmentSaver; }
        }

        /// <inheritdoc />
        public IEnchantmentApplicationFactoryManager Factories
        {
            get { return _enchantmentApplicationFactoryManager; }
        }
        #endregion

        #region Methods
        public static IEnchantmentApplicationManager Create(
            IDataManager dataManager,
            IEnchantmentApplicationFactoryManager enchantmentApplicationFactoryManager,
            IEnumerable<IEnchantmentTypeCalculator> enchantmentTypeCalculators)
        {
            Contract.Ensures(Contract.Result<IEnchantmentApplicationManager>() != null);

            return new EnchantmentApplicationManager(
                dataManager,
                enchantmentApplicationFactoryManager,
                enchantmentTypeCalculators);
        }
        #endregion
    }
}
