using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Game.Interface.Behaviors;
using ProjectXyz.Game.Interface.Stats;

namespace ProjectXyz.Game.Core.Behaviors
{
    public sealed class HasStats :
        BaseBehavior,
        IHasStats
    {
        private readonly IStatManager _statManager;

        public HasStats(
            IStatManager statManager)
        {
            _statManager = statManager;
        }

        public IReadOnlyDictionary<IIdentifier, double> BaseStats => _statManager.BaseStats;

        public double GetStatValue(
            IStatCalculationContext statCalculationContext,
            IIdentifier statDefinitionId) => _statManager.GetValue(
            statCalculationContext,
            statDefinitionId);
    }
}