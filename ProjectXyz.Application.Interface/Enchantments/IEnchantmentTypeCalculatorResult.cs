using System.Collections.Generic;
using ProjectXyz.Application.Interface.Stats;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentTypeCalculatorResult
    {
        #region Properties
        IReadOnlyCollection<IEnchantment> AddedEnchantments { get; }

        IReadOnlyCollection<IEnchantment> RemovedEnchantments { get; }

        IReadOnlyCollection<IEnchantment> ProcessedEnchantments { get; }

        IStatCollection Stats { get; }
        #endregion
    }
}