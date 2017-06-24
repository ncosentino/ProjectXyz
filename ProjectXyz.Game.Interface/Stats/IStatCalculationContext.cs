using System.Collections.Generic;
using ProjectXyz.Application.Enchantments.Api;
using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Game.Interface.Stats
{
    public interface IStatCalculationContext : IEntity
    {
        IReadOnlyCollection<IEnchantment> Enchantments { get; }
    }
}