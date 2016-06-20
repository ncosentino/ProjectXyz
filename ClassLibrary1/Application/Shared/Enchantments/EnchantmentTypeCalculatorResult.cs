using System.Collections.Generic;
using ClassLibrary1.Application.Interface.Enchantments;
using ClassLibrary1.Application.Interface.Stats;

namespace ClassLibrary1.Application.Shared.Enchantments
{
    public sealed class EnchantmentTypeCalculatorResult : IEnchantmentTypeCalculatorResult
    {
        #region Constructors
        public EnchantmentTypeCalculatorResult(
            IReadOnlyCollection<IEnchantment> addedEnchantments,
            IReadOnlyCollection<IEnchantment> removedEnchantments,
            IReadOnlyCollection<IEnchantment> processedEnchantments,
            IStatCollection stats)
        {
            AddedEnchantments = addedEnchantments;
            RemovedEnchantments = removedEnchantments;
            ProcessedEnchantments = processedEnchantments;
            Stats = stats;
        }
        #endregion

        #region Properties
        public IReadOnlyCollection<IEnchantment> AddedEnchantments { get; }

        public IReadOnlyCollection<IEnchantment> RemovedEnchantments { get; }

        public IReadOnlyCollection<IEnchantment> ProcessedEnchantments { get; }

        public IStatCollection Stats { get; }
        #endregion
    }
}