using System.Collections.Generic;
using ProjectXyz.Application.Interface.Stats;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentTypeCalculator
    {
        #region Methods
        IEnchantmentTypeCalculatorResult Calculate(
            IEnchantmentContext enchantmentContext,
            IStatCollection stats,
            IEnumerable<IEnchantment> enchantments);
        #endregion
    }
}