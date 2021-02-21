using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ISkillFactory
    {
        IGameObject Create(ISkillResourceUsageBehavior skillResourceUsageBehavior, IHasMutableStatsBehavior hasMutableStatsBehavior, ISkillTargetModeBehavior skillTargetModeBehavior, IHasSkillSynergiesBehavior hasSkillSynergiesBehavior, IHasEnchantmentsBehavior hasEnchantmentsBehavior, ISkillPrerequisitesBehavior skillPrerequisitesBehavior, ISkillRequirementsBehavior skillRequirementsBehavior, IEnumerable<IBehavior> additionalBehaviors);
    }
}