using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        Task UsingMutableStatsAsync(Func<IDictionary<IIdentifier, double>, Task> callback);
    }
}