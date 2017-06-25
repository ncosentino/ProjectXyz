using System;
using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Application.Enchantments.Interface.Calculations;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Enchantments.Core.Calculations
{
    public sealed class EnchantmentCalculatorContextFactory : IEnchantmentCalculatorContextFactory
    {
        public IEnchantmentCalculatorContext CreateEnchantmentCalculatorContext(
            IEnumerable<IComponent> components,
            IInterval elapsed,
            IReadOnlyCollection<IEnchantment> enchantments)
        {
            return new EnchantmentCalculatorContext(
                components,
                elapsed,
                enchantments);
        }
    }
}
