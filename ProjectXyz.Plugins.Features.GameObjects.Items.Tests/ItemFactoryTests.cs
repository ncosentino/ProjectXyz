using System.Collections.Generic;

using Moq;

using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;

using Xunit;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Tests
{
    public sealed class ItemFactoryTests
    {
        private readonly MockRepository _mockRepository;
        private readonly IItemFactory _itemFactory;
        private readonly Mock<IStatManagerFactory> _statManagerFactory;
        private readonly Mock<IGameObjectFactory> _gameObjectFactory;
        private readonly Mock<IMutableStatsProviderFactory> _mutableStatsProviderFactory;
        private readonly Mock<IMutableStatsProvider> _mutableStatsProvider;
        private readonly Mock<IStatManager> _statManager;
        private readonly Mock<IGameObject> _expectedItem;

        public ItemFactoryTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            _statManagerFactory = _mockRepository.Create<IStatManagerFactory>();
            _gameObjectFactory = _mockRepository.Create<IGameObjectFactory>();
            _mutableStatsProviderFactory = _mockRepository.Create<IMutableStatsProviderFactory>();
            _mutableStatsProvider = _mockRepository.Create<IMutableStatsProvider>();
            _statManager = _mockRepository.Create<IStatManager>();
            _expectedItem = _mockRepository.Create<IGameObject>();

            _itemFactory = new ItemFactory(
                _statManagerFactory.Object,
                _gameObjectFactory.Object,
                _mutableStatsProviderFactory.Object);
        }

        [Fact]
        private void Create_NoParams_ExpectedItem()
        {
            _mutableStatsProviderFactory
                .Setup(x => x.Create())
                .Returns(_mutableStatsProvider.Object);

            _statManagerFactory
                .Setup(x => x.Create(_mutableStatsProvider.Object))
                .Returns(_statManager.Object);

            _gameObjectFactory
                .Setup(x => x.Create(It.IsAny<IEnumerable<IBehavior>>()))
                .Returns(_expectedItem.Object);

            var actualItem = _itemFactory.Create();

            Assert.Equal(_expectedItem.Object, actualItem);
            _mockRepository.VerifyAll();
        }
    }
}
