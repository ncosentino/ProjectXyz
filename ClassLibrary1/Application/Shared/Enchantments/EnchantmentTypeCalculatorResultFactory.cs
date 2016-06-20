using System.Collections.Generic;
using ClassLibrary1.Application.Interface.Enchantments;
using ClassLibrary1.Application.Interface.Stats;

namespace ClassLibrary1.Application.Shared.Enchantments
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