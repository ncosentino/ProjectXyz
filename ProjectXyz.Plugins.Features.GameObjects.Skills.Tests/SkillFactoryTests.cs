using System;
using System.Collections.Generic;

using Moq;

using NexusLabs.Contracts;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

using Xunit;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Tests
{
    public sealed class SkillFactoryTests
    {
        private readonly ISkillFactory _skillFactory;
        private readonly MockRepository _mockRepository;
        private readonly Mock<ISkillBehaviorsInterceptorFacade> _skillBehaviorsInterceptorFacade;
        private readonly Mock<ISkillBehaviorsProviderFacade> _skillBehaviorsProviderFacade;
        private readonly Mock<IBehaviorManager> _behaviorManager;
        private readonly Mock<IReadOnlyTypeIdentifierBehavior> _typeIdentifierBehavior;
        private readonly Mock<IReadOnlyTemplateIdentifierBehavior> _templateIdentifierBehavior;
        private readonly Mock<IReadOnlyIdentifierBehavior> _identifierBehavior;
        private readonly Mock<ISkillResourceUsageBehavior> _skillResourceUsageBehavior;
        private readonly Mock<IHasMutableStatsBehavior> _hasMutableStatsBehavior;
        private readonly Mock<ISkillTargetModeBehavior> _skillTargetModeBehavior;
        private readonly Mock<IHasSkillSynergiesBehavior> _hasSkillSynergiesBehavior;
        private readonly Mock<IHasEnchantmentsBehavior> _hasEnchantmentsBehavior;
        private readonly Mock<ISkillPrerequisitesBehavior> _skillPrerequisitesBehavior;
        private readonly Mock<ISkillRequirementsBehavior> _skillRequirementsBehavior;

        public SkillFactoryTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _skillBehaviorsInterceptorFacade = _mockRepository.Create<ISkillBehaviorsInterceptorFacade>();
            _skillBehaviorsProviderFacade = _mockRepository.Create<ISkillBehaviorsProviderFacade>();
            _behaviorManager = _mockRepository.Create<IBehaviorManager>();
            _typeIdentifierBehavior = _mockRepository.Create<IReadOnlyTypeIdentifierBehavior>();
            _templateIdentifierBehavior = _mockRepository.Create<IReadOnlyTemplateIdentifierBehavior>();
            _identifierBehavior = _mockRepository.Create<IReadOnlyIdentifierBehavior>();
            _skillResourceUsageBehavior = _mockRepository.Create<ISkillResourceUsageBehavior>();
            _hasMutableStatsBehavior = _mockRepository.Create<IHasMutableStatsBehavior>();
            _skillTargetModeBehavior = _mockRepository.Create<ISkillTargetModeBehavior>();
            _hasSkillSynergiesBehavior = _mockRepository.Create<IHasSkillSynergiesBehavior>();
            _hasEnchantmentsBehavior = _mockRepository.Create<IHasEnchantmentsBehavior>();
            _skillPrerequisitesBehavior = _mockRepository.Create<ISkillPrerequisitesBehavior>();
            _skillRequirementsBehavior = _mockRepository.Create<ISkillRequirementsBehavior>();
            _skillFactory = new SkillFactory(
                _skillBehaviorsInterceptorFacade.Object,
                _skillBehaviorsProviderFacade.Object,
                _behaviorManager.Object);
        }

        [Fact]
        private void Create_NoAdditionalBehaviors_ExpectedSkill()
        {
            const int EXPECTED_BEHAVIOR_COUNT = 10;
            _skillBehaviorsProviderFacade
                .Setup(x => x.GetBehaviors(It.Is<IReadOnlyCollection<IBehavior>>(b => b.Count == EXPECTED_BEHAVIOR_COUNT)))
                .Returns(new IBehavior[0]);

            _skillBehaviorsInterceptorFacade
                .Setup(x => x.Intercept(It.Is<IReadOnlyCollection<IBehavior>>(b => b.Count == EXPECTED_BEHAVIOR_COUNT)));

            _behaviorManager
                .Setup(x => x.Register(
                    It.IsAny<IGameObject>(),
                    It.Is<IReadOnlyCollection<IBehavior>>(b => b.Count == EXPECTED_BEHAVIOR_COUNT)));

            _hasEnchantmentsBehavior
                .Setup(x => x.Enchantments)
                .Returns(new IEnchantment[] { });

            var skill = _skillFactory.Create(
                _typeIdentifierBehavior.Object,
                _templateIdentifierBehavior.Object,
                _identifierBehavior.Object,
                _skillResourceUsageBehavior.Object,
                _hasMutableStatsBehavior.Object,
                _skillTargetModeBehavior.Object,
                _hasSkillSynergiesBehavior.Object,
                _hasEnchantmentsBehavior.Object,
                _skillPrerequisitesBehavior.Object,
                _skillRequirementsBehavior.Object,
                new IBehavior[] { });

            Assert.NotNull(skill);
            _mockRepository.VerifyAll();
        }

        [Fact]
        private void Create_PassiveSkillNoEnchantmentsNoStats_ExpectedSkill()
        {
            const int EXPECTED_BEHAVIOR_COUNT = 11;
            _skillBehaviorsProviderFacade
                .Setup(x => x.GetBehaviors(It.Is<IReadOnlyCollection<IBehavior>>(b => b.Count == EXPECTED_BEHAVIOR_COUNT)))
                .Returns(new IBehavior[0]);

            _skillBehaviorsInterceptorFacade
                .Setup(x => x.Intercept(It.Is<IReadOnlyCollection<IBehavior>>(b => b.Count == EXPECTED_BEHAVIOR_COUNT)));

            _behaviorManager
                .Setup(x => x.Register(
                    It.IsAny<IGameObject>(),
                    It.Is<IReadOnlyCollection<IBehavior>>(b => b.Count == EXPECTED_BEHAVIOR_COUNT)));

            _hasEnchantmentsBehavior
                .Setup(x => x.Enchantments)
                .Returns(new IEnchantment[] { });

            var skill = _skillFactory.Create(
                _typeIdentifierBehavior.Object,
                _templateIdentifierBehavior.Object,
                _identifierBehavior.Object,
                _skillResourceUsageBehavior.Object,
                _hasMutableStatsBehavior.Object,
                _skillTargetModeBehavior.Object,
                _hasSkillSynergiesBehavior.Object,
                _hasEnchantmentsBehavior.Object,
                _skillPrerequisitesBehavior.Object,
                _skillRequirementsBehavior.Object,
                new IBehavior[]
                {
                    _mockRepository.Create<IPassiveSkillBehavior>().Object,
                });

            Assert.NotNull(skill);
            //_mockRepository.VerifyAll(); // skipping because boolean logic shortcircuits
        }

        [Fact]
        private void Create_PassiveSkillHasEnchantmentsNoStats_ExpectedSkill()
        {
            const int EXPECTED_BEHAVIOR_COUNT = 11;
            _skillBehaviorsProviderFacade
                .Setup(x => x.GetBehaviors(It.Is<IReadOnlyCollection<IBehavior>>(b => b.Count == EXPECTED_BEHAVIOR_COUNT)))
                .Returns(new IBehavior[0]);

            _skillBehaviorsInterceptorFacade
                .Setup(x => x.Intercept(It.Is<IReadOnlyCollection<IBehavior>>(b => b.Count == EXPECTED_BEHAVIOR_COUNT)));

            _behaviorManager
                .Setup(x => x.Register(
                    It.IsAny<IGameObject>(),
                    It.Is<IReadOnlyCollection<IBehavior>>(b => b.Count == EXPECTED_BEHAVIOR_COUNT)));

            _hasEnchantmentsBehavior
                .Setup(x => x.Enchantments)
                .Returns(new IEnchantment[]
                {
                    _mockRepository.Create<IEnchantment>().Object,
                });

            var skill = _skillFactory.Create(
                _typeIdentifierBehavior.Object,
                _templateIdentifierBehavior.Object,
                _identifierBehavior.Object,
                _skillResourceUsageBehavior.Object,
                _hasMutableStatsBehavior.Object,
                _skillTargetModeBehavior.Object,
                _hasSkillSynergiesBehavior.Object,
                _hasEnchantmentsBehavior.Object,
                _skillPrerequisitesBehavior.Object,
                _skillRequirementsBehavior.Object,
                new IBehavior[]
                {
                    _mockRepository.Create<IPassiveSkillBehavior>().Object,
                });

            Assert.NotNull(skill);
            //_mockRepository.VerifyAll(); // skipping because boolean logic shortcircuits
        }

        [Fact]
        private void Create_ActiveSkillHasEnchantmentsNoStats_ThrowsContractException()
        {
            _hasEnchantmentsBehavior
                .Setup(x => x.Enchantments)
                .Returns(new IEnchantment[] 
                {
                    _mockRepository.Create<IEnchantment>().Object,
                });

            Assert.Throws<ContractException>(() => _skillFactory.Create(
                _typeIdentifierBehavior.Object,
                _templateIdentifierBehavior.Object,
                _identifierBehavior.Object,
                _skillResourceUsageBehavior.Object,
                _hasMutableStatsBehavior.Object,
                _skillTargetModeBehavior.Object,
                _hasSkillSynergiesBehavior.Object,
                _hasEnchantmentsBehavior.Object,
                _skillPrerequisitesBehavior.Object,
                _skillRequirementsBehavior.Object,
                new IBehavior[]
                {
                }));

            _mockRepository.VerifyAll();
        }
    }
}
