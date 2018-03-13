using System;
using System.Collections.Generic;
using Moq;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Game.Core.Behaviors;
using Xunit;

namespace ProjectXyz.Game.Tests.Unit.Behaviors
{
    public sealed class BehaviorManagertests
    {
        private readonly BehaviorManager _behaviorManager;
        private readonly MockRepository _mockRepository;
        private readonly Mock<IHasBehaviors> _mockHasBehaviors;
        private readonly Mock<IBehaviorCollection> _mockBehaviorCollection;

        public BehaviorManagertests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _mockHasBehaviors = _mockRepository.Create<IHasBehaviors>();
            _mockBehaviorCollection = _mockRepository.Create<IBehaviorCollection>();
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

            _mockBehaviorCollection
                .SetupSequence(x => x.GetEnumerator())
                .Returns(new List<IBehavior>()
                {
                    mockBehavior.Object,
                }.GetEnumerator())
                .Returns(new List<IBehavior>()
                {
                    mockBehavior.Object,
                }.GetEnumerator());

            _behaviorManager.Register(
                _mockHasBehaviors.Object,
                _mockBehaviorCollection.Object);

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

            _mockBehaviorCollection
                .SetupSequence(x => x.GetEnumerator())
                .Returns(new List<IBehavior>()
                {
                    mockBehavior1.Object,
                    mockBehavior2.Object,
                }.GetEnumerator())
                .Returns(new List<IBehavior>()
                {
                    mockBehavior1.Object,
                    mockBehavior2.Object,
                }.GetEnumerator());

            _behaviorManager.Register(
                _mockHasBehaviors.Object,
                _mockBehaviorCollection.Object);

            _mockRepository.VerifyAll();
            Assert.Equal(2, registeringCalls);
            Assert.Equal(2, registeredCalls);
        }
    }
}
