using System;
using System.Collections.Generic;
using ProjectXyz.Application.Stats.Interface;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Game.Interface.Behaviors;
using ProjectXyz.Game.Interface.Stats;

namespace ProjectXyz.Game.Core.Behaviors
{
    public sealed class HasMutableStats : IHasMutableStats
    {
        private readonly IMutableStatsProvider _mutableStatsProvider;
        private readonly IStatManager _statManager;

        public HasMutableStats(
            IMutableStatsProvider mutableStatsProvider,
            IStatManager statManager)
        {
            _mutableStatsProvider = mutableStatsProvider;
            _statManager = statManager;
        }

        public IReadOnlyDictionary<IIdentifier, double> BaseStats => _mutableStatsProvider.Stats;

        public double GetValue(
            IStatCalculationContext statCalculationContext,
            IIdentifier statDefinitionId)
        {
            return _statManager.GetValue(statCalculationContext, statDefinitionId);
        }

        public void MutateStats(Action<IDictionary<IIdentifier, double>> callback)
        {
            _mutableStatsProvider.UsingMutableStats(callback);
        }
    }
}