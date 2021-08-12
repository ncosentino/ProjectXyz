using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Stats;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class HasStatsBehavior :
        BaseBehavior,
        IHasStatsBehavior
    {
        private readonly IStatManager _statManager;

        public HasStatsBehavior(IStatManager statManager)
        {
            _statManager = statManager;
            _statManager.BaseStatsChanged += async (s, e) => await BaseStatsChanged
                .InvokeOrderedAsync(this, e)
                .ConfigureAwait(false);
        }

        public event EventHandler<StatsChangedEventArgs> BaseStatsChanged;

        public IReadOnlyDictionary<IIdentifier, double> BaseStats => _statManager.BaseStats;

        public double GetStatValue(
            IStatCalculationContext statCalculationContext,
            IIdentifier statDefinitionId) => _statManager.GetValue(
                statCalculationContext,
                statDefinitionId);

        public async Task MutateStatsAsync(Func<IDictionary<IIdentifier, double>, Task> callback)
        {
            await _statManager.UsingMutableStatsAsync(callback).ConfigureAwait(false);
        }
    }
}