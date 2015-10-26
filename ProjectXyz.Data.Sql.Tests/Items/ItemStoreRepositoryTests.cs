using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Moq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Sql.Items;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Data.Sql.Tests.Unit.Items
{
    [DataLayer]
    [Items]
    public class ItemStoreRepositoryTests
    {
        #region Methods
        [Fact]
        public void GetById_IdExists_ExpectedValues()
        {
            // Setup
            var id =  Guid.NewGuid();
            var itemDefinitionId = Guid.NewGuid();
            var nameStringResourceId = Guid.NewGuid();
            var inventoryGraphicResourceId = Guid.NewGuid();
            var magicTypeId = Guid.NewGuid();
            var itemTypeId = Guid.NewGuid();
            var materialTypeId = Guid.NewGuid();
            var socketTypeId = Guid.NewGuid();

            var reader = new Mock<IDataReader>();
            reader
                .Setup(x => x.Read())
                .Returns(true);
            
            reader
                .Setup(x => x.GetOrdinal("Id"))
                .Returns(0);
            reader
                .Setup(x => x.GetGuid(0))
                .Returns(id);

            reader
                .Setup(x => x.GetOrdinal("ItemNamePartId"))
                .Returns(1);
            reader
                .Setup(x => x.GetGuid(1))
                .Returns(nameStringResourceId);

            reader
                .Setup(x => x.GetOrdinal("InventoryGraphicResourceId"))
                .Returns(2);
            reader
                .Setup(x => x.GetGuid(2))
                .Returns(inventoryGraphicResourceId);

            reader
                .Setup(x => x.GetOrdinal("MagicTypeId"))
                .Returns(3);
            reader
                .Setup(x => x.GetGuid(3))
                .Returns(magicTypeId);

            reader
                .Setup(x => x.GetOrdinal("ItemTypeId"))
                .Returns(4);
            reader
                .Setup(x => x.GetGuid(4))
                .Returns(itemTypeId);

            reader
                .Setup(x => x.GetOrdinal("MaterialTypeId"))
                .Returns(5);
            reader
                .Setup(x => x.GetGuid(5))
                .Returns(materialTypeId);
            
            reader
                .Setup(x => x.GetOrdinal("SocketTypeId"))
                .Returns(6);
            reader
                .Setup(x => x.GetGuid(6))
                .Returns(socketTypeId);
            
            reader
               .Setup(x => x.GetOrdinal("ItemDefinitionId"))
               .Returns(7);
            reader
                .Setup(x => x.GetGuid(7))
                .Returns(itemDefinitionId);
                        
            var command = new Mock<IDbCommand>();
            command
                .Setup(x => x.ExecuteReader())
                .Returns(reader.Object);

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(command.Object);

            var enchantmentStore = new Mock<IItemStore>();

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
                .Returns(enchantmentStore.Object);

            var repository = ItemStoreRepository.Create(
                database.Object,
                factory.Object);
           
            // Execute
            var result = repository.GetById(id);

            // Assert
            Assert.Equal(enchantmentStore.Object, result);

            factory.Verify(x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>()),
                    Times.Once);

            database.Verify(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);

            command.Verify(x => x.ExecuteReader(), Times.Once);
        }

        [Fact]
        public void GetById_NotAvailable_Throws()
        {
            // Setup
            var id = Guid.NewGuid();

            var reader = new Mock<IDataReader>(MockBehavior.Strict);
            reader
                .Setup(x => x.Read())
                .Returns(false);
            reader
                .Setup(x => x.Dispose());

            var command = new Mock<IDbCommand>(MockBehavior.Strict);
            command
                .Setup(x => x.ExecuteReader())
                .Returns(reader.Object);
            command
                .Setup(x => x.Dispose());

            var database = new Mock<IDatabase>(MockBehavior.Strict);
            database
                .Setup(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(command.Object);

            var factory = new Mock<IItemStoreFactory>(MockBehavior.Strict);

            var repository = ItemStoreRepository.Create(
                database.Object,
                factory.Object);

            // Execute
            var exception = Assert.Throws<InvalidOperationException>(() => repository.GetById(id));

            // Assert
            Assert.Equal("No item with Id '" + id + "' was found.", exception.Message);

            reader.Verify(x => x.Read(), Times.Once);
            reader.Verify(x => x.Dispose(), Times.Once);

            database.Verify(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);

            command.Verify(x => x.ExecuteReader(), Times.Once);
            command.Verify(x => x.Dispose(), Times.Once);
        }

        [Fact]
        public void RemoveById_IdExists_Success()
        {
            // Setup
            Guid enchantmentStoreId = Guid.NewGuid();
         
            var command = new Mock<IDbCommand>();

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(command.Object);

            var factory = new Mock<IItemStoreFactory>();
            
            var repository = ItemStoreRepository.Create(
                database.Object,
                factory.Object);

            // Execute
            repository.RemoveById(enchantmentStoreId);
            
            // Assert
            database.Verify(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);

            command.Verify(x => x.ExecuteNonQuery(), Times.Once);
        }

        [Fact]
        public void Add_ValidObject_Success()
        {
            // Setup
            var id = Guid.NewGuid();
            var itemDefinitionId = Guid.NewGuid();
            var nameStringResourceId = Guid.NewGuid();
            var inventoryGraphicResourceId = Guid.NewGuid();
            var magicTypeId = Guid.NewGuid();
            var itemTypeId = Guid.NewGuid();
            var materialTypeId = Guid.NewGuid();
            var socketTypeId = Guid.NewGuid();

            var command = new Mock<IDbCommand>();

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()))
                .Returns(command.Object);

            var objectToAdd = new Mock<IItemStore>(MockBehavior.Strict);

            var factory = new Mock<IItemStoreFactory>();
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
                .Returns(objectToAdd.Object);
          
            var repository = ItemStoreRepository.Create(
                database.Object,
                factory.Object);
          
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
            Assert.Equal(objectToAdd.Object, result);

            database.Verify(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()), Times.Once);

            command.Verify(x => x.ExecuteNonQuery(), Times.Once);

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
        #endregion
    }
}