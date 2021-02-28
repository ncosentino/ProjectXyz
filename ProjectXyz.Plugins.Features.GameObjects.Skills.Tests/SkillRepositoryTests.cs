using System;
using System.Collections.Generic;
using System.Linq;

using Moq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Synergies;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Tests
{
    public sealed class SkillRepositoryTests
    {
        private readonly ISkillRepository _skillRepository;
        private readonly MockRepository _mockRepository;
        private readonly Mock<IFilterContext> _filterContext;
        private readonly Mock<IHasEnchantmentsBehavior> _hasEnchantmentsBehavior;
        private readonly Mock<IHasMutableStatsBehavior> _hasMutableStatsBehavior;
        private readonly Mock<ISkillDefinition> _skillDefinition;
        private readonly Mock<IGameObject> _skillSynergy;
        private readonly Mock<IGameObject> _skill;
        private readonly Mock<ISkillDefinitionRepositoryFacade> _skillDefinitionRepositoryFacade;
        private readonly Mock<ISkillSynergyRepositoryFacade> _skillSynergyRepositoryFacade;
        private readonly Mock<IFilterContextFactory> _filterContextFactory;
        private readonly Mock<IHasEnchantmentsBehaviorFactory> _hasEnchantmentsBehaviorFactory;
        private readonly Mock<IHasMutableStatsBehaviorFactory> _hasMutableStatsBehaviorFactory;
        private readonly Mock<ISkillFactory> _skillFactory;
        private readonly Mock<IFilterComponentToBehaviorConverter> _filterComponentToBehaviorConverter;
        private readonly Mock<IEnchantmentLoader> _enchantmentLoader;
        private readonly Mock<ISkillIdentifiers> _skillIdentifiers;
        private readonly Mock<IEnchantmentIdentifiers> _enchantmentIdentifiers;

        public SkillRepositoryTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _skillDefinitionRepositoryFacade = _mockRepository.Create<ISkillDefinitionRepositoryFacade>();
            _skillSynergyRepositoryFacade = _mockRepository.Create<ISkillSynergyRepositoryFacade>();
            _filterContextFactory = _mockRepository.Create<IFilterContextFactory>();
            _hasEnchantmentsBehaviorFactory = _mockRepository.Create<IHasEnchantmentsBehaviorFactory>();
            _hasMutableStatsBehaviorFactory = _mockRepository.Create<IHasMutableStatsBehaviorFactory>();
            _skillFactory = _mockRepository.Create<ISkillFactory>();
            _filterComponentToBehaviorConverter = _mockRepository.Create<IFilterComponentToBehaviorConverter>();
            _enchantmentLoader = _mockRepository.Create<IEnchantmentLoader>();
            _skillIdentifiers = _mockRepository.Create<ISkillIdentifiers>();
            _enchantmentIdentifiers = _mockRepository.Create<IEnchantmentIdentifiers>();
            _filterContext = _mockRepository.Create<IFilterContext>();
            _hasEnchantmentsBehavior = _mockRepository.Create<IHasEnchantmentsBehavior>();
            _hasMutableStatsBehavior = _mockRepository.Create<IHasMutableStatsBehavior>();
            _skillDefinition = _mockRepository.Create<ISkillDefinition>();
            _skillSynergy = _mockRepository.Create<IGameObject>();
            _skill = _mockRepository.Create<IGameObject>();
            _skillRepository = new SkillRepository(
                _skillDefinitionRepositoryFacade.Object,
                _skillSynergyRepositoryFacade.Object,
                _filterContextFactory.Object,
                _hasEnchantmentsBehaviorFactory.Object,
                _hasMutableStatsBehaviorFactory.Object,
                _skillFactory.Object,
                _filterComponentToBehaviorConverter.Object,
                _enchantmentLoader.Object,
                _skillIdentifiers.Object,
                _enchantmentIdentifiers.Object);
        }

        [Fact]
        private void GetSkills_NoMatch_Empty()
        {
            _skillDefinitionRepositoryFacade
                .Setup(x => x.GetSkillDefinitions(_filterContext.Object))
                .Returns(new ISkillDefinition[0]);

            var skills = _skillRepository.GetSkills(_filterContext.Object);

            Assert.Empty(skills);
            _mockRepository.VerifyAll();
        }

        [Fact]
        private void GetSkills_HasSynergies_BehaviorPopulated()
        {
            _skillIdentifiers
                .Setup(x => x.SkillSynergyIdentifier)
                .Returns(new StringIdentifier("id"));
            _skillIdentifiers
                .Setup(x => x.SkillTypeIdentifier)
                .Returns(new StringIdentifier("skill"));

            var skillSynergyDefinitionIds = new[]
            {
                new StringIdentifier("skill synergy definition id"),
            };
            _skillDefinition
                .Setup(x => x.SkillSynergyDefinitionIds)
                .Returns(skillSynergyDefinitionIds);
            _skillDefinition
                .Setup(x => x.FilterComponents)
                .Returns(new IFilterComponent[] { });
            _skillDefinition
                .Setup(x => x.Stats)
                .Returns(new Dictionary<IIdentifier, double>());
            _skillDefinition
                .Setup(x => x.SkillDefinitionId)
                .Returns(new StringIdentifier("skill definition id"));
            _skillDefinition
                .Setup(x => x.SkillTargetModeId)
                .Returns(new StringIdentifier("target mode id"));
            _skillDefinition
                .Setup(x => x.StaticResourceRequirements)
                .Returns(new Dictionary<IIdentifier, double>());

            _skillDefinitionRepositoryFacade
                .Setup(x => x.GetSkillDefinitions(_filterContext.Object))
                .Returns(new[] { _skillDefinition.Object });

            _filterContext
                .Setup(x => x.Attributes)
                .Returns(new IFilterAttribute[] { });

            var skillSynergyContext = _mockRepository.Create<IFilterContext>();
            _filterContextFactory
                .Setup(x => x.CreateContext(
                    0,
                    int.MaxValue,
                    It.Is<IEnumerable<IFilterAttribute>>(fas =>
                        fas.Count() == 1 &&
                        fas.Single().Id.Equals(new StringIdentifier("id")) &&
                        (fas.Single().Value is AnyStringCollectionFilterAttributeValue) &&
                        ((AnyStringCollectionFilterAttributeValue)fas.Single().Value).Values.Count == skillSynergyDefinitionIds.Length)))
                .Returns(skillSynergyContext.Object);

            _skillSynergyRepositoryFacade
                .Setup(x => x.GetSkillSynergies(skillSynergyContext.Object))
                .Returns(new[] { _skillSynergy.Object });

            _hasEnchantmentsBehaviorFactory
                .Setup(x => x.Create())
                .Returns(_hasEnchantmentsBehavior.Object);

            _hasMutableStatsBehaviorFactory
                .Setup(x => x.Create())
                .Returns(_hasMutableStatsBehavior.Object);

            _hasMutableStatsBehavior
                .Setup(x => x.MutateStats(It.IsAny<Action<IDictionary<IIdentifier, double>>>()))
                .Callback<Action<IDictionary<IIdentifier, double>>>(x => x.Invoke(new Dictionary<IIdentifier, double>()));

            _skillFactory
                .Setup(x => x.Create(
                    It.Is<ITypeIdentifierBehavior>(b => b.TypeId.Equals(new StringIdentifier("skill"))),
                    It.Is<ITemplateIdentifierBehavior>(b => b.TemplateId.Equals(new StringIdentifier("skill definition id"))),
                    It.Is<IIdentifierBehavior>(b => b.Id.Equals(new StringIdentifier("skill definition id"))),
                    It.IsAny<ISkillResourceUsageBehavior>(),
                    _hasMutableStatsBehavior.Object,
                    It.Is<ISkillTargetModeBehavior>(b => b.TargetModeId.Equals(new StringIdentifier("target mode id"))),
                    It.Is<IHasSkillSynergiesBehavior>(b =>
                        b.SkillSynergies.Count == 1 &&
                        b.SkillSynergies.Single() == _skillSynergy.Object),
                    _hasEnchantmentsBehavior.Object,
                    It.IsAny<ISkillPrerequisitesBehavior>(),
                    It.IsAny<ISkillRequirementsBehavior>(),
                    It.Is<IEnumerable<IBehavior>>(b => b.Count() == 0)))
                .Returns(_skill.Object);

            var skills = _skillRepository
                .GetSkills(_filterContext.Object);
            
            Assert.Single(skills);
            Assert.Equal(_skill.Object, skills.Single());
            _mockRepository.VerifyAll();
        }

        [Fact]
        private void GetSkills_HasStats_BehaviorPopulated()
        {
            _skillIdentifiers
                .Setup(x => x.SkillSynergyIdentifier)
                .Returns(new StringIdentifier("id"));
            _skillIdentifiers
                .Setup(x => x.SkillTypeIdentifier)
                .Returns(new StringIdentifier("skill"));

            var skillSynergyDefinitionIds = new IIdentifier[]
            {
            };
            _skillDefinition
                .Setup(x => x.SkillSynergyDefinitionIds)
                .Returns(skillSynergyDefinitionIds);
            _skillDefinition
                .Setup(x => x.FilterComponents)
                .Returns(new IFilterComponent[] { });
            _skillDefinition
                .Setup(x => x.Stats)
                .Returns(new Dictionary<IIdentifier, double>()
                {
                    [new StringIdentifier("Stat1")] = 123,
                });
            _skillDefinition
                .Setup(x => x.SkillDefinitionId)
                .Returns(new StringIdentifier("skill definition id"));
            _skillDefinition
                .Setup(x => x.SkillTargetModeId)
                .Returns(new StringIdentifier("target mode id"));
            _skillDefinition
                .Setup(x => x.StaticResourceRequirements)
                .Returns(new Dictionary<IIdentifier, double>());

            _skillDefinitionRepositoryFacade
                .Setup(x => x.GetSkillDefinitions(_filterContext.Object))
                .Returns(new[] { _skillDefinition.Object });

            _filterContext
                .Setup(x => x.Attributes)
                .Returns(new IFilterAttribute[] { });

            var skillSynergyContext = _mockRepository.Create<IFilterContext>();
            _filterContextFactory
                .Setup(x => x.CreateContext(
                    0,
                    int.MaxValue,
                    It.Is<IEnumerable<IFilterAttribute>>(fas =>
                        fas.Count() == 1 &&
                        fas.Single().Id.Equals(new StringIdentifier("id")) &&
                        (fas.Single().Value is AnyStringCollectionFilterAttributeValue) &&
                        ((AnyStringCollectionFilterAttributeValue)fas.Single().Value).Values.Count == skillSynergyDefinitionIds.Length)))
                .Returns(skillSynergyContext.Object);

            _skillSynergyRepositoryFacade
                .Setup(x => x.GetSkillSynergies(skillSynergyContext.Object))
                .Returns(new IGameObject[] { });

            _hasEnchantmentsBehaviorFactory
                .Setup(x => x.Create())
                .Returns(_hasEnchantmentsBehavior.Object);

            _hasMutableStatsBehaviorFactory
                .Setup(x => x.Create())
                .Returns(_hasMutableStatsBehavior.Object);

            var mutatedStats = new Dictionary<IIdentifier, double>();
            _hasMutableStatsBehavior
                .Setup(x => x.MutateStats(It.IsAny<Action<IDictionary<IIdentifier, double>>>()))
                .Callback<Action<IDictionary<IIdentifier, double>>>(x => x.Invoke(mutatedStats));

            _skillFactory
                .Setup(x => x.Create(
                    It.Is<ITypeIdentifierBehavior>(b => b.TypeId.Equals(new StringIdentifier("skill"))),
                    It.Is<ITemplateIdentifierBehavior>(b => b.TemplateId.Equals(new StringIdentifier("skill definition id"))),
                    It.Is<IIdentifierBehavior>(b => b.Id.Equals(new StringIdentifier("skill definition id"))),
                    It.IsAny<ISkillResourceUsageBehavior>(),
                    _hasMutableStatsBehavior.Object,
                    It.Is<ISkillTargetModeBehavior>(b => b.TargetModeId.Equals(new StringIdentifier("target mode id"))),
                    It.Is<IHasSkillSynergiesBehavior>(b => b.SkillSynergies.Count == 0),
                    _hasEnchantmentsBehavior.Object,
                    It.IsAny<ISkillPrerequisitesBehavior>(),
                    It.IsAny<ISkillRequirementsBehavior>(),
                    It.Is<IEnumerable<IBehavior>>(b => b.Count() == 0)))
                .Returns(_skill.Object);

            var skills = _skillRepository
                .GetSkills(_filterContext.Object);

            Assert.Single(skills);
            Assert.Equal(_skill.Object, skills.Single());

            Assert.Equal(1, mutatedStats.Count);
            Assert.True(
                mutatedStats.TryGetValue(new StringIdentifier("Stat1"), out var statValue),
                "Expected stat was not found.");
            Assert.Equal(123d, statValue);

            _mockRepository.VerifyAll();
        }

        [Fact]
        private void GetSkills_PassiveHasMultipleEnchantmentBehaviors_EnchantmentsAreCombinedWithStateful()
        {
            _skillIdentifiers
                .Setup(x => x.SkillSynergyIdentifier)
                .Returns(new StringIdentifier("id"));
            _skillIdentifiers
                .Setup(x => x.SkillTypeIdentifier)
                .Returns(new StringIdentifier("skill"));

            var filterComponent = _mockRepository.Create<IFilterComponent>();

            var enchantment1 = _mockRepository.Create<IEnchantment>();
            var enchantmentsBehavior1 = _mockRepository.Create<IHasEnchantmentsBehavior>();
            enchantmentsBehavior1
                .Setup(x => x.Enchantments)
                .Returns(new[] { enchantment1.Object });

            var enchantment2 = _mockRepository.Create<IEnchantment>();
            var enchantmentsBehavior2 = _mockRepository.Create<IHasReadOnlyEnchantmentsBehavior>();
            enchantmentsBehavior2
                .Setup(x => x.Enchantments)
                .Returns(new[] { enchantment2.Object });

            var statefulEnchantmentDefinitionIds = new[]
            {
                new StringIdentifier("stateful enchantment")
            };

            var enchantment3 = _mockRepository.Create<IEnchantment>();
            _enchantmentLoader
                .Setup(x => x.LoadForEnchantmenDefinitionIds(statefulEnchantmentDefinitionIds))
                .Returns(new[] { enchantment3.Object });

            var passiveBehavior = _mockRepository.Create<IPassiveSkillBehavior>();

            _filterComponentToBehaviorConverter
                .Setup(x => x.Convert(filterComponent.Object))
                .Returns(new IBehavior[]
                {
                    enchantmentsBehavior1.Object,
                    enchantmentsBehavior2.Object,
                    passiveBehavior.Object,
                });

            var skillSynergyDefinitionIds = new IIdentifier[]
            {
            };
            _skillDefinition
                .Setup(x => x.SkillSynergyDefinitionIds)
                .Returns(skillSynergyDefinitionIds);
            _skillDefinition
                .Setup(x => x.FilterComponents)
                .Returns(new IFilterComponent[] 
                {
                    filterComponent.Object,
                });
            _skillDefinition
                .Setup(x => x.Stats)
                .Returns(new Dictionary<IIdentifier, double>());
            _skillDefinition
                .Setup(x => x.SkillDefinitionId)
                .Returns(new StringIdentifier("skill definition id"));
            _skillDefinition
                .Setup(x => x.SkillTargetModeId)
                .Returns(new StringIdentifier("target mode id"));
            _skillDefinition
                .Setup(x => x.StaticResourceRequirements)
                .Returns(new Dictionary<IIdentifier, double>());
            _skillDefinition
                .Setup(x => x.StatefulEnchantmentDefinitions)
                .Returns(statefulEnchantmentDefinitionIds);

            _skillDefinitionRepositoryFacade
                .Setup(x => x.GetSkillDefinitions(_filterContext.Object))
                .Returns(new[] { _skillDefinition.Object });

            _filterContext
                .Setup(x => x.Attributes)
                .Returns(new IFilterAttribute[] { });

            var skillSynergyContext = _mockRepository.Create<IFilterContext>();
            _filterContextFactory
                .Setup(x => x.CreateContext(
                    0,
                    int.MaxValue,
                    It.Is<IEnumerable<IFilterAttribute>>(fas =>
                        fas.Count() == 1 &&
                        fas.Single().Id.Equals(new StringIdentifier("id")) &&
                        (fas.Single().Value is AnyStringCollectionFilterAttributeValue) &&
                        ((AnyStringCollectionFilterAttributeValue)fas.Single().Value).Values.Count == skillSynergyDefinitionIds.Length)))
                .Returns(skillSynergyContext.Object);

            _skillSynergyRepositoryFacade
                .Setup(x => x.GetSkillSynergies(skillSynergyContext.Object))
                .Returns(new IGameObject[] { });

            _hasEnchantmentsBehaviorFactory
                .Setup(x => x.Create())
                .Returns(_hasEnchantmentsBehavior.Object);

            _hasMutableStatsBehaviorFactory
                .Setup(x => x.Create())
                .Returns(_hasMutableStatsBehavior.Object);

            _hasEnchantmentsBehavior
                .Setup(x => x.AddEnchantments(It.Is<IEnumerable<IEnchantment>>(e =>
                    e.Count() == 1 &&
                    e.Single() == enchantment1.Object)));
            _hasEnchantmentsBehavior
                .Setup(x => x.AddEnchantments(It.Is<IEnumerable<IEnchantment>>(e =>
                    e.Count() == 1 &&
                    e.Single() == enchantment2.Object)));
            _hasEnchantmentsBehavior
                .Setup(x => x.AddEnchantments(It.Is<IEnumerable<IEnchantment>>(e =>
                    e.Count() == 1 &&
                    e.Single() == enchantment3.Object)));

            var mutatedStats = new Dictionary<IIdentifier, double>();
            _hasMutableStatsBehavior
                .Setup(x => x.MutateStats(It.IsAny<Action<IDictionary<IIdentifier, double>>>()))
                .Callback<Action<IDictionary<IIdentifier, double>>>(x => x.Invoke(mutatedStats));

            _skillFactory
                .Setup(x => x.Create(
                    It.Is<ITypeIdentifierBehavior>(b => b.TypeId.Equals(new StringIdentifier("skill"))),
                    It.Is<ITemplateIdentifierBehavior>(b => b.TemplateId.Equals(new StringIdentifier("skill definition id"))),
                    It.Is<IIdentifierBehavior>(b => b.Id.Equals(new StringIdentifier("skill definition id"))),
                    It.IsAny<ISkillResourceUsageBehavior>(),
                    _hasMutableStatsBehavior.Object,
                    It.Is<ISkillTargetModeBehavior>(b => b.TargetModeId.Equals(new StringIdentifier("target mode id"))),
                    It.Is<IHasSkillSynergiesBehavior>(b => b.SkillSynergies.Count == 0),
                    _hasEnchantmentsBehavior.Object,
                    It.IsAny<ISkillPrerequisitesBehavior>(),
                    It.IsAny<ISkillRequirementsBehavior>(),
                    It.Is<IEnumerable<IBehavior>>(b => 
                        b.Count() == 1 &&
                        b.Single() == passiveBehavior.Object)))
                .Returns(_skill.Object);

            var skills = _skillRepository
                .GetSkills(_filterContext.Object);

            Assert.Single(skills);
            Assert.Equal(_skill.Object, skills.Single());

            _mockRepository.VerifyAll();
        }

        [Fact]
        private void GetSkills_ActiveSkillHasMultipleEnchantmentBehaviors_EnchantmentsAreCombinedWithoutStateless()
        {
            _skillIdentifiers
                .Setup(x => x.SkillSynergyIdentifier)
                .Returns(new StringIdentifier("id"));
            _skillIdentifiers
                .Setup(x => x.SkillTypeIdentifier)
                .Returns(new StringIdentifier("skill"));

            var filterComponent = _mockRepository.Create<IFilterComponent>();

            var enchantment1 = _mockRepository.Create<IEnchantment>();
            var enchantmentsBehavior1 = _mockRepository.Create<IHasEnchantmentsBehavior>();
            enchantmentsBehavior1
                .Setup(x => x.Enchantments)
                .Returns(new[] { enchantment1.Object });

            var enchantment2 = _mockRepository.Create<IEnchantment>();
            var enchantmentsBehavior2 = _mockRepository.Create<IHasReadOnlyEnchantmentsBehavior>();
            enchantmentsBehavior2
                .Setup(x => x.Enchantments)
                .Returns(new[] { enchantment2.Object });

            _filterComponentToBehaviorConverter
                .Setup(x => x.Convert(filterComponent.Object))
                .Returns(new IBehavior[]
                {
                    enchantmentsBehavior1.Object,
                    enchantmentsBehavior2.Object
                });

            var skillSynergyDefinitionIds = new IIdentifier[]
            {
            };
            _skillDefinition
                .Setup(x => x.SkillSynergyDefinitionIds)
                .Returns(skillSynergyDefinitionIds);
            _skillDefinition
                .Setup(x => x.FilterComponents)
                .Returns(new IFilterComponent[]
                {
                    filterComponent.Object,
                });
            _skillDefinition
                .Setup(x => x.Stats)
                .Returns(new Dictionary<IIdentifier, double>());
            _skillDefinition
                .Setup(x => x.SkillDefinitionId)
                .Returns(new StringIdentifier("skill definition id"));
            _skillDefinition
                .Setup(x => x.SkillTargetModeId)
                .Returns(new StringIdentifier("target mode id"));
            _skillDefinition
                .Setup(x => x.StaticResourceRequirements)
                .Returns(new Dictionary<IIdentifier, double>());

            _skillDefinitionRepositoryFacade
                .Setup(x => x.GetSkillDefinitions(_filterContext.Object))
                .Returns(new[] { _skillDefinition.Object });

            _filterContext
                .Setup(x => x.Attributes)
                .Returns(new IFilterAttribute[] { });

            var skillSynergyContext = _mockRepository.Create<IFilterContext>();
            _filterContextFactory
                .Setup(x => x.CreateContext(
                    0,
                    int.MaxValue,
                    It.Is<IEnumerable<IFilterAttribute>>(fas =>
                        fas.Count() == 1 &&
                        fas.Single().Id.Equals(new StringIdentifier("id")) &&
                        (fas.Single().Value is AnyStringCollectionFilterAttributeValue) &&
                        ((AnyStringCollectionFilterAttributeValue)fas.Single().Value).Values.Count == skillSynergyDefinitionIds.Length)))
                .Returns(skillSynergyContext.Object);

            _skillSynergyRepositoryFacade
                .Setup(x => x.GetSkillSynergies(skillSynergyContext.Object))
                .Returns(new IGameObject[] { });

            _hasEnchantmentsBehaviorFactory
                .Setup(x => x.Create())
                .Returns(_hasEnchantmentsBehavior.Object);

            _hasMutableStatsBehaviorFactory
                .Setup(x => x.Create())
                .Returns(_hasMutableStatsBehavior.Object);

            _hasEnchantmentsBehavior
                .Setup(x => x.AddEnchantments(It.Is<IEnumerable<IEnchantment>>(e =>
                    e.Count() == 1 &&
                    e.Single() == enchantment1.Object)));
            _hasEnchantmentsBehavior
                .Setup(x => x.AddEnchantments(It.Is<IEnumerable<IEnchantment>>(e =>
                    e.Count() == 1 &&
                    e.Single() == enchantment2.Object)));

            var mutatedStats = new Dictionary<IIdentifier, double>();
            _hasMutableStatsBehavior
                .Setup(x => x.MutateStats(It.IsAny<Action<IDictionary<IIdentifier, double>>>()))
                .Callback<Action<IDictionary<IIdentifier, double>>>(x => x.Invoke(mutatedStats));

            _skillFactory
                .Setup(x => x.Create(
                    It.Is<ITypeIdentifierBehavior>(b => b.TypeId.Equals(new StringIdentifier("skill"))),
                    It.Is<ITemplateIdentifierBehavior>(b => b.TemplateId.Equals(new StringIdentifier("skill definition id"))),
                    It.Is<IIdentifierBehavior>(b => b.Id.Equals(new StringIdentifier("skill definition id"))),
                    It.IsAny<ISkillResourceUsageBehavior>(),
                    _hasMutableStatsBehavior.Object,
                    It.Is<ISkillTargetModeBehavior>(b => b.TargetModeId.Equals(new StringIdentifier("target mode id"))),
                    It.Is<IHasSkillSynergiesBehavior>(b => b.SkillSynergies.Count == 0),
                    _hasEnchantmentsBehavior.Object,
                    It.IsAny<ISkillPrerequisitesBehavior>(),
                    It.IsAny<ISkillRequirementsBehavior>(),
                    It.Is<IEnumerable<IBehavior>>(b => b.Count() == 0)))
                .Returns(_skill.Object);

            var skills = _skillRepository
                .GetSkills(_filterContext.Object);

            Assert.Single(skills);
            Assert.Equal(_skill.Object, skills.Single());

            _mockRepository.VerifyAll();
        }
    }
}
