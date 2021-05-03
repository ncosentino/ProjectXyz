using System.Collections.Generic;

using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Api.Enchantments.Stats
{
    public interface IStatCalculationContext : IEntity
    {
        IReadOnlyCollection<IGameObject> Enchantments { get; }
    }
}