using System;
using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Stats;

namespace ProjectXyz.Api.Enchantments.Stats
{
    public interface IStatManager
    {
        event EventHandler<StatsChangedEventArgs> BaseStatsChanged;

        IReadOnlyDictionary<IIdentifier, double> BaseStats { get; }

        double GetValue(
            IStatCalculationContext statCalculationContext,
            IIdentifier statDefinitionId);

        void UsingMutableStats(Action<IDictionary<IIdentifier, double>> callback);
    }
}