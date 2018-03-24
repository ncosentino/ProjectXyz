using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Stats.Calculations;

namespace ProjectXyz.Api.Behaviors
{
    public interface IHasStatsBehavior : IBehavior
    {
        IReadOnlyDictionary<IIdentifier, double> BaseStats { get; }

        double GetStatValue(
            IStatCalculationContext statCalculationContext,
            IIdentifier statDefinitionId);
    }
}