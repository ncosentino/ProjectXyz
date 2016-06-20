using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Stats;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class EnchantmentCalculatorResult : IEnchantmentCalculatorResult
    {
        public EnchantmentCalculatorResult(
            IEnumerable<IEnchantment> enchantments,
            IStatCollection stats)
        {
            Enchantments = enchantments.ToArray();
            Stats = stats;
        }

        public IStatCollection Stats { get; }

        public IEnumerable<IEnchantment> Enchantments { get; }
    }
}