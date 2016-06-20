using System.Collections.Generic;
using System.Linq;
using ClassLibrary1.Application.Interface.Enchantments;
using ClassLibrary1.Application.Interface.Stats;

namespace ClassLibrary1.Application.Core.Enchantments
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