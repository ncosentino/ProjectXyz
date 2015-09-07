using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class EnchantmentApplicationFactoryManager : IEnchantmentApplicationFactoryManager
    {
        #region Fields
        private readonly IEnchantmentFactory _enchantmentFactory;
        private readonly IEnchantmentCalculatorResultFactory _enchantmentCalculatorResultFactory;
        private readonly IEnchantmentTypeCalculatorResultFactory _enchantmentTypeCalculatorResultFactory;
        #endregion

        #region Constructors
        private EnchantmentApplicationFactoryManager()
        {
            _enchantmentFactory = Core.Enchantments.EnchantmentFactory.Create();
            _enchantmentCalculatorResultFactory = Core.Enchantments.Calculations.EnchantmentCalculatorResultFactory.Create();
            _enchantmentTypeCalculatorResultFactory = Core.Enchantments.Calculations.EnchantmentTypeCalculatorResultFactory.Create();
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public IEnchantmentFactory Enchantments { get { return _enchantmentFactory; } }

        /// <inheritdoc />
        public IEnchantmentCalculatorResultFactory EnchantmentCalculatorResults { get { return _enchantmentCalculatorResultFactory; } }

        /// <inheritdoc />
        public IEnchantmentTypeCalculatorResultFactory EnchantmentTypeCalculatorResults { get { return _enchantmentTypeCalculatorResultFactory; } }
        #endregion

        #region Methods
        public static IEnchantmentApplicationFactoryManager Create()
        {
            var manager = new EnchantmentApplicationFactoryManager();
            return manager;
        }
        #endregion
    }
}
