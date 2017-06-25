using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Enchantments.Interface.Calculations
{
    public interface IEnchantmentCalculatorContextFactory
    {
        IEnchantmentCalculatorContext CreateEnchantmentCalculatorContext(
            IEnumerable<IComponent> components,
            IInterval elapsed,
            IReadOnlyCollection<IEnchantment> enchantments);
    }
}