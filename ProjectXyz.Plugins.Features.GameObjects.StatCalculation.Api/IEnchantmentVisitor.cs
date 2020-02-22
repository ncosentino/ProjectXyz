using System;
using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api
{
    public interface IEnchantmentVisitor
    {
        IEnumerable<IEnchantment> GetEnchantments(
            IHasBehaviors gameObject,
            Predicate<IHasBehaviors> visited,
            IIdentifier target,
            IIdentifier statId,
            IStatCalculationContext context);
    }
}
