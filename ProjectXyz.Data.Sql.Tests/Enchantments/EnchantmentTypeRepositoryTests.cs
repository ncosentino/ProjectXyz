using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Moq;
using ProjectXyz.Data.Sql.Enchantments;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Data.Sql.Tests.Enchantments
{
    [DataLayer]
    [Enchantments]
    public class EnchantmentTypeRepositoryTests
    {
        #region Methods
        [Fact]
        public void GetStoreRepositoryClassName_IdExists_ExpectedValues()
        {
            // Setup
            Guid enchantmentDefinitionId = Guid.NewGuid();
            const string CLASS_NAME = "the class name";

            var reader = new Mock<IDataReader>(MockBehavior.Strict);
            reader
                .Setup(x => x.Read())
                .Returns(true);
            reader
                .Setup(x => x.GetString(0))
                .Returns(CLASS_NAME);
            reader
                .Setup(x => x.Dispose());

            var command = new Mock<IDbCommand>(MockBehavior.Strict);
            command
                .Setup(x => x.ExecuteReader())
                .Returns(reader.Object);
            command
                .Setup(x => x.Dispose());

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(command.Object);

            var repository = EnchantmentTypeRepository.Create(database.Object);

            // Execute
            var result = repository.GetStoreRepositoryClassName(enchantmentDefinitionId);

            // Assert
            Assert.Equal(CLASS_NAME, result);

            reader.Verify(x => x.Read(), Times.Once());
            reader.Verify(x => x.GetString(0), Times.Once);
            reader.Verify(x => x.Dispose(), Times.Once);

            database.Verify(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);

            command.Verify(x => x.ExecuteReader(), Times.Once);
            command.Verify(x => x.Dispose(), Times.Once);
        }

        [Fact]
        public void GetStoreRepositoryClassName_IdDoesNotExist_ThrowsInvalidOperationException()
        {
            // Setup
            Guid enchantmentDefinitionId = Guid.NewGuid();

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

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(command.Object);

            var repository = EnchantmentTypeRepository.Create(database.Object);

            Assert.ThrowsDelegateWithReturn method = () => repository.GetStoreRepositoryClassName(enchantmentDefinitionId);

            // Execute
            var result = Assert.Throws<InvalidOperationException>(method);

            // Assert
            Assert.True(
                Regex.IsMatch(result.Message, "No enchantment definition with Id '.+' was found\\."),
                "Not expecting message: " + result.Message);

            reader.Verify(x => x.Read(), Times.Once());
            reader.Verify(x => x.Dispose(), Times.Once);

            database.Verify(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);

            command.Verify(x => x.ExecuteReader(), Times.Once);
            command.Verify(x => x.Dispose(), Times.Once);
        }

        [Fact]
        public void GetDefinitionRepositoryClassName_IdExists_ExpectedValues()
        {
            // Setup
            Guid enchantmentDefinitionId = Guid.NewGuid();
            const string CLASS_NAME = "the class name";

            var reader = new Mock<IDataReader>(MockBehavior.Strict);
            reader
                .Setup(x => x.Read())
                .Returns(true);
            reader
                .Setup(x => x.GetString(0))
                .Returns(CLASS_NAME);
            reader
                .Setup(x => x.Dispose());

            var command = new Mock<IDbCommand>(MockBehavior.Strict);
            command
                .Setup(x => x.ExecuteReader())
                .Returns(reader.Object);
            command
                .Setup(x => x.Dispose());

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(command.Object);

            var repository = EnchantmentTypeRepository.Create(database.Object);

            // Execute
            var result = repository.GetDefinitionRepositoryClassName(enchantmentDefinitionId);

            // Assert
            Assert.Equal(CLASS_NAME, result);

            reader.Verify(x => x.Read(), Times.Once());
            reader.Verify(x => x.GetString(0), Times.Once);
            reader.Verify(x => x.Dispose(), Times.Once);

            database.Verify(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);

            command.Verify(x => x.ExecuteReader(), Times.Once);
            command.Verify(x => x.Dispose(), Times.Once);
        }

        [Fact]
        public void GetDefinitionRepositoryClassName_IdDoesNotExist_ThrowsInvalidOperationException()
        {
            // Setup
            Guid enchantmentDefinitionId = Guid.NewGuid();

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

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(command.Object);

            var repository = EnchantmentTypeRepository.Create(database.Object);

            Assert.ThrowsDelegateWithReturn method = () => repository.GetDefinitionRepositoryClassName(enchantmentDefinitionId);

            // Execute
            var result = Assert.Throws<InvalidOperationException>(method);

            // Assert
            Assert.True(
                Regex.IsMatch(result.Message, "No enchantment definition with Id '.+' was found\\."),
                "Not expecting message: " + result.Message);

            reader.Verify(x => x.Read(), Times.Once());
            reader.Verify(x => x.Dispose(), Times.Once);

            database.Verify(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);

            command.Verify(x => x.ExecuteReader(), Times.Once);
            command.Verify(x => x.Dispose(), Times.Once);
        }
        #endregion
    }
}