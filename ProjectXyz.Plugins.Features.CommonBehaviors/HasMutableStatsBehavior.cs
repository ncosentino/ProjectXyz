using System;
using System.Collections.Generic;

using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
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