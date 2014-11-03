using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Application.Interface.Enchantments.Contracts;

namespace ProjectXyz.Application.Interface.Enchantments
{
    [ContractClass(typeof(IEnchantmentCalculatorContract))]
    public interface IEnchantmentCalculator
    {
        #region Methods
        IStatCollection<IStat> Calculate<TStat>(
            IStatCollection<TStat> stats,
            IEnchantmentCollection enchantments)
            where TStat : IStat;
        #endregion
    }
}
