using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework.Entities;

namespace ProjectXyz.Api.Enchantments.Stats
{
    public interface IStatCalculationContext : IEntity
    {
        IReadOnlyCollection<IEnchantment> Enchantments { get; }
    }
}