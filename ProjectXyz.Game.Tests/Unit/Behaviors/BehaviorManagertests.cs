using System.Collections.Generic;
using Moq;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Shared.Behaviors;
using Xunit;

namespace ProjectXyz.Game.Tests.Unit.Behaviors
{
    public sealed class BehaviorManagertests
    {
        private readonly BehaviorManager _behaviorManager;
        private readonly MockRepository _mockRepository;
        private readonly Mock<IHasBehaviors> _mockHasBehaviors;

        public BehaviorManagertests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _mockHasBehaviors = _mockRepository.Create<IHasBehaviors>();
            _behaviorManager = new BehaviorManager();
        }

        [Fact]
        private void Register_SingleBehavior_RegistrationInCorrectOrder()
        {
            int registeredCalls = 0;
            int registeringCalls = 0;

            var mockBehavior = _mockRepository.Create<IBehavior>();
            mockBehavior
                .Setup(x => x.RegisteringToOwner(_mockHasBehaviors.Object))
                .Callback<IHasBehaviors>(_ =>
                {
                    Assert.Equal(0, registeredCalls);
                    Assert.Equal(0, registeringCalls);
                    registeringCalls++;
                });
            mockBehavior
                .Setup(x => x.RegisteredToOwner(_mockHasBehaviors.Object))
                .Callback<IHasBehaviors>(_ =>
                {
                    Assert.Equal(0, registeredCalls);
                    Assert.Equal(1, registeringCalls);
                    registeredCalls++;
                });

            var behaviors = new List<IBehavior>()
            {
                mockBehavior.Object,
            };

            _behaviorManager.Register(
                _mockHasBehaviors.Object,
                behaviors);

            _mockRepository.VerifyAll();
            Assert.Equal(1, registeringCalls);
            Assert.Equal(1, registeredCalls);
        }

        [Fact]
        private void Register_MultipleBehaviors_RegistrationInCorrectOrder()
        {
            int registeredCalls = 0;
            int registeringCalls = 0;

            var mockBehavior1 = _mockRepository.Create<IBehavior>();
            var mockBehavior2 = _mockRepository.Create<IBehavior>();

            mockBehavior1
                .Setup(x => x.RegisteringToOwner(_mockHasBehaviors.Object))
                .Callback<IHasBehaviors>(_ =>
                {
                    Assert.Equal(0, registeredCalls);
                    Assert.Equal(0, registeringCalls);
                    registeringCalls++;
                });
            mockBehavior1
                .Setup(x => x.RegisteredToOwner(_mockHasBehaviors.Object))
                .Callback<IHasBehaviors>(_ =>
                {
                    Assert.Equal(0, registeredCalls);
                    Assert.Equal(2, registeringCalls);
                    registeredCalls++;
                });

            mockBehavior2
                .Setup(x => x.RegisteringToOwner(_mockHasBehaviors.Object))
                .Callback<IHasBehaviors>(_ =>
                {
                    Assert.Equal(0, registeredCalls);
                    Assert.Equal(1, registeringCalls);
                    registeringCalls++;
                });
            mockBehavior2
                .Setup(x => x.RegisteredToOwner(_mockHasBehaviors.Object))
                .Callback<IHasBehaviors>(_ =>
                {
                    Assert.Equal(1, registeredCalls);
                    Assert.Equal(2, registeringCalls);
                    registeredCalls++;
                });

            var behaviors = new List<IBehavior>()
            {
                mockBehavior1.Object,
                mockBehavior2.Object,
            };

            _behaviorManager.Register(
                _mockHasBehaviors.Object,
                behaviors);

            _mockRepository.VerifyAll();
            Assert.Equal(2, registeringCalls);
            Assert.Equal(2, registeredCalls);
        }
    }
}
