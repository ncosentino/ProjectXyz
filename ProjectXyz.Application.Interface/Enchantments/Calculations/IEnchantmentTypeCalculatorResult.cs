using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Interface.Enchantments.Calculations
{
    public interface IEnchantmentTypeCalculatorResult
    {
        #region Properties
        IEnumerable<IEnchantment> RemovedEnchantments { get; }

        IEnumerable<IEnchantment> ProcessedEnchantments { get; }

        IStatCollection Stats { get; }
        #endregion
    }
}
