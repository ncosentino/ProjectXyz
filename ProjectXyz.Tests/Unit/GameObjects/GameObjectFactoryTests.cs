using System.Collections.Generic;

using Moq;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Behaviors.Default;

using Xunit;

namespace ProjectXyz.Tests.Unit.GameObjects
{
    public sealed class GameObjectFactoryTests
    {
        private readonly GameObjectFactory _gameObjectFactory;
        private readonly MockRepository _mockRepository;

        public GameObjectFactoryTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _gameObjectFactory = new GameObjectFactory();
        }

        [Fact]
        private void Create_SingleRegisterableBehavior_RegistrationInCorrectOrder()
        {
            int registeredCalls = 0;
            int registeringCalls = 0;

            var mockBehavior = _mockRepository.Create<IRegisterableBehavior>();
            mockBehavior
                .Setup(x => x.RegisteringToOwner(It.IsNotNull<IGameObject>()))
                .Callback<IGameObject>(_ =>
                {
                    Assert.Equal(0, registeredCalls);
                    Assert.Equal(0, registeringCalls);
                    registeringCalls++;
                });
            mockBehavior
                .Setup(x => x.RegisteredToOwner(It.IsNotNull<IGameObject>()))
                .Callback<IGameObject>(_ =>
                {
                    Assert.Equal(0, registeredCalls);
                    Assert.Equal(1, registeringCalls);
                    registeredCalls++;
                });

            var behaviors = new List<IBehavior>()
            {
                mockBehavior.Object,
            };

            _gameObjectFactory.Create(behaviors);

            _mockRepository.VerifyAll();
            Assert.Equal(1, registeringCalls);
            Assert.Equal(1, registeredCalls);
        }

        [Fact]
        private void Create_MultipleRegisterableBehaviors_RegistrationInCorrectOrder()
        {
            int registeredCalls = 0;
            int registeringCalls = 0;

            var mockBehavior1 = _mockRepository.Create<IRegisterableBehavior>();
            var mockBehavior2 = _mockRepository.Create<IRegisterableBehavior>();

            mockBehavior1
                .Setup(x => x.RegisteringToOwner(It.IsNotNull<IGameObject>()))
                .Callback<IGameObject>(_ =>
                {
                    Assert.Equal(0, registeredCalls);
                    Assert.Equal(0, registeringCalls);
                    registeringCalls++;
                });
            mockBehavior1
                .Setup(x => x.RegisteredToOwner(It.IsNotNull<IGameObject>()))
                .Callback<IGameObject>(_ =>
                {
                    Assert.Equal(0, registeredCalls);
                    Assert.Equal(2, registeringCalls);
                    registeredCalls++;
                });

            mockBehavior2
                .Setup(x => x.RegisteringToOwner(It.IsNotNull<IGameObject>()))
                .Callback<IGameObject>(_ =>
                {
                    Assert.Equal(0, registeredCalls);
                    Assert.Equal(1, registeringCalls);
                    registeringCalls++;
                });
            mockBehavior2
                .Setup(x => x.RegisteredToOwner(It.IsNotNull<IGameObject>()))
                .Callback<IGameObject>(_ =>
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

            _gameObjectFactory.Create(behaviors);

            _mockRepository.VerifyAll();
            Assert.Equal(2, registeringCalls);
            Assert.Equal(2, registeredCalls);
        }
    }
}
