using System.Collections.Generic;
using ClassLibrary1.Application.Interface.Stats;

namespace ClassLibrary1.Application.Interface.Enchantments
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