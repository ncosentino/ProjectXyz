using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Synergies
{
    public sealed class SkillSynergyDefinitionIdBehavior :
        BaseBehavior,
        ISkillSynergyDefinitionIdBehavior
    {
        public SkillSynergyDefinitionIdBehavior(IIdentifier skillSynergyDefinitionId)
        {
            SkillSynergyDefinitionId = skillSynergyDefinitionId;
        }

        public IIdentifier SkillSynergyDefinitionId { get; }
    }
}
