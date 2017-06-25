using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Game.Interface.Stats
{
    public interface IStatCalculationContext : IEntity
    {
        IReadOnlyCollection<IEnchantment> Enchantments { get; }
    }
}