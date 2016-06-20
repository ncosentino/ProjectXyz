using System.Collections.Generic;
using ClassLibrary1.Application.Interface.Stats;

namespace ClassLibrary1.Application.Interface.Enchantments
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