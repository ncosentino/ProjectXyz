using System.Collections.Generic;
using ClassLibrary1.Application.Interface.Stats;

namespace ClassLibrary1.Application.Interface.Enchantments
{
    public interface IEnchantmentCalculatorResult
    {
        #region Properties
        IStatCollection Stats { get; }

        IEnumerable<IEnchantment> Enchantments { get; }
        #endregion
    }
}