using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class HasStatsBehavior :
        BaseBehavior,
        IHasStatsBehavior
    {
        private readonly IStatManager _statManager;

        public HasStatsBehavior(
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