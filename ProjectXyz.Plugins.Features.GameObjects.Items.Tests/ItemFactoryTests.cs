using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Moq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.GameObjects;
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
        private readonly Mock<IBehaviorManager> _behaviorManager;
        private readonly Mock<IMutableStatsProviderFactory> _mutableStatsProviderFactory;
        private readonly Mock<IMutableStatsProvider> _mutableStatsProvider;
        private readonly Mock<IStatManager> _statManager;

        public ItemFactoryTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            _statManagerFactory = _mockRepository.Create<IStatManagerFactory>();
            _behaviorManager = _mockRepository.Create<IBehaviorManager>();
            _mutableStatsProviderFactory = _mockRepository.Create<IMutableStatsProviderFactory>();
            _mutableStatsProvider = _mockRepository.Create<IMutableStatsProvider>();
            _statManager = _mockRepository.Create<IStatManager>();

            _itemFactory = new ItemFactory(
                _statManagerFactory.Object,
                _behaviorManager.Object,
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

            _behaviorManager
                .Setup(x => x.Register(It.IsAny<IGameObject>(), It.IsAny<IReadOnlyCollection<IBehavior>>()));

            var item = _itemFactory.Create();

            _mockRepository.VerifyAll();
        }
    }
}
