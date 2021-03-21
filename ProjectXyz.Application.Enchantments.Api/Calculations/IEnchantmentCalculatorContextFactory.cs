using System.Collections.Generic;
using ProjectXyz.Api.Framework.Entities;

namespace ProjectXyz.Api.Enchantments.Calculations
{
    public interface IEnchantmentCalculatorContextFactory
    {
        IEnchantmentCalculatorContext CreateEnchantmentCalculatorContext(
            double elapsedTurns,
            IReadOnlyCollection<IEnchantment> enchantments);

        IEnchantmentCalculatorContext CreateEnchantmentCalculatorContext(
            double elapsedTurns,
            IReadOnlyCollection<IEnchantment> enchantments,
            IEnumerable<IComponent> additionalComponents);
    }
}