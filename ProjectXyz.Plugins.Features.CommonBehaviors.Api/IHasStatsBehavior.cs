using System.Collections.Generic;

using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IHasStatsBehavior : IBehavior
    {
        IReadOnlyDictionary<IIdentifier, double> BaseStats { get; }

        double GetStatValue(
            IStatCalculationContext statCalculationContext,
            IIdentifier statDefinitionId);
    }
}