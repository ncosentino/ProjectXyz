using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Interface.Enchantments.Calculations.Contracts
{
    [ContractClassFor(typeof(IEnchantmentCalculator))]
    public abstract class IEnchantmentCalculatorContract : IEnchantmentCalculator
    {
        #region Methods
        public IEnchantmentCalculatorResult Calculate(IStatCollection stats, IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Ensures(Contract.Result<IEnchantmentCalculatorResult>() != null);
            return default(IEnchantmentCalculatorResult);
        }
        #endregion
    }
}
