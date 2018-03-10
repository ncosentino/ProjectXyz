using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;

namespace ProjectXyz.Api.Enchantments.Calculations
{
    public interface IEnchantmentCalculatorContextFactory
    {
        IEnchantmentCalculatorContext CreateEnchantmentCalculatorContext(
            IInterval elapsed,
            IReadOnlyCollection<IEnchantment> enchantments);

        IEnchantmentCalculatorContext CreateEnchantmentCalculatorContext(
            IInterval elapsed,
            IReadOnlyCollection<IEnchantment> enchantments,
            IEnumerable<IComponent> additionalComponents);
    }
}