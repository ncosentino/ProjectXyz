using System.Collections.Generic;
using ProjectXyz.Application.Stats.Interface;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Game.Interface.Behaviors;

namespace ProjectXyz.Game.Core.Behaviors
{
    public sealed class HasStats : IHasStats
    {
        private readonly IStatsProvider _statsProvider;

        public HasStats(IStatsProvider statsProvider)
        {
            _statsProvider = statsProvider;
        }

        public IReadOnlyDictionary<IIdentifier, double> Stats => _statsProvider.Stats;
    }
}