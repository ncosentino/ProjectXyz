using System.Linq;

using Autofac;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations;
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
                var passiveSkillBehaviors = hasSkillsBehavior
                    .Skills
                    .SelectMany(x => x.Behaviors)
                    .TakeTypes<IPassiveSkillBehavior>()
                    .ToArray();

                var skillsTarget = targetNavigator.NavigateDown(target);
                var passiveSkillsOwnerEnchantments = passiveSkillBehaviors
                    .SelectMany(x => enchantmentVisitor.GetEnchantments(
                        x.Owner,
                        y => y.Equals(behaviors), // TODO: handle visited
                        skillsTarget,
                        statId,
                        context))
                    .ToArray();

                var passiveSkillStats = passiveSkillBehaviors
                    .Where(x => x.Owner.Has<IHasStatsBehavior>())
                    .Select(x => statVisitor.GetStatValue(
                        x.Owner,
                        statId,
                        context))
                    .ToArray();
                var statTerm = statDefinitionToTermConverter[statId];
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
                            $"{statTerm} + {passiveSkillStats.Sum()}")
                    });
                var allPassiveSkillEnchantments = passiveSkillsOwnerEnchantments
                    .AppendSingle(passiveSkillsStatsEnchantment)
                    .ToArray();
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
