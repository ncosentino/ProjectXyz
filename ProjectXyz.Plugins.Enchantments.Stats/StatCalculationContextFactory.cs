using System.Collections.Generic;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework.Entities;

namespace ProjectXyz.Plugins.Enchantments.Stats
{
    public sealed class StatCalculationContextFactory : IStatCalculationContextFactory
    {
        public IStatCalculationContext Create(
            IEnumerable<IComponent> components,
            IEnumerable<IEnchantment> enchantments)
        {
            var context = new StatCalculationContext(
                components,
                enchantments);
            return context;
        }
    }
}