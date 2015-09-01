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
        private readonly IEnchantmentFactory _enchantmentFactory;
        private readonly IEnchantmentGenerator _enchantmentGenerator;
        private readonly IEnchantmentCalculator _enchantmentCalculator;
        #endregion

        #region Constructors
        private EnchantmentApplicationManager(
            IDataStore dataStore,
            IEnumerable<IEnchantmentTypeCalculator> enchantmentTypeCalculators)
        {
            _enchantmentFactory = Enchantments.EnchantmentFactory.Create();
            
            _enchantmentGenerator = Enchantments.EnchantmentGenerator.Create(
                dataStore.Enchantments.EnchantmentTypes,
                TypeLoader.Create());

            var enchantmentContext = EnchantmentContext.Create();
            var enchantmentCalculatorResultFactory = Calculations.EnchantmentCalculatorResultFactory.Create();
            _enchantmentCalculator = Calculations.EnchantmentCalculator.Create(
                enchantmentContext,
                enchantmentCalculatorResultFactory,
                enchantmentTypeCalculators);
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
        #endregion

        #region Methods
        public static IEnchantmentApplicationManager Create(
            IDataStore dataStore,
            IEnumerable<IEnchantmentTypeCalculator> enchantmentTypeCalculators)
        {
            Contract.Ensures(Contract.Result<IEnchantmentApplicationManager>() != null);

            return new EnchantmentApplicationManager(
                dataStore,
                enchantmentTypeCalculators);
        }
        #endregion
    }
}
