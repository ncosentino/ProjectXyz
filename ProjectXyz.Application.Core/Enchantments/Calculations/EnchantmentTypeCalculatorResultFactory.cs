using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Core.Enchantments.Calculations
{
    public sealed class EnchantmentTypeCalculatorResultFactory : IEnchantmentTypeCalculatorResultFactory
    {
        #region Constructors
        private EnchantmentTypeCalculatorResultFactory()
        {
        }
        #endregion

        #region Methods
        public static IEnchantmentTypeCalculatorResultFactory Create()
        {
            var factory = new EnchantmentTypeCalculatorResultFactory();
            return factory;
        }

        public IEnchantmentTypeCalculatorResult Create(
            IEnumerable<IEnchantment> removedEnchantments,
            IEnumerable<IEnchantment> processedEnchantments,
            IStatCollection stats)
        {
            var result = EnchantmentTypeCalculatorResult.Create(
                removedEnchantments,
                processedEnchantments,
                stats);
            return result;
        }
        #endregion
    }
}
