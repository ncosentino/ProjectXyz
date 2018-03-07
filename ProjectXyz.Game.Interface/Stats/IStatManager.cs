using System;
using System.Collections.Generic;
using ProjectXyz.Framework.Interface;

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