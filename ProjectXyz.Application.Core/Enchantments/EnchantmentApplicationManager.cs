using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Interface;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class EnchantmentApplicationManager : IEnchantmentApplicationManager
    {
        #region Fields
        private readonly IEnchantmentFactory _enchantmentFactory;
        private readonly IEnchantmentGenerator _enchantmentGenerator;
        private readonly IEnchantmentCalculator _enchantmentCalculator;
        private readonly IEnchantmentSaver _enchantmentSaver;
        private readonly IEnchantmentTypeCalculatorResultFactory _enchantmentTypeCalculatorResultFactory;
        #endregion

        #region Constructors
        private EnchantmentApplicationManager(
            IDataManager dataManager,
            IEnumerable<IEnchantmentTypeCalculator> enchantmentTypeCalculators)
        {
            _enchantmentFactory = Enchantments.EnchantmentFactory.Create();
            
            _enchantmentGenerator = Enchantments.EnchantmentGenerator.Create(
                dataManager.Enchantments.EnchantmentTypes,
                TypeLoader.Create());

            var enchantmentContext = EnchantmentContext.Create();
            var enchantmentCalculatorResultFactory = Calculations.EnchantmentCalculatorResultFactory.Create();
            _enchantmentCalculator = Calculations.EnchantmentCalculator.Create(
                enchantmentContext,
                enchantmentCalculatorResultFactory,
                enchantmentTypeCalculators);

            var enchantmentStoreFactory = EnchantmentStoreFactory.Create();
            _enchantmentSaver = Enchantments.EnchantmentSaver.Create(
                enchantmentStoreFactory,
                dataManager.Enchantments.EnchantmentStores);

            _enchantmentTypeCalculatorResultFactory = Calculations.EnchantmentTypeCalculatorResultFactory.Create();
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public IEnchantmentFactory EnchantmentFactory
        {
            get { return _enchantmentFactory; }
        }

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
        public IEnchantmentTypeCalculatorResultFactory EnchantmentTypeCalculatorResultFactory
        {
            get { return _enchantmentTypeCalculatorResultFactory; }
        }
        #endregion

        #region Methods
        public static IEnchantmentApplicationManager Create(
            IDataManager dataManager,
            IEnumerable<IEnchantmentTypeCalculator> enchantmentTypeCalculators)
        {
            Contract.Ensures(Contract.Result<IEnchantmentApplicationManager>() != null);

            return new EnchantmentApplicationManager(
                dataManager,
                enchantmentTypeCalculators);
        }
        #endregion
    }
}
