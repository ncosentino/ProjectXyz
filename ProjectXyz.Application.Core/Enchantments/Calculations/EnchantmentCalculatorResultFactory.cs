using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Core.Enchantments.Calculations
{
    public sealed class EnchantmentCalculatorResultFactory : IEnchantmentCalculatorResultFactory
    {
        #region Constructors
        private EnchantmentCalculatorResultFactory()
        {
        }
        #endregion

        #region Methods
        public static IEnchantmentCalculatorResultFactory Create()
        {
            return new EnchantmentCalculatorResultFactory();
        }

        public IEnchantmentCalculatorResult Create(
            IEnumerable<IEnchantment> enchantments,
            IStatCollection stats)
        {
            var enchantmentCalculatorResult = EnchantmentCalculatorResult.Create(
                enchantments,
                stats);
            return enchantmentCalculatorResult;
        }
        #endregion
    }
}
