using System.Collections.Generic;

using ProjectXyz.Api.Framework.Entities;

namespace ProjectXyz.Api.Enchantments.Stats
{
    public interface IStatCalculationContextFactory
    {
        IStatCalculationContext Create(
            IEnumerable<IComponent> components,
            IEnumerable<IEnchantment> enchantments);
    }
}