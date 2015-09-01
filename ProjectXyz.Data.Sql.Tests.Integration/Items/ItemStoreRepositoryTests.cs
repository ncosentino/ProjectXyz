using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Data.Core.Items;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Sql.Items;
using ProjectXyz.Tests.Integration;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Data.Sql.Tests.Integration.Items
{
    [DataLayer]
    [Enchantments]
    public class ItemStoreRepositoryTests : DatabaseTest
    {
        #region Methods
        [Fact]
        public void GetAll_NoItems_NoResults()
        {
            // Setup
            var factory = new Mock<IItemStoreFactory>(MockBehavior.Strict);

            var repository = ItemStoreRepository.Create(
                Database,
                factory.Object);

            // Execute
            var result = repository.GetAll();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetAll_MultipleItems_MultipleResults()
        {
            // Setup
            var genericCollectionResult = new Mock<IItemStore>(MockBehavior.Strict);

            var factory = new Mock<IItemStoreFactory>(MockBehavior.Strict);
            factory
                .Setup(x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>()))
                .Returns(genericCollectionResult.Object);

            var repository = ItemStoreRepository.Create(
                Database,
                factory.Object);

            const int NUMBER_OF_RESULTS = 5;
            for (int i = 0; i < NUMBER_OF_RESULTS; i++)
            {
                CreateItemStore(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid());
            }

            // Execute
            var result = repository.GetAll();

            // Assert
            Assert.Equal(NUMBER_OF_RESULTS, result.Count());

            factory.Verify(
                x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>()),
                Times.Exactly(NUMBER_OF_RESULTS));
        }

        [Fact]
        public void GetById_NoItem_ThrowsException()
        {
            // Setup
            var factory = new Mock<IItemStoreFactory>(MockBehavior.Strict);

            var repository = ItemStoreRepository.Create(
                Database,
                factory.Object);

            // Execute
            Assert.ThrowsDelegateWithReturn method = () => repository.GetById(Guid.NewGuid());

            // Assert
            var result = Assert.Throws<InvalidOperationException>(method);
        }

        [Fact]
        public void GetById_ItemExists_ExpectedResult()
        {
            // Setup
            var expectedResult = new Mock<IItemStore>(MockBehavior.Strict);

            var id = Guid.NewGuid();
            var itemDefinitionId = Guid.NewGuid();
            var inventoryGraphicResourceId = Guid.NewGuid();
            var itemTypeId = Guid.NewGuid();
            var magicTypeId = Guid.NewGuid();
            var materialTypeId = Guid.NewGuid();
            var nameStringResourceId = Guid.NewGuid();
            var socketTypeId = Guid.NewGuid();

            var factory = new Mock<IItemStoreFactory>(MockBehavior.Strict);
            factory
                .Setup(x => x.Create(
                    id,
                    itemDefinitionId,
                    nameStringResourceId,
                    inventoryGraphicResourceId,
                    magicTypeId,
                    itemTypeId,
                    materialTypeId,
                    socketTypeId))
                .Returns(expectedResult.Object);

            var repository = ItemStoreRepository.Create(
                Database,
                factory.Object);

            CreateItemStore(
                id,
                itemDefinitionId,
                nameStringResourceId,
                inventoryGraphicResourceId,
                magicTypeId,
                itemTypeId,
                materialTypeId,
                socketTypeId);

            // Execute
            var result = repository.GetById(id);

            // Assert
            Assert.Equal(expectedResult.Object, result);

            factory.Verify(
                x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>()),
                Times.Once);
        }

        [Fact]
        public void RemoveById_ItemExists_RemovedFromDatabase()
        {
            // Setup
            var factory = new Mock<IItemStoreFactory>(MockBehavior.Strict);
            
            var repository = ItemStoreRepository.Create(
                Database,
                factory.Object);

            var id = Guid.NewGuid();
            CreateItemStore(
                id,
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid());

            // Execute
            repository.RemoveById(id);

            // Assert
            using (var command = Database.CreateCommand("SELECT * FROM Items"))
            {
                using (var reader = command.ExecuteReader())
                {
                    Assert.False(reader.Read(), "Not expecting any rows.");
                }
            }
        }

        [Fact]
        public void Add_ValidItem_AddedToDatabase()
        {
            // Setup
            var id = Guid.NewGuid();
            var itemDefinitionId = Guid.NewGuid();
            var inventoryGraphicResourceId = Guid.NewGuid();
            var itemTypeId = Guid.NewGuid();
            var magicTypeId = Guid.NewGuid();
            var materialTypeId = Guid.NewGuid();
            var nameStringResourceId = Guid.NewGuid();
            var socketTypeId = Guid.NewGuid();

            var factory = ItemStoreFactory.Create();

            var repository = ItemStoreRepository.Create(
                Database,
                factory);

            // Execute
            var result = repository.Add(
                id,
                itemDefinitionId,
                nameStringResourceId,
                inventoryGraphicResourceId,
                magicTypeId,
                itemTypeId,
                materialTypeId,
                socketTypeId);

            // Assert
            using (var command = Database.CreateCommand("SELECT * FROM Items"))
            {
                using (var reader = command.ExecuteReader())
                {
                    Assert.True(reader.Read(), "Expecting to read one row.");
                    Assert.False(reader.Read(), "Not expecting additional rows.");
                }
            }

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(nameStringResourceId, result.NameStringResourceId);
            Assert.Equal(inventoryGraphicResourceId, result.InventoryGraphicResourceId);
            Assert.Equal(magicTypeId, result.MagicTypeId);
            Assert.Equal(itemTypeId, result.ItemTypeId);
            Assert.Equal(materialTypeId, result.MaterialTypeId);
            Assert.Equal(socketTypeId, result.SocketTypeId);
        }

        private void CreateItemStore(
            Guid id,
            Guid itemDefinitionId,
            Guid nameStringResourceId,
            Guid inventoryGraphicResourceId,
            Guid magicTypeId,
            Guid itemTypeId,
            Guid materialTypeId,
            Guid socketTypeId)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "ItemDefinitionId", itemDefinitionId },
                { "InventoryGraphicResourceId", inventoryGraphicResourceId },
                { "ItemTypeId", itemTypeId },
                { "MagicTypeId", magicTypeId },
                { "MaterialTypeId", materialTypeId },
                { "NameStringResourceId", nameStringResourceId },
                { "SocketTypeId", socketTypeId },
            };

            using (var command = Database.CreateCommand(
                @"
                INSERT INTO
                    Items
                (
                    Id,
                    ItemDefinitionId,
                    InventoryGraphicResourceId,
                    ItemTypeId,
                    MagicTypeId,
                    MaterialTypeId,
                    NameStringResourceId,
                    SocketTypeId
                )
                VALUES
                (
                    @Id,
                    @ItemDefinitionId,
                    @InventoryGraphicResourceId,
                    @ItemTypeId,
                    @MagicTypeId,
                    @MaterialTypeId,
                    @NameStringResourceId,
                    @SocketTypeId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }
        }
        #endregion
    }
}