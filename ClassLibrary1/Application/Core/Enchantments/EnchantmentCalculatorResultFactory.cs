using System.Collections.Generic;
using ClassLibrary1.Application.Interface.Enchantments;
using ClassLibrary1.Application.Interface.Stats;

namespace ClassLibrary1.Application.Core.Enchantments
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