using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Synergies;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class SkillRepository : ISkillRepository
    {
        private readonly ISkillDefinitionRepositoryFacade _skillDefinitionRepositoryFacade;
        private readonly ISkillSynergyRepositoryFacade _skillSynergyRepositoryFacade;
        private readonly IFilterContextFactory _filterContextFactory;
        private readonly IHasEnchantmentsBehaviorFactory _hasEnchantmentsBehaviorFactory;
        private readonly IHasMutableStatsBehaviorFactory _hasMutableStatsBehaviorFactory;
        private readonly ISkillFactory _skillFactory;
        private readonly IEnchantmentLoader _enchantmentLoader;

        public SkillRepository(
            ISkillDefinitionRepositoryFacade skillDefinitionRepositoryFacade,
            ISkillSynergyRepositoryFacade skillSynergyRepositoryFacade,
            IFilterContextFactory filterContextFactory,
            IHasEnchantmentsBehaviorFactory hasEnchantmentsBehaviorFactory,
            IHasMutableStatsBehaviorFactory hasMutableStatsBehaviorFactory,
            ISkillFactory skillFactory,
            IEnchantmentLoader enchantmentLoader)
        {
            _skillDefinitionRepositoryFacade = skillDefinitionRepositoryFacade;
            _skillSynergyRepositoryFacade = skillSynergyRepositoryFacade;
            _filterContextFactory = filterContextFactory;
            _hasEnchantmentsBehaviorFactory = hasEnchantmentsBehaviorFactory;
            _hasMutableStatsBehaviorFactory = hasMutableStatsBehaviorFactory;
            _skillFactory = skillFactory;
            _enchantmentLoader = enchantmentLoader;
        }

        public IEnumerable<IGameObject> GetSkills(IFilterContext filterContext)
        {
            foreach (var skillDefinition in _skillDefinitionRepositoryFacade.GetSkillDefinitions(filterContext))
            {
                var skill = SkillFromDefinition(
                    filterContext,
                    skillDefinition);
                yield return skill;
            }
        }

        private IGameObject SkillFromDefinition(
            IFilterContext filterContext,
            ISkillDefinition skillDefinition)
        {
            var skillSynergies = GetSkillSynergies(
                filterContext,
                skillDefinition);

            var hasEnchantmentsBehavior = _hasEnchantmentsBehaviorFactory.Create();
            hasEnchantmentsBehavior.AddEnchantments(GetSkillEnchantments(
                filterContext,
                skillDefinition));

            var hasMutableStats = CreateStatsBehavior(skillDefinition);

            var skill = _skillFactory.Create(
                new TypeIdentifierBehavior(new StringIdentifier("skill")),
                new TemplateIdentifierBehavior(new StringIdentifier("skill")),
                new IdentifierBehavior(skillDefinition.SkillDefinitionId),
                // FIXME: modifiers (D3 & Wolcen style?)
                new SkillResourceUsageBehavior(),
                hasMutableStats,
                new SkillTargetModeBehavior(skillDefinition.SkillTargetModeId),
                new HasSkillSynergiesBehavior(skillSynergies),
                hasEnchantmentsBehavior,
                new SkillPrerequisitesBehavior(new IFilterAttribute[]
                {
                    // FIXME: load this from the definition
                    //new FilterAttribute(
                    //    new StringIdentifier("actor-stats"),
                    //    new ActorStatFilterAttributeValue(
                    //        new StringIdentifier("FIXME: how to define the owner here?"),
                    //        new StringIdentifier("LEVEL"),
                    //        10,
                    //        int.MaxValue),
                    //    true),
                    //new FilterAttribute(
                    //    new StringIdentifier("actor-skills"),
                    //    new ActorStatFilterAttributeValue(
                    //        new StringIdentifier("FIXME: how to define the owner here?"),
                    //        new StringIdentifier("skill-slash"),
                    //        10,
                    //        int.MaxValue),
                    //    true),
                }),
                new SkillRequirementsBehavior(new IFilterAttribute[]
                {
                    // FIXME: load this from the definition
                    //new FilterAttribute(
                    //    new StringIdentifier("actor-equipment"), // FIXME: check the player holding a weapon
                    //    new ActorStatFilterAttributeValue(
                    //        new StringIdentifier("FIXME: how to define the owner here?"),
                    //        new StringIdentifier("LEVEL"),
                    //        10,
                    //        int.MaxValue),
                    //    true),
                }),
                new IBehavior[]
                {
                    new AuraSkillBehavior(),
                    new PassiveSkillBehavior(),
                });            
            return skill;
        }

        private IEnumerable<IEnchantment> GetSkillEnchantments(
            IFilterContext filterContext,
            ISkillDefinition skillDefinition)
        {
            var enchantmentsFilterContext = _filterContextFactory
                .CreateFilterContextForAnyAmount(filterContext
                .Attributes
                .Select(x => x.Required ? x.CopyWithRequired(false) : x)
                .AppendSingle(new FilterAttribute(
                    new StringIdentifier("skill-definition-id"),
                    // FIXME: use ID filtering here not strings
                    new IdentifierFilterAttributeValue(skillDefinition.SkillDefinitionId),
                    true)));
            var enchantments = _enchantmentLoader.Load(enchantmentsFilterContext);
            return enchantments;
        }

        private IHasMutableStatsBehavior CreateStatsBehavior(ISkillDefinition skillDefinition)
        {
            var hasMutableStats = _hasMutableStatsBehaviorFactory.Create();
            hasMutableStats.MutateStats(stats =>
            {
                foreach (var stat in skillDefinition.Stats)
                {
                    stats[stat.Key] = stat.Value;
                }
            });
            return hasMutableStats;
        }

        private IEnumerable<IGameObject> GetSkillSynergies(
            IFilterContext filterContext,
            ISkillDefinition skillDefinition)
        {
            var skillSynergyFilterContext = _filterContextFactory
                .CreateFilterContextForAnyAmount(filterContext
                .Attributes
                .Select(x => x.Required ? x.CopyWithRequired(false) : x)
                .AppendSingle(new FilterAttribute(
                    new StringIdentifier("id"),
                    // FIXME: use ID filtering here not strings
                    new AnyStringCollectionFilterAttributeValue(skillDefinition
                        .SkillSynergyDefinitionIds
                        .Select(x => x.ToString())),
                    true)));
            var skillSynergies = _skillSynergyRepositoryFacade.GetSkillSynergies(skillSynergyFilterContext);
            return skillSynergies;
        }
    }
}
