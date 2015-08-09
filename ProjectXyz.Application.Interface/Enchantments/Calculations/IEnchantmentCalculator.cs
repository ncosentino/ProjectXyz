using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments.Calculations.Contracts;
using ProjectXyz.Application.Interface.Enchantments.Contracts;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Interface.Enchantments.Calculations
{
    [ContractClass(typeof(IEnchantmentCalculatorContract))]
    public interface IEnchantmentCalculator
    {
        #region Methods
        IEnchantmentCalculatorResult Calculate(IStatCollection stats, IEnumerable<IEnchantment> enchantments);
        #endregion
    }
}
