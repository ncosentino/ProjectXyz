using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

using Moq;

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

        private const string COLUMN_NAME_NAME = "Name";
        private const int COLUMN_INDEX_NAME = COLUMN_INDEX_ID + 1;
        #endregion

        #region Methods
        [Fact]
        public void EnchantmentTriggerRepository_GetById_ExpectedValues()
        {
            var reader = new Mock<IDataReader>();
            reader
                .Setup(x => x.Read())
                .Returns(true);
            
            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_ID))
                .Returns(COLUMN_INDEX_ID);
            reader
                .Setup(x => x.GetGuid(COLUMN_INDEX_ID))
                .Returns(new Guid("d5cfc545-2d99-472a-81ce-9ac62d583a9e"));

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_NAME))
                .Returns(COLUMN_INDEX_NAME);
            reader
                .Setup(x => x.GetString(COLUMN_INDEX_NAME))
                .Returns("Some Name");
            
            var command = new Mock<IDbCommand>();
            command
                .Setup(x => x.ExecuteReader())
                .Returns(reader.Object);

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(command.Object);

            var enchantmentTrigger = new Mock<IEnchantmentTrigger>();

            var factory = new Mock<IEnchantmentTriggerFactory>();
            factory
                .Setup(x => x.CreateEnchantmentTrigger(new Guid("d5cfc545-2d99-472a-81ce-9ac62d583a9e"), "Some Name"))
                .Returns(enchantmentTrigger.Object);

            var repository = EnchantmentTriggerRepository.Create(
                database.Object, 
                factory.Object);
            var guid = new Guid("9a760e46-4a52-416f-8c54-e39b0583610f");
           
            var result = repository.GetById(guid);

            Assert.Equal(enchantmentTrigger.Object, result);
            factory.Verify(x => x.CreateEnchantmentTrigger(It.IsAny<Guid>(), It.IsAny<string>()), Times.Once);
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
            Assert.Equal("Could not spawn enchantment trigger with Id = '" + guid + "'.", exception.Message);
        }
        #endregion
    }
}