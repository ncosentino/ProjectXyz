using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Application.Interface.Enchantments.Contracts;

namespace ProjectXyz.Application.Interface.Enchantments
{
    [ContractClass(typeof(IEnchantmentCalculatorContract))]
    public interface IEnchantmentCalculator
    {
        #region Methods
        IStatCollection Calculate(IStatCollection stats, IEnumerable<IEnchantment> enchantments);
        #endregion
    }
}
