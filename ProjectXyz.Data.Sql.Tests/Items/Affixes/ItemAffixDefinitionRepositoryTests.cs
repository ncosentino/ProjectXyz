using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Moq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items.Affixes;
using ProjectXyz.Data.Sql.Items;
using ProjectXyz.Data.Sql.Items.Affixes;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Data.Sql.Tests.Unit.Items.Affixes
{
    [DataLayer]
    [Items]
    public class ItemAffixDefinitionRepositoryTests
    {
        #region Methods
        [Fact]
        public void GetById_IdExists_ExpectedValues()
        {
            // Setup
            var id =  Guid.NewGuid();
            var nameStringResourceId = Guid.NewGuid();
            const bool IS_PREFIX = true;
            const int MINIMUM_LEVEL = 123;
            const int MAXIMUMLEVEL = 456;

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
                .Setup(x => x.GetOrdinal("NameStringResourceId"))
                .Returns(1);
            reader
                .Setup(x => x.GetGuid(1))
                .Returns(nameStringResourceId);

            reader
                .Setup(x => x.GetOrdinal("IsPrefix"))
                .Returns(2);
            reader
                .Setup(x => x.GetBoolean(2))
                .Returns(IS_PREFIX);

            reader
                .Setup(x => x.GetOrdinal("MinimumLevel"))
                .Returns(3);
            reader
                .Setup(x => x.GetInt32(3))
                .Returns(MINIMUM_LEVEL);

            reader
                .Setup(x => x.GetOrdinal("MaximumLevel"))
                .Returns(4);
            reader
                .Setup(x => x.GetInt32(4))
                .Returns(MAXIMUMLEVEL);
                        
            var command = new Mock<IDbCommand>();
            command
                .Setup(x => x.ExecuteReader())
                .Returns(reader.Object);

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(command.Object);

            var enchantmentStore = new Mock<IItemAffixDefinition>();

            var factory = new Mock<IItemAffixDefinitionFactory>(MockBehavior.Strict);
            factory
                .Setup(x => x.Create(
                    id,
                    nameStringResourceId,
                    IS_PREFIX,
                    MINIMUM_LEVEL,
                    MAXIMUMLEVEL))
                .Returns(enchantmentStore.Object);

            var repository = ItemAffixDefinitionRepository.Create(
                database.Object,
                factory.Object);
           
            // Execute
            var result = repository.GetById(id);

            // Assert
            Assert.Equal(enchantmentStore.Object, result);

            factory.Verify(x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<bool>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()),
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

            var factory = new Mock<IItemAffixDefinitionFactory>(MockBehavior.Strict);

            var repository = ItemAffixDefinitionRepository.Create(
                database.Object,
                factory.Object);

            // Execute
            var exception = Assert.Throws<InvalidOperationException>(() => repository.GetById(id));

            // Assert
            Assert.Equal("No item affix definition with Id '" + id + "' was found.", exception.Message);

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

            var factory = new Mock<IItemAffixDefinitionFactory>();
            
            var repository = ItemAffixDefinitionRepository.Create(
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
            var nameStringResourceId = Guid.NewGuid();
            const bool IS_PREFIX = true;
            const int MINIMUM_LEVEL = 123;
            const int MAXIMUMLEVEL = 456;

            var command = new Mock<IDbCommand>();

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()))
                .Returns(command.Object);

            var itemAffixDefinition = new Mock<IItemAffixDefinition>(MockBehavior.Strict);
            itemAffixDefinition
                .Setup(x => x.Id)
                .Returns(id);
            itemAffixDefinition
                .Setup(x => x.NameStringResourceId)
                .Returns(nameStringResourceId);
            itemAffixDefinition
                .Setup(x => x.IsPrefix)
                .Returns(IS_PREFIX);
            itemAffixDefinition
                .Setup(x => x.MinimumLevel)
                .Returns(MINIMUM_LEVEL);
            itemAffixDefinition
                .Setup(x => x.MaximumLevel)
                .Returns(MAXIMUMLEVEL);

            var factory = new Mock<IItemAffixDefinitionFactory>();

            var repository = ItemAffixDefinitionRepository.Create(
                database.Object,
                factory.Object);

            // Execute
            repository.Add(itemAffixDefinition.Object);

            // Assert
            database.Verify(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()), Times.Once);

            command.Verify(x => x.ExecuteNonQuery(), Times.Once);

            itemAffixDefinition.Verify(x => x.Id, Times.Once);
            itemAffixDefinition.Verify(x => x.NameStringResourceId, Times.Once);
            itemAffixDefinition.Verify(x => x.IsPrefix, Times.Once);
            itemAffixDefinition.Verify(x => x.MinimumLevel, Times.Once);
            itemAffixDefinition.Verify(x => x.MaximumLevel, Times.Once);
        }
        #endregion
    }
}