using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api
{
    public delegate double CalculateStatDelegate(
        IHasBehaviors behaviors,
        IReadOnlyCollection<IEnchantment> enchantments,
        IIdentifier statId,
        IStatCalculationContext context);
}
