using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ISkillFactory
    {
        IGameObject Create(
            IReadOnlyTypeIdentifierBehavior typeIdentifierBehavior,
            IReadOnlyTemplateIdentifierBehavior templateIdentifierBehavior,
            IReadOnlyIdentifierBehavior identifierBehavior,
            ISkillResourceUsageBehavior skillResourceUsageBehavior,
            IHasMutableStatsBehavior hasMutableStatsBehavior,
            ISkillTargetModeBehavior skillTargetModeBehavior,
            IHasSkillSynergiesBehavior hasSkillSynergiesBehavior,
            IHasEnchantmentsBehavior hasEnchantmentsBehavior, 
            ISkillPrerequisitesBehavior skillPrerequisitesBehavior,
            ISkillRequirementsBehavior skillRequirementsBehavior,
            IEnumerable<IBehavior> additionalBehaviors);
    }
}