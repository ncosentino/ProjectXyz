using System.Collections.Generic;
using ClassLibrary1.Application.Interface.Stats;

namespace ClassLibrary1.Application.Interface.Enchantments
{
    public interface IEnchantmentCalculatorResultFactory
    {
        IEnchantmentCalculatorResult Create(
            IEnumerable<IEnchantment> enchantments,
            IStatCollection stats);
    }
}