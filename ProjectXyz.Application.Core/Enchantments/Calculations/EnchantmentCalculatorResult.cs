using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Application.Core.Enchantments.Calculations
{
    public sealed class EnchantmentCalculatorResult : IEnchantmentCalculatorResult
    {
        #region Fields
        private readonly List<IEnchantment> _enchantments;
        private readonly IStatCollection _stats;
        #endregion

        #region Constructors
        private EnchantmentCalculatorResult(
            IEnumerable<IEnchantment> enchantments,
            IStatCollection stats)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Requires<ArgumentNullException>(stats != null);

            _enchantments = new List<IEnchantment>(enchantments);
            _stats = stats;
        }
        #endregion

        #region Properties
        public IEnumerable<IEnchantment> Enchantments { get { return _enchantments; } }

        public IStatCollection Stats { get { return _stats; } }
        #endregion

        #region Methods
        public static IEnchantmentCalculatorResult Create(
            IEnumerable<IEnchantment> enchantments,
            IStatCollection stats)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Ensures(Contract.Result<IEnchantmentCalculatorResult>() != null);

            return new EnchantmentCalculatorResult(
                enchantments,
                stats);
        }
        #endregion
    }
}
