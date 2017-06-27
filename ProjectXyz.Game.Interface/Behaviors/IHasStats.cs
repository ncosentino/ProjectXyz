using System.Collections.Generic;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Game.Interface.Stats;

namespace ProjectXyz.Game.Interface.Behaviors
{
    public interface IHasStats : IBehavior
    {
        IReadOnlyDictionary<IIdentifier, double> BaseStats { get; }

        double GetValue(
            IStatCalculationContext statCalculationContext,
            IIdentifier statDefinitionId);
    }
}