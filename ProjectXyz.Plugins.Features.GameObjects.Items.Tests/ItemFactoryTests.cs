﻿using System.Collections.Generic;
using System.Linq;

using Moq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Default;

using Xunit;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Tests
{
    public sealed class ItemFactoryTests
    {
        private readonly MockRepository _mockRepository;
        private readonly IItemFactory _itemFactory;
        private readonly Mock<IHasStatsBehaviorFactory> _hasMutableStatsBehaviorFactory;
        private readonly Mock<IGameObjectFactory> _gameObjectFactory;
        private readonly Mock<IGameObject> _expectedItem;
        private readonly Mock<IHasStatsBehavior> _hasStatsBehavior;

        public ItemFactoryTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            _hasMutableStatsBehaviorFactory = _mockRepository.Create<IHasStatsBehaviorFactory>();
            _gameObjectFactory = _mockRepository.Create<IGameObjectFactory>();
            _expectedItem = _mockRepository.Create<IGameObject>();
            _hasStatsBehavior = _mockRepository.Create<IHasStatsBehavior>();

            _itemFactory = new ItemFactory(
                _gameObjectFactory.Object,
                _hasMutableStatsBehaviorFactory.Object);
        }

        [Fact]
        private void Create_NoParams_ExpectedDefaultBehaviors()
        {
            _hasMutableStatsBehaviorFactory
                .Setup(x => x.Create())
                .Returns(_hasStatsBehavior.Object);

            _gameObjectFactory
                .Setup(x => x.Create(It.Is<IEnumerable<IBehavior>>(b =>
                    b.Count() == 2 &&
                    b.TakeTypes<IIdentifierBehavior>().Count() == 1 &&
                    b.TakeTypes<IHasStatsBehavior>().Single() == _hasStatsBehavior.Object)))
                .Returns(_expectedItem.Object);

            var actualItem = _itemFactory.Create();

            Assert.Equal(_expectedItem.Object, actualItem);
            _mockRepository.VerifyAll();
        }
    }
}
