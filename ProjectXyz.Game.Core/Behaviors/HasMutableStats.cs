using System;
using System.Collections.Generic;
using ProjectXyz.Application.Stats.Interface;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Game.Interface.Behaviors;

namespace ProjectXyz.Game.Core.Behaviors
{
    public sealed class HasMutableStats : IHasMutableStats
    {
        private readonly IMutableStatsProvider _mutableStatsProvider;

        public HasMutableStats(IMutableStatsProvider mutableStatsProvider)
        {
            _mutableStatsProvider = mutableStatsProvider;
        }

        public IReadOnlyDictionary<IIdentifier, double> Stats => _mutableStatsProvider.Stats;
        
        public void MutateStats(Action<IDictionary<IIdentifier, double>> callback)
        {
            _mutableStatsProvider.UsingMutableStats(callback);
        }
    }
}