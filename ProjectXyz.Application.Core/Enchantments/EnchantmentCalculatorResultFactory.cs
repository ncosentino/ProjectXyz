using System.Collections.Generic;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Stats;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class EnchantmentCalculatorResultFactory : IEnchantmentCalculatorResultFactory
    {
        public IEnchantmentCalculatorResult Create(
            IEnumerable<IEnchantment> enchantments, 
            IStatCollection stats)
        {
            return new EnchantmentCalculatorResult(
                enchantments,
                stats);
        }
    }
}