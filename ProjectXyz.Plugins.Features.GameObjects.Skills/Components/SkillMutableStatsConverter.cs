using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components.Api;
using ProjectXyz.Shared.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Components
{
    public sealed class SkillMutableStatsGeneratorComponent : IGeneratorComponent
    {
        public SkillMutableStatsGeneratorComponent(
            IIdentifier identifier,
            double cost) : this(new Dictionary<IIdentifier, double>() { { identifier, cost } })
        {
        }

        public SkillMutableStatsGeneratorComponent(
            IReadOnlyDictionary<IIdentifier, double> requirements)
        {
            Stats = requirements
                .ToDictionary(
                    x => x.Key,
                    x => x.Value);
        }

        public IReadOnlyDictionary<IIdentifier, double> Stats { get; }
    }


    public sealed class SkillMutableStatsConverter : IBehaviorConverter
    {
        private readonly IHasStatsBehaviorFactory _hasMutableStatsBehaviorFactory;

        public SkillMutableStatsConverter(
            IHasStatsBehaviorFactory hasMutableStatsBehaviorFactory)
        {
            _hasMutableStatsBehaviorFactory = hasMutableStatsBehaviorFactory;
        }

        public bool CanConvert(
            IGeneratorComponent component)
        {
            return component is SkillMutableStatsGeneratorComponent;
        }

        public IBehavior ConvertToBehavior(
            IGeneratorComponent component)
        {
            var statsComponent = (SkillMutableStatsGeneratorComponent)component;

            var hasMutableStats = _hasMutableStatsBehaviorFactory.Create();
            hasMutableStats.MutateStats(stats =>
            {
                foreach (var stat in statsComponent.Stats)
                {
                    stats[stat.Key] = stat.Value;
                }
            });

            return hasMutableStats;
        }
    }
}
