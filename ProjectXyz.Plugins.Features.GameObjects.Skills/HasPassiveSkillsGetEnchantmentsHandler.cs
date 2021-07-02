using System.Linq;

using Autofac;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Effects;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class HasPassiveSkillsGetEnchantmentsHandler : IDiscoverableGetEnchantmentsHandler
    {
        private readonly IEnchantmentFactory _enchantmentFactory;
        private readonly ICalculationPriorityFactory _calculationPriorityFactory;

        public HasPassiveSkillsGetEnchantmentsHandler(
            IEnchantmentFactory enchantmentFactory,
            ICalculationPriorityFactory calculationPriorityFactory,
            ITargetNavigator targetNavigator,
            IStatDefinitionToTermConverter statDefinitionToTermConverter)
        {
            GetEnchantments = (statVisitor, enchantmentVisitor, behaviors, target, statId, context) =>
            {
                var hasSkillsBehavior = behaviors.GetOnly<IHasSkillsBehavior>();
                var skillEffectBehaviors = hasSkillsBehavior
                    .Skills
                    .SelectMany(x => x.Behaviors)
                    .TakeTypes<ISkillEffectBehavior>()
                    .SelectMany(x => x
                        .EffectExecutors
                        .SelectMany(effExObj => effExObj
                            .Behaviors
                            .TakeTypes<ISkillEffectExecutorBehavior>()
                            .SelectMany(effEx => effEx
                            .Effects
                            .Where(effect => effect.Has<IPassiveSkillEffectBehavior>()))))
                    .ToArray();
                var passiveEnchantmentBehaviors = skillEffectBehaviors
                    .SelectMany(x => x.Get<IHasEnchantmentsBehavior>())
                    .ToArray();

                // multiple levels of depth here we need to elaborate to
                // explain what's going on. a skill has skill effect behaviors.
                // each skill effect behavior contains an effect executor
                // behavior. each effect executor behavior has enchantments.
                // therefore, in order for a passive skill to take effect on an
                // actor it's like:
                // owner.owner.owner.self
                // (Effect -> EffectExecutor -> Skill -> Actor)
                // confusing? yes.
                // FIXME: perfect example of why this is really difficult to
                // use and we should revisit this
                var skillsTarget = targetNavigator.NavigateDown(target);
                skillsTarget = targetNavigator.NavigateDown(skillsTarget);
                skillsTarget = targetNavigator.NavigateDown(skillsTarget);
                var passiveSkillsOwnerEnchantments = passiveEnchantmentBehaviors
                    .SelectMany(x => enchantmentVisitor.GetEnchantments(
                        x.Owner,
                        y => y.Equals(behaviors), // TODO: handle visited
                        skillsTarget,
                        statId,
                        context))
                    .ToList();

                var passiveSkillStats = passiveEnchantmentBehaviors
                    .Where(x => x.Owner.Has<IHasStatsBehavior>())
                    .Select(x => statVisitor.GetStatValue(
                        x.Owner,
                        statId,
                        context))
                    .ToArray();
                var statTerm = statDefinitionToTermConverter[statId];

                var enchantmentValue = passiveSkillStats.Sum();
                var allPassiveSkillEnchantments = passiveSkillsOwnerEnchantments;
                if (enchantmentValue != 0)
                {
                    var passiveSkillsStatsEnchantment = _enchantmentFactory.Create(
                    new IBehavior[]
                    {
                        new EnchantmentTargetBehavior(skillsTarget),
                        new HasStatDefinitionIdBehavior()
                        {
                            StatDefinitionId = statId,
                        },
                        new EnchantmentExpressionBehavior(
                            _calculationPriorityFactory.Create<int>(-1),
                            $"{statTerm} + {enchantmentValue}")
                    });
                    allPassiveSkillEnchantments.Add(passiveSkillsStatsEnchantment);
                }
                
                return allPassiveSkillEnchantments;
            };
            _enchantmentFactory = enchantmentFactory;
            _calculationPriorityFactory = calculationPriorityFactory;
        }

        public CanGetEnchantmentsDelegate CanGetEnchantments { get; } =
            behaviors => behaviors.Has<IHasSkillsBehavior>();

        public GetEnchantmentsDelegate GetEnchantments { get; }
    }
}
