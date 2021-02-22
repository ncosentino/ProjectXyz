using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class SkillFactory : ISkillFactory
    {
        private readonly ISkillBehaviorsInterceptorFacade _skillBehaviorsInterceptorFacade;
        private readonly ISkillBehaviorsProviderFacade _skillBehaviorsProviderFacade;
        private readonly IBehaviorManager _behaviorManager;

        public SkillFactory(
            ISkillBehaviorsInterceptorFacade skillBehaviorsInterceptorFacade,
            ISkillBehaviorsProviderFacade skillBehaviorsProviderFacade,
            IBehaviorManager behaviorManager)
        {
            _skillBehaviorsInterceptorFacade = skillBehaviorsInterceptorFacade;
            _skillBehaviorsProviderFacade = skillBehaviorsProviderFacade;
            _behaviorManager = behaviorManager;
        }

        public IGameObject Create(
            ISkillResourceUsageBehavior skillResourceUsageBehavior,
            IHasMutableStatsBehavior hasMutableStatsBehavior,
            ISkillTargetModeBehavior skillTargetModeBehavior,
            IHasSkillSynergiesBehavior hasSkillSynergiesBehavior,
            IHasEnchantmentsBehavior hasEnchantmentsBehavior,
            ISkillPrerequisitesBehavior skillPrerequisitesBehavior,
            ISkillRequirementsBehavior skillRequirementsBehavior,
            IEnumerable<IBehavior> additionalBehaviors)
        {
            var baseAndInjectedBehaviours = new IBehavior[]
                {
                    skillResourceUsageBehavior,
                    hasMutableStatsBehavior,
                    skillTargetModeBehavior,
                    hasSkillSynergiesBehavior,
                    hasEnchantmentsBehavior,
                    skillPrerequisitesBehavior,
                    skillRequirementsBehavior,
                }
                .Concat(additionalBehaviors)
                .ToArray();
            var additionalBehaviorsFromProviders = _skillBehaviorsProviderFacade
                .GetBehaviors(baseAndInjectedBehaviours);
            var allBehaviors = baseAndInjectedBehaviours
                .Concat(additionalBehaviorsFromProviders)
                .ToArray();
            _skillBehaviorsInterceptorFacade.Intercept(allBehaviors);

            var skill = new Skill(allBehaviors);
            _behaviorManager.Register(skill, allBehaviors);
            return skill;
        }
    }
}
