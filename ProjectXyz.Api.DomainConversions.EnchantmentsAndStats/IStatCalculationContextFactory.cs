using System.Collections.Generic;

using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Api.Enchantments.Stats
{
    public interface IStatCalculationContextFactory
    {
        IStatCalculationContext Create(
            IEnumerable<IComponent> components,
            IEnumerable<IGameObject> enchantments);
    }
}