using System;
using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Stats.Calculations;

namespace ProjectXyz.Game.Interface.Stats
{
    public interface IStatManager
    {
        IReadOnlyDictionary<IIdentifier, double> BaseStats { get; }

        double GetValue(
            IStatCalculationContext statCalculationContext,
            IIdentifier statDefinitionId);

        void UsingMutableStats(Action<IDictionary<IIdentifier, double>> callback);
    }
}