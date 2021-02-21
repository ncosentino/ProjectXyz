using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class Skill : IGameObject
    {
        public Skill(
            ISkillResourceUsageBehavior skillResourceUsageBehavior,
            IHasMutableStatsBehavior hasMutableStatsBehavior,
            ISkillTargetModeBehavior skillTargetModeBehavior,
            IHasSkillSynergiesBehavior hasSkillSynergiesBehavior,
            IHasEnchantmentsBehavior hasEnchantmentsBehavior,
            ISkillPrerequisitesBehavior skillPrerequisitesBehavior,
            ISkillRequirementsBehavior skillRequirementsBehavior,
            IEnumerable<IBehavior> additionalBehaviors)
        {
            Behaviors = new IBehavior[]
            {
                skillResourceUsageBehavior,
                hasMutableStatsBehavior,
                skillTargetModeBehavior,
                hasSkillSynergiesBehavior,
                hasEnchantmentsBehavior,
                skillPrerequisitesBehavior,
                skillRequirementsBehavior
            }
            .Concat(additionalBehaviors)
            .ToArray();
        }
        public IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}
