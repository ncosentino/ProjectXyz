using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Interface.Enchantments.Calculations
{
    public interface IEnchantmentCalculatorResult
    {
        #region Properties
        IStatCollection Stats { get; }

        IEnumerable<IEnchantment> Enchantments { get; }
        #endregion
    }
}
