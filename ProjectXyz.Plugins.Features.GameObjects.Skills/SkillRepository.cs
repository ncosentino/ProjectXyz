using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Synergies;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class SkillRepository : ISkillRepository
    {
        private readonly IGeneratorComponentToBehaviorConverterFacade _filterComponentToBehaviorConverter;
        private readonly ISkillDefinitionRepositoryFacade _skillDefinitionRepositoryFacade;
        private readonly ISkillSynergyRepositoryFacade _skillSynergyRepositoryFacade;
        private readonly IFilterContextFactory _filterContextFactory;
        private readonly IHasEnchantmentsBehaviorFactory _hasEnchantmentsBehaviorFactory;
        private readonly IHasMutableStatsBehaviorFactory _hasMutableStatsBehaviorFactory;
        private readonly ISkillFactory _skillFactory;
        private readonly IEnchantmentLoader _enchantmentLoader;
        private readonly ISkillIdentifiers _skillIdentifiers;
        private readonly IEnchantmentIdentifiers _enchantmentIdentifiers;

        public SkillRepository(
            ISkillDefinitionRepositoryFacade skillDefinitionRepositoryFacade,
            ISkillSynergyRepositoryFacade skillSynergyRepositoryFacade,
            IFilterContextFactory filterContextFactory,
            IHasEnchantmentsBehaviorFactory hasEnchantmentsBehaviorFactory,
            IHasMutableStatsBehaviorFactory hasMutableStatsBehaviorFactory,
            ISkillFactory skillFactory,
            IGeneratorComponentToBehaviorConverterFacade filterComponentToBehaviorConverter,
            IEnchantmentLoader enchantmentLoader,
            ISkillIdentifiers skillIdentifiers,
            IEnchantmentIdentifiers enchantmentIdentifiers)
        {
            _skillDefinitionRepositoryFacade = skillDefinitionRepositoryFacade;
            _skillSynergyRepositoryFacade = skillSynergyRepositoryFacade;
            _filterContextFactory = filterContextFactory;
            _hasEnchantmentsBehaviorFactory = hasEnchantmentsBehaviorFactory;
            _hasMutableStatsBehaviorFactory = hasMutableStatsBehaviorFactory;
            _skillFactory = skillFactory;
            _filterComponentToBehaviorConverter = filterComponentToBehaviorConverter;
            _enchantmentLoader = enchantmentLoader;
            _skillIdentifiers = skillIdentifiers;
            _enchantmentIdentifiers = enchantmentIdentifiers;
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

            var hasMutableStats = CreateStatsBehavior(skillDefinition);

            var filterComponentBehaviors = _filterComponentToBehaviorConverter.Convert(
                Enumerable.Empty<IBehavior>(),
                skillDefinition.FilterComponents);
            var additionalBehaviors = filterComponentBehaviors
                .Concat(new IBehavior[]
                {
                })
                .ToArray();

            // copy out all of the enchantments from the definition defined in behaviors
            var hasEnchantmentsBehavior = _hasEnchantmentsBehaviorFactory.Create();
            foreach (var definitionHasReadOnlyEnchantmentsBehavior in additionalBehaviors
                .TakeTypes<IHasReadOnlyEnchantmentsBehavior>())
            {
                hasEnchantmentsBehavior.AddEnchantments(definitionHasReadOnlyEnchantmentsBehavior.Enchantments);
            }

            // skip these since we took their contents
            additionalBehaviors = additionalBehaviors
                .Where(x => !(x is IHasReadOnlyEnchantmentsBehavior))
                .ToArray();

            // if we have a passive skill, we can load up the stateful 
            // enchantments directly since we don't need to ever activate a 
            // passive sill (i.e. copy their enchantments to another collection)
            // and their bonuses are always active
            if (additionalBehaviors.Has<IPassiveSkillBehavior>())
            {
                var statefulEnchantments = GetStatefulEnchantments(skillDefinition.StatefulEnchantmentDefinitions);
                hasEnchantmentsBehavior.AddEnchantments(statefulEnchantments);
            }

            var skill = _skillFactory.Create(
                new TypeIdentifierBehavior(_skillIdentifiers.SkillTypeIdentifier),
                new TemplateIdentifierBehavior(skillDefinition.SkillDefinitionId),
                new IdentifierBehavior(skillDefinition.SkillDefinitionId),
                // FIXME: how to add & define modifiers (D3 & Wolcen style? i.e. increase range, change element)
                new SkillResourceUsageBehavior(skillDefinition.StaticResourceRequirements),
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
                additionalBehaviors);            
            return skill;
        }

        private IEnumerable<IGameObject> GetStatefulEnchantments(IEnumerable<IIdentifier> enchantmentDefinitionIds)
        {
            return _enchantmentLoader.LoadForEnchantmenDefinitionIds(enchantmentDefinitionIds);
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
                    _skillIdentifiers.SkillSynergyIdentifier,
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
