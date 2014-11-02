using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Interface.Stats;

namespace ProjectXyz.Application.Interface.Enchantments
{
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
