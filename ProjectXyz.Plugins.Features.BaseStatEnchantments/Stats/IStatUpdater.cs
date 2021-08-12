using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.BaseStatEnchantments.Stats
{
    public interface IStatUpdater
    {
        Task UpdateAsync(
            IReadOnlyDictionary<IIdentifier, double> baseStats,
            IReadOnlyCollection<IGameObject> enchantments,
            Func<Func<IDictionary<IIdentifier, double>, Task>, Task> mutateStatsCallback,
            double elapsedTurns);
    }
}