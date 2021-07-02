using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components.Api;
using ProjectXyz.Shared.Framework;

using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Effects
{
    public sealed class SkillTargetCombatTeamGeneratorComponent : IGeneratorComponent
    {
        public SkillTargetCombatTeamGeneratorComponent(
            IEnumerable<int> affectedTeams)
        {
            AffectedTeams = affectedTeams
                .Select(x => new IntIdentifier(x))
                .ToArray();
        }

        public IReadOnlyCollection<IIdentifier> AffectedTeams { get; }
    }

    public sealed class SkillTargetCombatTeamConverter : ISkillEffectBehaviorConverter
    {
        public bool CanConvert(
            IGeneratorComponent component)
        {
            return component is SkillTargetCombatTeamGeneratorComponent;
        }

        public IBehavior ConvertToBehavior(
            IGeneratorComponent component)
        {
            var skillTeamComponent = (SkillTargetCombatTeamGeneratorComponent)component;

            return new TargetCombatTeamBehavior(
                skillTeamComponent.AffectedTeams);
        }
    }
}
