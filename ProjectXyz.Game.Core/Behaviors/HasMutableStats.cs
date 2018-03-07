using System;
using System.Collections.Generic;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Game.Interface.Behaviors;
using ProjectXyz.Game.Interface.Stats;

namespace ProjectXyz.Game.Core.Behaviors
{
    public sealed class HasMutableStats : IHasMutableStats
    {
        private readonly IStatManager _statManager;

        public HasMutableStats(IStatManager statManager)
        {
            _statManager = statManager;
        }

        public IReadOnlyDictionary<IIdentifier, double> BaseStats => _statManager.BaseStats;

        public double GetStatValue(
            IStatCalculationContext statCalculationContext,
            IIdentifier statDefinitionId) => _statManager.GetValue(
                statCalculationContext,
                statDefinitionId);

        public void MutateStats(Action<IDictionary<IIdentifier, double>> callback)
        {
            _statManager.UsingMutableStats(callback);
        }
    }
}