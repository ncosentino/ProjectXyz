using System.Collections.Generic;
using ProjectXyz.Application.Interface.Stats;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentCalculator
    {
        #region Methods
        IEnchantmentCalculatorResult Calculate(
            IStatCollection stats, 
            IEnumerable<IEnchantment> enchantments);
        #endregion
    }
}