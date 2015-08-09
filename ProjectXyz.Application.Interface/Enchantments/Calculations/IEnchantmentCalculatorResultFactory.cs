using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Interface.Enchantments.Calculations
{
    public interface IEnchantmentCalculatorResultFactory
    {
        #region Methods
        IEnchantmentCalculatorResult Create(
            IEnumerable<IEnchantment> enchantments,
            IStatCollection stats);
        #endregion
    }
}
