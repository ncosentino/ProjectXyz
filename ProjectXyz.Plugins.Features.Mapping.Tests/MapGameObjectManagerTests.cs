using System;

using Moq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.Mapping.Default;

using Xunit;

namespace ProjectXyz.Plugins.Features.Mapping.Tests
{
    public sealed class MapGameObjectManagerTests
    {
        private readonly MockRepository _mockRepository;
        private readonly IMapGameObjectManager _mapGameObjectManager;

        public MapGameObjectManagerTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _mapGameObjectManager = new MapGameObjectManager();
        }

        [Fact]
        private void MarkForAddition_SingleItemNoSync_EmptyGameObjectsNoSyncEvent()
        {
            var gameObject = _mockRepository.Create<IGameObject>();

            var synchronizedCount = 0;
            _mapGameObjectManager.Synchronized += (_, e) => synchronizedCount++;

            _mapGameObjectManager.MarkForAddition(gameObject.Object);

            Assert.Empty(_mapGameObjectManager.GameObjects);
            Assert.Equal(0, synchronizedCount);
            _mockRepository.VerifyAll();
        }

        [Fact]
        private void MarkForRemoval_SingleItemNoSync_EmptyGameObjectsNoSyncEvent()
        {
            var gameObject = _mockRepository.Create<IGameObject>();

            var synchronizedCount = 0;
            _mapGameObjectManager.Synchronized += (_, e) => synchronizedCount++;

            _mapGameObjectManager.MarkForRemoval(gameObject.Object);

            Assert.Empty(_mapGameObjectManager.GameObjects);
            Assert.Equal(0, synchronizedCount);
            _mockRepository.VerifyAll();
        }

        [Fact]
        private void Synchronize_SingleItemAdded_SingleGameObjectExpectedSyncEvent()
        {
            var gameObject = _mockRepository.Create<IGameObject>();

            var synchronizedCount = 0;
            _mapGameObjectManager.Synchronized += (_, e) =>
            {
                Assert.Single(e.Added, gameObject.Object);
                Assert.Empty(e.Removed);
                synchronizedCount++;
            };

            _mapGameObjectManager.MarkForAddition(gameObject.Object);
            _mapGameObjectManager.Synchronize();

            Assert.Single(_mapGameObjectManager.GameObjects, gameObject.Object);
            Assert.Equal(1, synchronizedCount);
            _mockRepository.VerifyAll();
        }

        [Fact]
        private void Synchronize_DuplicateItemAdded_SingleGameObjectNoSyncEvent()
        {
            var gameObject = _mockRepository.Create<IGameObject>();

            _mapGameObjectManager.MarkForAddition(gameObject.Object);
            _mapGameObjectManager.Synchronize();

            var synchronizedCount = 0;
            _mapGameObjectManager.Synchronized += (_, e) => synchronizedCount++;

            // add the same one
            _mapGameObjectManager.MarkForAddition(gameObject.Object);
            _mapGameObjectManager.Synchronize();

            Assert.Single(_mapGameObjectManager.GameObjects, gameObject.Object);
            Assert.Equal(0, synchronizedCount);
            _mockRepository.VerifyAll();
        }

        [Fact]
        private void Synchronize_NonExistentItemRemoved_EmptyGameObjectsNoSyncEvent()
        {
            var gameObject = _mockRepository.Create<IGameObject>();

            var synchronizedCount = 0;
            _mapGameObjectManager.Synchronized += (_, e) => synchronizedCount++;

            _mapGameObjectManager.MarkForRemoval(gameObject.Object);
            _mapGameObjectManager.Synchronize();

            Assert.Empty(_mapGameObjectManager.GameObjects);
            Assert.Equal(0, synchronizedCount);
            _mockRepository.VerifyAll();
        }

        [Fact]
        private void Synchronize_ItemAddedAndRemovedBeforeSync_EmptyGameObjectsNoSyncEvent()
        {
            var gameObject = _mockRepository.Create<IGameObject>();

            var synchronizedCount = 0;
            _mapGameObjectManager.Synchronized += (_, e) => synchronizedCount++;

            _mapGameObjectManager.MarkForAddition(gameObject.Object);
            _mapGameObjectManager.MarkForRemoval(gameObject.Object);
            _mapGameObjectManager.Synchronize();

            Assert.Empty(_mapGameObjectManager.GameObjects);
            Assert.Equal(0, synchronizedCount);
            _mockRepository.VerifyAll();
        }

        [Fact]
        private void Synchronize_ItemAddedAndRemovedAfterSync_EmptyGameObjectsNoSyncEvent()
        {
            var gameObject = _mockRepository.Create<IGameObject>();

            _mapGameObjectManager.MarkForAddition(gameObject.Object);
            _mapGameObjectManager.Synchronize();

            var synchronizedCount = 0;
            _mapGameObjectManager.Synchronized += (_, e) => synchronizedCount++;

            _mapGameObjectManager.MarkForRemoval(gameObject.Object);
            _mapGameObjectManager.Synchronize();

            Assert.Empty(_mapGameObjectManager.GameObjects);
            Assert.Equal(1, synchronizedCount);
            _mockRepository.VerifyAll();
        }
    }
}
