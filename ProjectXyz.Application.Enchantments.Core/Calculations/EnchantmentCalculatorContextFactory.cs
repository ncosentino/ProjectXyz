using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Application.Enchantments.Interface.Calculations;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Enchantments.Core.Calculations
{
    public sealed class EnchantmentCalculatorContextFactory : IEnchantmentCalculatorContextFactory
    {
        private readonly IComponentCollection _componentCollection;

        public EnchantmentCalculatorContextFactory(IEnumerable<IComponent> components)
        {
            _componentCollection = new ComponentCollection(components);
        }

        public IEnchantmentCalculatorContext CreateEnchantmentCalculatorContext(
            IInterval elapsed,
            IReadOnlyCollection<IEnchantment> enchantments)
        {
            return new EnchantmentCalculatorContext(
                _componentCollection,
                elapsed,
                enchantments);
        }
    }
}
