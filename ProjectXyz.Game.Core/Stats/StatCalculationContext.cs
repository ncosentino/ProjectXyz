using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Game.Interface.Stats;

namespace ProjectXyz.Game.Core.Stats
{
    public sealed class StatCalculationContext : IStatCalculationContext
    {
        public StatCalculationContext(
            IEnumerable<IComponent> components,
            IEnumerable<IEnchantment> enchantments)
        {
            Components = new ComponentCollection(components);
            Enchantments = enchantments.ToArray();
        }

        public IReadOnlyCollection<IEnchantment> Enchantments { get; }

        public IComponentCollection Components { get; }
    }
}