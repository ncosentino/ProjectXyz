using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Framework.Entities;

namespace ProjectXyz.Plugins.Enchantments.Stats
{
    public sealed class StatCalculationContext : IStatCalculationContext
    {
        public StatCalculationContext(
            IEnumerable<IComponent> components,
            IEnumerable<IGameObject> enchantments)
        {
            Components = new ComponentCollection(components);
            Enchantments = enchantments.ToArray();
        }

        public IReadOnlyCollection<IGameObject> Enchantments { get; }

        public IComponentCollection Components { get; }
    }
}