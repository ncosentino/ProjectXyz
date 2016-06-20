using System.Collections.Generic;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Stats;

namespace ProjectXyz.Application.Shared.Enchantments
{
    public sealed class EnchantmentTypeCalculatorResultFactory : IEnchantmentTypeCalculatorResultFactory
    {
        #region Methods
        public IEnchantmentTypeCalculatorResult Create(
            IReadOnlyCollection<IEnchantment> addedEnchantments,
            IReadOnlyCollection<IEnchantment> removedEnchantments,
            IReadOnlyCollection<IEnchantment> processedEnchantments,
            IStatCollection stats)
        {
            return new EnchantmentTypeCalculatorResult(
                addedEnchantments,
                removedEnchantments,
                processedEnchantments,
                stats);
        }
        #endregion
    }
}