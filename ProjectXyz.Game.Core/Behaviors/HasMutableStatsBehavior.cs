using System;
using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Stats.Calculations;
using ProjectXyz.Game.Interface.Stats;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Game.Core.Behaviors
{
    public sealed class HasMutableStatsBehavior :
        BaseBehavior,
        IHasMutableStatsBehavior
    {
        private readonly IStatManager _statManager;

        public HasMutableStatsBehavior(IStatManager statManager)
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