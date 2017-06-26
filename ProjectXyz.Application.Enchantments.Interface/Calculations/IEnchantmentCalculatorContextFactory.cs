using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Enchantments.Interface.Calculations
{
    public interface IEnchantmentCalculatorContextFactory
    {
        IEnchantmentCalculatorContext CreateEnchantmentCalculatorContext(
            IInterval elapsed,
            IReadOnlyCollection<IEnchantment> enchantments);
    }
}