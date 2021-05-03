using System.Collections.Generic;

using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Enchantments.Stats
{
    public sealed class StatCalculationContextFactory : IStatCalculationContextFactory
    {
        public IStatCalculationContext Create(
            IEnumerable<IComponent> components,
            IEnumerable<IGameObject> enchantments)
        {
            var context = new StatCalculationContext(
                components,
                enchantments);
            return context;
        }
    }
}