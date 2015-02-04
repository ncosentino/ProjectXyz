using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IEnchantmentCalculator))]
    public abstract class IEnchantmentCalculatorContract : IEnchantmentCalculator
    {
        #region Methods
        public IStatCollection Calculate(IStatCollection stats, IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Ensures(Contract.Result<IStatCollection>() != null);
            return default(IStatCollection);
        }
        #endregion
    }
}
