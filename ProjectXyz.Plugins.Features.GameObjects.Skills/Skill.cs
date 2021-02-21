using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ISkillBehaviorsProvider
    {
        IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors);
    }

    public interface ISkillBehaviorsProviderFacade : ISkillBehaviorsProvider
    {
    }

    public interface IDiscoverableSkillBehaviorsProvider : ISkillBehaviorsProvider
    {
    }

    public interface ISkillBehaviorsInterceptor
    {
        void Intercept(IReadOnlyCollection<IBehavior> behaviors);
    }

    public interface ISkillBehaviorsInterceptorFacade : ISkillBehaviorsInterceptor
    {
    }

    public interface IDiscoverableSkillBehaviorsInterceptor : ISkillBehaviorsInterceptor
    {
    }

    public sealed class SkillBehaviorsInterceptorFacade : ISkillBehaviorsInterceptorFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableSkillBehaviorsInterceptor> _interceptors;

        public SkillBehaviorsInterceptorFacade(IEnumerable<IDiscoverableSkillBehaviorsInterceptor> interceptors)
        {
            _interceptors = interceptors.ToArray();
        }

        public void Intercept(IReadOnlyCollection<IBehavior> behaviors)
        {
            foreach (var interceptor in _interceptors)
            {
                interceptor.Intercept(behaviors);
            }
        }
    }

    public sealed class SkillBehaviorsProviderFacade : ISkillBehaviorsProviderFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableSkillBehaviorsProvider> _providers;

        public SkillBehaviorsProviderFacade(IEnumerable<IDiscoverableSkillBehaviorsProvider> providers)
        {
            _providers = providers.ToArray();
        }

        public IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors) =>
            _providers.SelectMany(x => x.GetBehaviors(baseBehaviors));
    }

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

    public sealed class Skill : IGameObject
    {
        public Skill(IEnumerable<IBehavior> behaviors)
        {
            Behaviors = behaviors.ToArray();
        }

        public IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}
