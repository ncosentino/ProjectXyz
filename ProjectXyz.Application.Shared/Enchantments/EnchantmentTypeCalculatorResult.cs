using System.Collections.Generic;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Stats;

namespace ProjectXyz.Application.Shared.Enchantments
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