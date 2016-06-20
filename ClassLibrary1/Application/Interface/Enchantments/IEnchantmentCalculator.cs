using System.Collections.Generic;
using ClassLibrary1.Application.Interface.Stats;

namespace ClassLibrary1.Application.Interface.Enchantments
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