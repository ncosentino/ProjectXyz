using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Shared.Framework.Entities;

namespace ProjectXyz.Shared.Game.GameObjects.Enchantments.Calculations
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
            return CreateEnchantmentCalculatorContext(
                elapsed,
                enchantments,
                Enumerable.Empty<IComponent>());
        }

        public IEnchantmentCalculatorContext CreateEnchantmentCalculatorContext(
            IInterval elapsed,
            IReadOnlyCollection<IEnchantment> enchantments,
            IEnumerable<IComponent> additionalComponents)
        {
            return new EnchantmentCalculatorContext(
                _componentCollection.Concat(additionalComponents),
                elapsed,
                enchantments);
        }
    }
}
