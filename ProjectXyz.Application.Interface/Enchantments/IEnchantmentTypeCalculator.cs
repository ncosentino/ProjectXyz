using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentTypeCalculator
    {
        #region Methods
        IEnchantmentTypeCalculatorResult Calculate(
            IEnchantmentContext enchantmentContext,
            IStatCollection stats,
            IEnumerable<IEnchantment> enchantments);
        #endregion
    }
}
