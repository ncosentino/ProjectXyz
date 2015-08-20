using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Moq;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Sql;
using ProjectXyz.Plugins.Enchantments.Additive.Sql;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Enchantments.Additive.Tests.Unit
{
    [DataLayer]
    [Enchantments]
    public class AdditiveEnchantmentStoreRepositoryTests
    {
        #region Constants
        private const string COLUMN_NAME_ID = "Id";
        private const int COLUMN_INDEX_ID = 0;

        private const string COLUMN_NAME_ENCHANTMENT_TYPE_ID = "EnchantmentTypeId";
        private const int COLUMN_INDEX_ENCHANTMENT_TYPE_ID = COLUMN_INDEX_ID + 1;

        private const string COLUMN_NAME_STAT_ID = "StatId";
        private const int COLUMN_INDEX_STAT_ID = COLUMN_INDEX_ENCHANTMENT_TYPE_ID + 1;
        
        private const string COLUMN_NAME_TRIGGER_ID = "TriggerId";
        private const int COLUMN_INDEX_TRIGGER_ID = COLUMN_INDEX_STAT_ID + 1;

        private const string COLUMN_NAME_STATUS_TYPE_ID = "StatusTypeId";
        private const int COLUMN_INDEX_STATUS_TYPE_ID = COLUMN_INDEX_TRIGGER_ID + 1;

        private const string COLUMN_NAME_VALUE = "Value";
        private const int COLUMN_INDEX_VALUE = COLUMN_INDEX_STATUS_TYPE_ID + 1;

        private const string COLUMN_NAME_REMAINING_DURATION = "RemainingDuration";
        private const int COLUMN_INDEX_REMAINING_DURATION = COLUMN_INDEX_VALUE + 1;
        #endregion

        #region Methods
        [Fact]
        public void GetById_IdExists_ExpectedValues()
        {
            // Setup
            Guid STORE_ID =  Guid.NewGuid();
            Guid enchantmentTypeId = Guid.NewGuid();
            Guid STAT_ID = Guid.NewGuid();
            Guid TRIGGER_ID = Guid.NewGuid();
            Guid STATUS_TYPE_ID = Guid.NewGuid();
            var remainingDuration = TimeSpan.FromSeconds(123);

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
                .Setup(x => x.GetOrdinal(COLUMN_NAME_ENCHANTMENT_TYPE_ID))
                .Returns(COLUMN_INDEX_ENCHANTMENT_TYPE_ID);
            reader
                .Setup(x => x.GetGuid(COLUMN_INDEX_ENCHANTMENT_TYPE_ID))
                .Returns(enchantmentTypeId);

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_STAT_ID))
                .Returns(COLUMN_INDEX_STAT_ID);
            reader
                .Setup(x => x.GetGuid(COLUMN_INDEX_STAT_ID))
                .Returns(STAT_ID);

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
                .Returns(remainingDuration.TotalMilliseconds);
                        
            var command = new Mock<IDbCommand>();
            command
                .Setup(x => x.ExecuteReader())
                .Returns(reader.Object);

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(command.Object);

            var enchantmentStore = new Mock<IAdditiveEnchantmentStore>();

            var factory = new Mock<IAdditiveEnchantmentStoreFactory>(MockBehavior.Strict);
            factory
                .Setup(x => x.CreateEnchantmentStore(
                    STORE_ID, 
                    STAT_ID,
                    0,
                    remainingDuration))
                .Returns(enchantmentStore.Object);

            var repository = AdditiveEnchantmentStoreRepository.Create(
                database.Object,
                factory.Object);
           
            // Execute
            var result = repository.GetById(STORE_ID);

            // Assert
            Assert.Equal(enchantmentStore.Object, result);

            factory.Verify(x => x.CreateEnchantmentStore(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<double>(),
                    It.IsAny<TimeSpan>()),
                    Times.Once);

            database.Verify(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);

            command.Verify(x => x.ExecuteReader(), Times.Once);
        }

        [Fact]
        public void GetById_NotAvailable_Throws()
        {
            // Setup
            Guid STORE_ID = new Guid();

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

            var factory = new Mock<IAdditiveEnchantmentStoreFactory>(MockBehavior.Strict);

            var repository = AdditiveEnchantmentStoreRepository.Create(
                database.Object,
                factory.Object);

            // Execute
            var exception = Assert.Throws<InvalidOperationException>(() => repository.GetById(STORE_ID));

            // Assert
            Assert.Equal("No enchantment with Id '" + STORE_ID + "' was found.", exception.Message);

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

            var factory = new Mock<IAdditiveEnchantmentStoreFactory>();
            
            var repository = AdditiveEnchantmentStoreRepository.Create(
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
            Guid STORE_ID = new Guid();
            Guid STAT_ID = new Guid();
            var remainingDuration = TimeSpan.FromSeconds(123);

            var command = new Mock<IDbCommand>();

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()))
                .Returns(command.Object);

            var enchantmentStore = new Mock<IAdditiveEnchantmentStore>(MockBehavior.Strict);
            enchantmentStore
                .Setup(x => x.Id)
                .Returns(STORE_ID);
            enchantmentStore
                .Setup(x => x.StatId)
                .Returns(STAT_ID);
            enchantmentStore
                .Setup(x => x.Value)
                .Returns(456);
            enchantmentStore
                .Setup(x => x.RemainingDuration)
                .Returns(remainingDuration);

            var factory = new Mock<IAdditiveEnchantmentStoreFactory>();
            
            var repository = AdditiveEnchantmentStoreRepository.Create(
                database.Object,
                factory.Object);
            
            // Execute
            repository.Add(enchantmentStore.Object);

            // Assert
            database.Verify(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()), Times.Once);

            command.Verify(x => x.ExecuteNonQuery(), Times.Once);

            enchantmentStore.Verify(x => x.Id, Times.Once);
            enchantmentStore.Verify(x => x.StatId, Times.Once);
            enchantmentStore.Verify(x => x.Value, Times.Once);
            enchantmentStore.Verify(x => x.RemainingDuration, Times.Once);
        }
        #endregion
    }
}