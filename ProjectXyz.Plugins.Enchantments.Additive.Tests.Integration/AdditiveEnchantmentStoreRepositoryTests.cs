using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Moq;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Sql;
using ProjectXyz.Plugins.Enchantments.Additive.Sql;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Enchantments.Additive.Tests.Integration
{
    [DataLayer]
    [Enchantments]
    public class AdditiveEnchantmentStoreRepositoryTests
    {
        #region Fields
        private readonly IDatabase _database;
        #endregion

        #region Constructors
        public AdditiveEnchantmentStoreRepositoryTests()
        {
            var connection = new SQLiteConnection("Data Source=:memory:");
            connection.Open();

            _database = SqlDatabase.Create(connection, true);

            SqlDatabaseUpgrader.Create().UpgradeDatabase(_database, 0, 1);
        }
        #endregion

        #region Methods
        [Fact]
        public void Add_ValidEnchantmentStore_Success()
        {
            // Setup
            var enchantmentStoreId = Guid.NewGuid();
            var statId = Guid.NewGuid();
            const double VALUE = 12345;
            var remainingDuration = TimeSpan.FromSeconds(123);
            
            var enchantmentStore = new Mock<IAdditiveEnchantmentStore>(MockBehavior.Strict);
            enchantmentStore
                .Setup(x => x.Id)
                .Returns(enchantmentStoreId);
            enchantmentStore
                .Setup(x => x.StatId)
                .Returns(statId);
            enchantmentStore
                .Setup(x => x.Value)
                .Returns(VALUE);
            enchantmentStore
                .Setup(x => x.RemainingDuration)
                .Returns(remainingDuration);

            var factory = new Mock<IAdditiveEnchantmentStoreFactory>(MockBehavior.Strict);
            
            var repository = AdditiveEnchantmentStoreRepository.Create(
                _database,
                factory.Object);

            // Execute
            repository.Add(enchantmentStore.Object);

            // Assert
            using (var reader = _database.Query("SELECT COUNT(1) FROM AdditiveEnchantments WHERE EnchantmentId=@Id", "@Id", enchantmentStoreId))
            {
                Assert.True(reader.Read(), "Expecting the reader to read a single row.");
                Assert.Equal(1, reader.GetInt32(0));
            }

            enchantmentStore.Verify(x => x.Id, Times.Once);
            enchantmentStore.Verify(x => x.StatId, Times.Once);
            enchantmentStore.Verify(x => x.Value, Times.Once);
            enchantmentStore.Verify(x => x.RemainingDuration, Times.Once);
        }

        [Fact]
        public void RemoveById_IdExists_Success()
        {
            // Setup
            var enchantmentStoreId = Guid.NewGuid();
            var statId = Guid.NewGuid();
            const double VALUE = 12345;
            var remainingDuration = TimeSpan.FromSeconds(123);
            
            var factory = new Mock<IAdditiveEnchantmentStoreFactory>(MockBehavior.Strict);
            
            var repository = AdditiveEnchantmentStoreRepository.Create(
                _database,
                factory.Object);

            var namedParameters = new Dictionary<string, object>()
            {
                { "EnchantmentId", enchantmentStoreId },
                { "StatId", statId },
                { "Value", VALUE },
                { "RemainingDuration", remainingDuration },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    AdditiveEnchantments
                (
                    EnchantmentId,
                    StatId,
                    Value,
                    RemainingDuration
                )
                VALUES
                (
                    @EnchantmentId,
                    @StatId,
                    @Value,
                    @RemainingDuration
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            // Execute
            repository.RemoveById(enchantmentStoreId);

            // Assert
            using (var reader = _database.Query("SELECT COUNT(1) FROM AdditiveEnchantments WHERE EnchantmentId=@Id", "@Id", enchantmentStoreId))
            {
                Assert.True(reader.Read(), "Expecting the reader to read a single row.");
                Assert.Equal(0, reader.GetInt32(0));
            }
        }

        [Fact]
        public void GetById_IdExists_Success()
        {
            // Setup
            var enchantmentStoreId = Guid.NewGuid();
            var statId = Guid.NewGuid();
            const double VALUE = 12345;
            var remainingDuration = TimeSpan.FromSeconds(123);

            var enchantmentStore = new Mock<IAdditiveEnchantmentStore>(MockBehavior.Strict);

            var factory = new Mock<IAdditiveEnchantmentStoreFactory>(MockBehavior.Strict);
            factory
                .Setup(x => x.CreateEnchantmentStore(enchantmentStoreId, statId, VALUE, remainingDuration))
                .Returns(enchantmentStore.Object);
            
            var repository = AdditiveEnchantmentStoreRepository.Create(
                _database,
                factory.Object);

            var namedParameters = new Dictionary<string, object>()
            {
                { "EnchantmentId", enchantmentStoreId },
                { "StatId", statId },
                { "Value", VALUE },
                { "RemainingDuration", remainingDuration.TotalMilliseconds },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    AdditiveEnchantments
                (
                    EnchantmentId,
                    StatId,
                    Value,
                    RemainingDuration
                )
                VALUES
                (
                    @EnchantmentId,
                    @StatId,
                    @Value,
                    @RemainingDuration
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            // Execute
            var result = repository.GetById(enchantmentStoreId);

            // Assert
            Assert.Equal(enchantmentStore.Object, result);

            factory.Verify(
                x => x.CreateEnchantmentStore(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<double>(),
                    It.IsAny<TimeSpan>()), 
                Times.Once);
        }
        #endregion
    }
}