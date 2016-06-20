using System.Collections.Generic;
using ProjectXyz.Application.Interface.Stats;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentTypeCalculatorResultFactory
    {
        #region Methods
        IEnchantmentTypeCalculatorResult Create(
            IReadOnlyCollection<IEnchantment> addedEnchantments,
            IReadOnlyCollection<IEnchantment> removedEnchantments,
            IReadOnlyCollection<IEnchantment> processedEnchantments,
            IStatCollection stats);
        #endregion
    }
}