using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Interface.Enchantments.Calculations
{
    public interface IEnchantmentTypeCalculatorResultFactory
    {
        #region Methods
        IEnchantmentTypeCalculatorResult Create(
            IEnumerable<IEnchantment> removedEnchantments,
            IEnumerable<IEnchantment> processedEnchantments,
            IStatCollection stats);
        #endregion
    }
}
