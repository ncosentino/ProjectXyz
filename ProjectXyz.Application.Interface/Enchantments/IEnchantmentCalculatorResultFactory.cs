using System.Collections.Generic;
using ProjectXyz.Application.Interface.Stats;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentCalculatorResultFactory
    {
        IEnchantmentCalculatorResult Create(
            IEnumerable<IEnchantment> enchantments,
            IStatCollection stats);
    }
}