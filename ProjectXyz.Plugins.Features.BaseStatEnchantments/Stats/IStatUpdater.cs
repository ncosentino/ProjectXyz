using System;
using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.BaseStatEnchantments.Stats
{
    public interface IStatUpdater
    {
        void Update(
            IReadOnlyDictionary<IIdentifier, double> baseStats,
            IReadOnlyCollection<IGameObject> enchantments,
            Action<Action<IDictionary<IIdentifier, double>>> mutateStatsCallback,
            double elapsedTurns);
    }
}