using System;
using System.Collections.Generic;

using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api
{
    public interface IEnchantmentVisitor
    {
        IEnumerable<IGameObject> GetEnchantments(
            IGameObject gameObject,
            Predicate<IGameObject> visited,
            IIdentifier target,
            IIdentifier statId,
            IStatCalculationContext context);
    }
}
