using System.Collections.Generic;

using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api
{
    public delegate IReadOnlyCollection<IGameObject> GetEnchantmentsDelegate(
        IStatVisitor statVisitor,
        IEnchantmentVisitor enchantmentVisitor,
        IGameObject gameObject,
        IIdentifier target,
        IIdentifier statId,
        IStatCalculationContext context);
}
