using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xunit;

using Moq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Tests.Xunit.Categories;
using ProjectXyz.Data.Sql.Enchantments;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Sql.Tests.Enchantments
{
    [DataLayer]
    [Enchantments]
    public class EnchantmentTriggerRepositoryTests
    {
        #region Constants
        private const string COLUMN_NAME_ID = "Id";
        private const int COLUMN_INDEX_ID = 0;

        private const string COLUMN_NAME_NAME = "NameStringResourceId";
        private const int COLUMN_INDEX_NAME = COLUMN_INDEX_ID + 1;
        #endregion

        #region Methods
        [Fact]
        public void EnchantmentTriggerRepository_GetById_ExpectedValues()
        {
            // Setup
            var enchantmentTriggerId = Guid.NewGuid();
            var nameStringResourceId = Guid.NewGuid();

            var reader = new Mock<IDataReader>(MockBehavior.Strict);
            reader
                .Setup(x => x.Read())
                .Returns(true);
            reader
                .Setup(x => x.Dispose());
            
            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_ID))
                .Returns(COLUMN_INDEX_ID);
            reader
                .Setup(x => x.GetGuid(COLUMN_INDEX_ID))
                .Returns(enchantmentTriggerId);

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_NAME))
                .Returns(COLUMN_INDEX_NAME);
            reader
                .Setup(x => x.GetGuid(COLUMN_INDEX_NAME))
                .Returns(nameStringResourceId);

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

            var enchantmentTrigger = new Mock<IEnchantmentTrigger>(MockBehavior.Strict);

            var factory = new Mock<IEnchantmentTriggerFactory>(MockBehavior.Strict);
            factory
                .Setup(x => x.Create(enchantmentTriggerId, nameStringResourceId))
                .Returns(enchantmentTrigger.Object);

            var repository = EnchantmentTriggerRepository.Create(
                database.Object, 
                factory.Object);

            // Execute
            var result = repository.GetById(enchantmentTriggerId);

            // Assert
            Assert.Equal(enchantmentTrigger.Object, result);
            factory.Verify(x => x.Create(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void EnchantmentTriggerRepository_GetByIdNotAvailable_Throws()
        {
            var reader = new Mock<IDataReader>();
            reader
                .Setup(x => x.Read())
                .Returns(false);

            var command = new Mock<IDbCommand>();
            command
                .Setup(x => x.ExecuteReader())
                .Returns(reader.Object);

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(command.Object);

            var factory = new Mock<IEnchantmentTriggerFactory>();

            var repository = EnchantmentTriggerRepository.Create(
                database.Object,
                factory.Object);
            var guid = new Guid("9a760e46-4a52-416f-8c54-e39b0583610f");

            var exception = Assert.Throws<InvalidOperationException>(() => repository.GetById(guid));
            Assert.Equal("No enchantment trigger with Id '" + guid + "' was found.", exception.Message);

            factory.Verify(x => x.Create(
                It.IsAny<Guid>(),
                It.IsAny<Guid>()),
                Times.Never);

            database.Verify(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);

            command.Verify(x => x.ExecuteReader(), Times.Once);
        }
        #endregion
    }
}