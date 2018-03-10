using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Game.Interface.Stats;

namespace ProjectXyz.Game.Interface.Behaviors
{
    public interface IHasStats : IBehavior
    {
        IReadOnlyDictionary<IIdentifier, double> BaseStats { get; }

        double GetStatValue(
            IStatCalculationContext statCalculationContext,
            IIdentifier statDefinitionId);
    }
}