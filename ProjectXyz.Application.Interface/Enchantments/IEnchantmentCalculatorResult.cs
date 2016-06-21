using System.Collections.Generic;
using ProjectXyz.Application.Interface.Stats;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentCalculatorResult
    {
        #region Properties
        IStatCollection Stats { get; }

        IReadOnlyCollection<IEnchantment> Enchantments { get; }
        #endregion
    }
}