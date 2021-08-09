using System.Collections.Generic;
using System.Linq;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Effects;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class SkillFactory : ISkillFactory
    {
        private readonly ISkillBehaviorsInterceptorFacade _skillBehaviorsInterceptorFacade;
        private readonly ISkillBehaviorsProviderFacade _skillBehaviorsProviderFacade;
        private readonly IGameObjectFactory _gameObjectFactory;

        public SkillFactory(
            ISkillBehaviorsInterceptorFacade skillBehaviorsInterceptorFacade,
            ISkillBehaviorsProviderFacade skillBehaviorsProviderFacade,
            IGameObjectFactory gameObjectFactory)
        {
            _skillBehaviorsInterceptorFacade = skillBehaviorsInterceptorFacade;
            _skillBehaviorsProviderFacade = skillBehaviorsProviderFacade;
            _gameObjectFactory = gameObjectFactory;
        }

        public IGameObject Create(
            IReadOnlyTypeIdentifierBehavior typeIdentifierBehavior,
            IReadOnlyTemplateIdentifierBehavior templateIdentifierBehavior,
            IReadOnlyIdentifierBehavior identifierBehavior,
            ISkillResourceUsageBehavior skillResourceUsageBehavior,
            IHasStatsBehavior hasStatsBehavior,
            ISkillTargetModeBehavior skillTargetModeBehavior,
            IHasSkillSynergiesBehavior hasSkillSynergiesBehavior,
            IHasEnchantmentsBehavior hasEnchantmentsBehavior,
            ISkillPrerequisitesBehavior skillPrerequisitesBehavior,
            ISkillRequirementsBehavior skillRequirementsBehavior,
            IEnumerable<IBehavior> additionalBehaviors)
        {
            var baseAndInjectedBehaviours = new IBehavior[]
                {
                    typeIdentifierBehavior,
                    templateIdentifierBehavior,
                    identifierBehavior,
                    skillResourceUsageBehavior,
                    hasStatsBehavior,
                    skillTargetModeBehavior,
                    hasSkillSynergiesBehavior,
                    hasEnchantmentsBehavior,
                    skillPrerequisitesBehavior,
                    skillRequirementsBehavior,
                }
                .Concat(additionalBehaviors)
                .ToArray();

            Contract.Requires(
                () => baseAndInjectedBehaviours.Any(x => x is IPassiveSkillEffectBehavior) ||
                    !hasEnchantmentsBehavior.Enchantments.Any(),
                () => $"Passive skills are the only ones that can have enchantments " +
                $"as state. Did you forget to add '{typeof(IPassiveSkillEffectBehavior)}'?");

            var additionalBehaviorsFromProviders = _skillBehaviorsProviderFacade
                .GetBehaviors(baseAndInjectedBehaviours);
            var allBehaviors = baseAndInjectedBehaviours
                .Concat(additionalBehaviorsFromProviders)
                .ToArray();
            _skillBehaviorsInterceptorFacade.Intercept(allBehaviors);

            var skill = _gameObjectFactory.Create(allBehaviors);
            return skill;
        }
    }
}
