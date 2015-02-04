using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xunit;

using Moq;

using ProjectXyz.Tests.Xunit.Categories;
using ProjectXyz.Data.Sql.Enchantments;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Sql.Tests.Enchantments
{
    [DataLayer]
    [Enchantments]
    public class EnchantmentStoreRepositoryTests
    {
        #region Constants
        private const string COLUMN_NAME_ID = "Id";
        private const int COLUMN_INDEX_ID = 0;

        private const string COLUMN_NAME_STAT_ID = "StatId";
        private const int COLUMN_INDEX_STAT_ID = COLUMN_INDEX_ID + 1;

        private const string COLUMN_NAME_CALCULATION_ID = "CalculationId";
        private const int COLUMN_INDEX_CALCULATION_ID = COLUMN_INDEX_STAT_ID + 1;

        private const string COLUMN_NAME_TRIGGER_ID = "TriggerId";
        private const int COLUMN_INDEX_TRIGGER_ID = COLUMN_INDEX_CALCULATION_ID + 1;

        private const string COLUMN_NAME_STATUS_TYPE_ID = "StatusTypeId";
        private const int COLUMN_INDEX_STATUS_TYPE_ID = COLUMN_INDEX_TRIGGER_ID + 1;

        private const string COLUMN_NAME_VALUE = "Value";
        private const int COLUMN_INDEX_VALUE = COLUMN_INDEX_STATUS_TYPE_ID + 1;

        private const string COLUMN_NAME_REMAINING_DURATION = "RemainingDuration";
        private const int COLUMN_INDEX_REMAINING_DURATION = COLUMN_INDEX_VALUE + 1;
        #endregion

        #region Methods
        [Fact]
        public void EnchantmentStoreRepository_GetById_ExpectedValues()
        {
            Guid STORE_ID = new Guid();
            Guid STAT_ID = new Guid();
            Guid CALCULATION_ID = new Guid();
            Guid TRIGGER_ID = new Guid();
            Guid STATUS_TYPE_ID = new Guid();
            
            var reader = new Mock<IDataReader>();
            reader
                .Setup(x => x.Read())
                .Returns(true);
            
            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_ID))
                .Returns(COLUMN_INDEX_ID);
            reader
                .Setup(x => x.GetGuid(COLUMN_INDEX_ID))
                .Returns(STORE_ID);

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_STAT_ID))
                .Returns(COLUMN_INDEX_STAT_ID);
            reader
                .Setup(x => x.GetGuid(COLUMN_INDEX_STAT_ID))
                .Returns(STAT_ID);

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_CALCULATION_ID))
                .Returns(COLUMN_INDEX_CALCULATION_ID);
            reader
                .Setup(x => x.GetGuid(COLUMN_INDEX_CALCULATION_ID))
                .Returns(CALCULATION_ID);

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_TRIGGER_ID))
                .Returns(COLUMN_INDEX_TRIGGER_ID);
            reader
                .Setup(x => x.GetGuid(COLUMN_INDEX_TRIGGER_ID))
                .Returns(TRIGGER_ID);

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_STATUS_TYPE_ID))
                .Returns(COLUMN_INDEX_STATUS_TYPE_ID);
            reader
                .Setup(x => x.GetGuid(COLUMN_INDEX_STATUS_TYPE_ID))
                .Returns(STATUS_TYPE_ID);

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_VALUE))
                .Returns(COLUMN_INDEX_VALUE);
            reader
                .Setup(x => x.GetDouble(COLUMN_INDEX_VALUE))
                .Returns(0);
            
            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_REMAINING_DURATION))
                .Returns(COLUMN_INDEX_REMAINING_DURATION);
            reader
                .Setup(x => x.GetDouble(COLUMN_INDEX_REMAINING_DURATION))
                .Returns(1000);
                        
            var command = new Mock<IDbCommand>();
            command
                .Setup(x => x.ExecuteReader())
                .Returns(reader.Object);

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(command.Object);

            var enchantmentStore = new Mock<IEnchantmentStore>();

            var factory = new Mock<IEnchantmentStoreFactory>();
            factory
                .Setup(x => x.CreateEnchantmentStore(
                    STORE_ID, 
                    STAT_ID,
                    CALCULATION_ID,
                    TRIGGER_ID,
                    STATUS_TYPE_ID,
                    TimeSpan.FromMilliseconds(1000),
                    0))
                .Returns(enchantmentStore.Object);

            var repository = EnchantmentStoreRepository.Create(
                database.Object, 
                factory.Object);
           
            var result = repository.GetById(STORE_ID);

            Assert.Equal(enchantmentStore.Object, result);

            factory.Verify(x => x.CreateEnchantmentStore(
                    STORE_ID,
                    STAT_ID,
                    CALCULATION_ID,
                    TRIGGER_ID,
                    STATUS_TYPE_ID,
                    TimeSpan.FromMilliseconds(1000),
                    0),
                    Times.Once);

            database.Verify(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);

            command.Verify(x => x.ExecuteReader(), Times.Once);
        }

        [Fact]
        public void EnchantmentStoreRepository_GetByIdNotAvailable_Throws()
        {
            Guid STORE_ID = new Guid();

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

            var enchantmentStore = new Mock<IEnchantmentStore>();

            var factory = new Mock<IEnchantmentStoreFactory>();

            var repository = EnchantmentStoreRepository.Create(
                database.Object,
                factory.Object);

            var exception = Assert.Throws<InvalidOperationException>(() => repository.GetById(STORE_ID));
            Assert.Equal("No enchantment with Id '" + STORE_ID + "' was found.", exception.Message);

            factory.Verify(x => x.CreateEnchantmentStore(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<TimeSpan>(),
                    It.IsAny<double>()),
                    Times.Never);

            database.Verify(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);

            command.Verify(x => x.ExecuteReader(), Times.Once);
        }

        [Fact]
        public void EnchantmentStoreRepository_RemoveById_Success()
        {
            Guid STORE_ID = new Guid();
         
            var command = new Mock<IDbCommand>();

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(command.Object);

            var factory = new Mock<IEnchantmentStoreFactory>();

            var repository = EnchantmentStoreRepository.Create(
                database.Object,
                factory.Object);

            repository.RemoveById(STORE_ID);
            
            database.Verify(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);

            command.Verify(x => x.ExecuteNonQuery(), Times.Once);
        }

        [Fact]
        public void EnchantmentStoreRepository_AddValidObject_Success()
        {
            Guid STORE_ID = new Guid();
            Guid STAT_ID = new Guid();
            Guid CALCULATION_ID = new Guid();
            Guid TRIGGER_ID = new Guid();
            Guid STATUS_TYPE_ID = new Guid();

            var command = new Mock<IDbCommand>();

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()))
                .Returns(command.Object);

            var enchantmentStore = new Mock<IEnchantmentStore>();
            enchantmentStore
                .Setup(x => x.CalculationId)
                .Returns(CALCULATION_ID);
            enchantmentStore
                .Setup(x => x.Id)
                .Returns(STORE_ID);
            enchantmentStore
                .Setup(x => x.RemainingDuration)
                .Returns(TimeSpan.FromMilliseconds(123));
            enchantmentStore
                .Setup(x => x.StatId)
                .Returns(STAT_ID);
            enchantmentStore
                .Setup(x => x.StatusTypeId)
                .Returns(STATUS_TYPE_ID);
            enchantmentStore
                .Setup(x => x.TriggerId)
                .Returns(TRIGGER_ID);
            enchantmentStore
                .Setup(x => x.Value)
                .Returns(456);

            var factory = new Mock<IEnchantmentStoreFactory>();

            var repository = EnchantmentStoreRepository.Create(
                database.Object,
                factory.Object);
            
            repository.Add(enchantmentStore.Object);

            database.Verify(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()), Times.Once);

            command.Verify(x => x.ExecuteNonQuery(), Times.Once);

            enchantmentStore.Verify(x => x.CalculationId, Times.Once);
            enchantmentStore.Verify(x => x.Id, Times.Once);
            enchantmentStore.Verify(x => x.RemainingDuration, Times.Once);
            enchantmentStore.Verify(x => x.StatId, Times.Once);
            enchantmentStore.Verify(x => x.StatusTypeId, Times.Once);
            enchantmentStore.Verify(x => x.TriggerId, Times.Once);
            enchantmentStore.Verify(x => x.Value, Times.Once);
        }
        #endregion
    }
}