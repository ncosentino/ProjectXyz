using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IEnchantmentCalculator))]
    public abstract class IEnchantmentCalculatorContract : IEnchantmentCalculator
    {
        #region Methods
        public IStatCollection<IStat> Calculate<TStat>(
            IStatCollection<TStat> stats,
            IEnchantmentCollection enchantments)
            where TStat : IStat
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Requires<ArgumentNullException>(enchantments != null);
            return default(IStatCollection<IStat>);
        }
        #endregion
    }
}
