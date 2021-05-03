using System.Collections.Generic;

using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api
{
    public delegate double CalculateStatDelegate(
        IGameObject gameObject,
        IReadOnlyCollection<IGameObject> enchantments,
        IIdentifier statId,
        IStatCalculationContext context);
}
