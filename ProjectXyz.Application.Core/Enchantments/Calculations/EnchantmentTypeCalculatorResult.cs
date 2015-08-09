using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Core.Enchantments.Calculations
{
    public sealed class EnchantmentTypeCalculatorResult : IEnchantmentTypeCalculatorResult
    {
        #region Fields
        private readonly List<IEnchantment> _removedEnchantments;
        private readonly List<IEnchantment> _processedEnchantments;
        private readonly IStatCollection _stats;
        #endregion

        #region Constructors
        private EnchantmentTypeCalculatorResult(
            IEnumerable<IEnchantment> removedEnchantments,
            IEnumerable<IEnchantment> processedEnchantments,
            IStatCollection stats)
        {
            _removedEnchantments = new List<IEnchantment>(removedEnchantments);
            _processedEnchantments = new List<IEnchantment>(processedEnchantments);
            _stats = StatCollection.Create(stats);
        }
        #endregion

        #region Properties        
        /// <inheritdoc />
        public IEnumerable<IEnchantment> RemovedEnchantments { get { return _removedEnchantments; } }

        /// <inheritdoc />
        public IEnumerable<IEnchantment> ProcessedEnchantments { get { return _processedEnchantments; } }

        /// <inheritdoc />
        public IStatCollection Stats { get { return _stats; } }
        #endregion

        #region Methods
        public static IEnchantmentTypeCalculatorResult Create(
            IEnumerable<IEnchantment> removedEnchantments,
            IEnumerable<IEnchantment> processedEnchantments,
            IStatCollection stats)
        {
            return new EnchantmentTypeCalculatorResult(
                removedEnchantments,
                processedEnchantments,
                stats);
        }
        #endregion
    }
}
