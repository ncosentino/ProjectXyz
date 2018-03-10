using System;
using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Game.Interface.Stats
{
    public interface IStatUpdater
    {
        void Update(
            IReadOnlyDictionary<IIdentifier, double> baseStats,
            IReadOnlyCollection<IEnchantment> enchantments,
            Action<Action<IDictionary<IIdentifier, double>>> mutateStatsCallback,
            IInterval elapsed);
    }
}