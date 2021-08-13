using System.Collections.Generic;

using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Api.Enchantments.Calculations
{
    public interface IEnchantmentCalculatorContextFactory
    {
        IEnchantmentCalculatorContext CreateEnchantmentCalculatorContext(
            double elapsedTurns,
            IReadOnlyCollection<IGameObject> enchantments);

        IEnchantmentCalculatorContext CreateEnchantmentCalculatorContext(
            double elapsedTurns,
            IReadOnlyCollection<IGameObject> enchantments,
            IEnumerable<IComponent> additionalComponents);
    }
}