﻿using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Framework.Entities;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations
{
    public sealed class EnchantmentCalculatorContextFactory : IEnchantmentCalculatorContextFactory
    {
        private readonly IComponentCollection _componentCollection;

        public EnchantmentCalculatorContextFactory(IEnumerable<IComponent> components)
        {
            _componentCollection = new ComponentCollection(components);
        }

        public IEnchantmentCalculatorContext CreateEnchantmentCalculatorContext(
            double elapsedTurns,
            IReadOnlyCollection<IGameObject> enchantments)
        {
            return CreateEnchantmentCalculatorContext(
                elapsedTurns,
                enchantments,
                Enumerable.Empty<IComponent>());
        }

        public IEnchantmentCalculatorContext CreateEnchantmentCalculatorContext(
            double elapsedTurns,
            IReadOnlyCollection<IGameObject> enchantments,
            IEnumerable<IComponent> additionalComponents)
        {
            return new EnchantmentCalculatorContext(
                _componentCollection.Concat(additionalComponents),
                elapsedTurns,
                enchantments);
        }
    }
}
