using System.Collections.Generic;
using ProjectXyz.Application.Stats.Interface;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Game.Interface.Behaviors;
using ProjectXyz.Game.Interface.Stats;

namespace ProjectXyz.Game.Core.Behaviors
{
    public sealed class HasStats : IHasStats
    {
        private readonly IStatsProvider _statsProvider;
        private readonly IStatManager _statManager;

        public HasStats(IStatsProvider statsProvider, IStatManager statManager)
        {
            _statsProvider = statsProvider;
            _statManager = statManager;
        }

        public IReadOnlyDictionary<IIdentifier, double> BaseStats => _statsProvider.Stats;

        public double GetValue(
            IStatCalculationContext statCalculationContext,
            IIdentifier statDefinitionId)
        {
            return _statManager.GetValue(statCalculationContext, statDefinitionId);
        }
    }
}