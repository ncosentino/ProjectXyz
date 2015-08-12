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

            var factory = new Mock<IAdditiveEnchantmentStoreFactory>(MockBehavior.Strict);

            var enchantmentStoreRepository = new Mock<IEnchantmentStoreRepository<IEnchantmentStore>>(MockBehavior.Strict);
            enchantmentStoreRepository
                .Setup(x => x.Add(enchantmentStore.Object));

            var repository = AdditiveEnchantmentStoreRepository.Create(
                _database,
                enchantmentStoreRepository.Object,
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

            enchantmentStoreRepository.Verify(x => x.Add(It.IsAny<IEnchantmentStore>()), Times.Once);
        }

        [Fact]
        public void RemoveById_IdExists_Success()
        {
            // Setup
            var enchantmentStoreId = Guid.NewGuid();
            var statId = Guid.NewGuid();
            const double VALUE = 12345;
            
            var factory = new Mock<IAdditiveEnchantmentStoreFactory>(MockBehavior.Strict);

            var enchantmentStoreRepository = new Mock<IEnchantmentStoreRepository<IEnchantmentStore>>(MockBehavior.Strict);
            enchantmentStoreRepository
                .Setup(x => x.RemoveById(enchantmentStoreId));

            var repository = AdditiveEnchantmentStoreRepository.Create(
                _database,
                enchantmentStoreRepository.Object,
                factory.Object);

            var namedParameters = new Dictionary<string, object>()
            {
                { "EnchantmentId", enchantmentStoreId },
                { "StatId", statId },
                { "Value", VALUE },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    AdditiveEnchantments
                (
                    EnchantmentId,
                    StatId,
                    Value
                )
                VALUES
                (
                    @EnchantmentId,
                    @StatId,
                    @Value
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

            enchantmentStoreRepository.Verify(x => x.RemoveById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void GetById_IdExists_Success()
        {
            // Setup
            var enchantmentStoreId = Guid.NewGuid();
            var statId = Guid.NewGuid();
            var enchantmentTypeId = Guid.NewGuid();
            var triggerId = Guid.NewGuid();
            var statusTypeId = Guid.NewGuid();
            var remainingDuration = TimeSpan.FromSeconds(123);
            const double VALUE = 12345;

            var enchantmentStore = new Mock<IAdditiveEnchantmentStore>(MockBehavior.Strict);

            var factory = new Mock<IAdditiveEnchantmentStoreFactory>(MockBehavior.Strict);
            factory
                .Setup(x => x.CreateEnchantmentStore(enchantmentStoreId, statId, triggerId, statusTypeId, remainingDuration, VALUE))
                .Returns(enchantmentStore.Object);

            var enchantmentStoreRepository = new Mock<IEnchantmentStoreRepository<IEnchantmentStore>>(MockBehavior.Strict);

            var repository = AdditiveEnchantmentStoreRepository.Create(
                _database,
                enchantmentStoreRepository.Object,
                factory.Object);

            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", enchantmentStoreId },
                { "EnchantmentTypeId", enchantmentTypeId },
                { "TriggerId", triggerId },
                { "StatusTypeId", statusTypeId },
                { "RemainingDuration", remainingDuration.TotalMilliseconds },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    Enchantments
                (
                    Id,
                    EnchantmentTypeId,
                    TriggerId,
                    StatusTypeId,
                    RemainingDuration
                )
                VALUES
                (
                    @Id,
                    @EnchantmentTypeId,
                    @TriggerId,
                    @StatusTypeId,
                    @RemainingDuration
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            namedParameters = new Dictionary<string, object>()
            {
                { "EnchantmentId", enchantmentStoreId },
                { "StatId", statId },
                { "Value", VALUE },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    AdditiveEnchantments
                (
                    EnchantmentId,
                    StatId,
                    Value
                )
                VALUES
                (
                    @EnchantmentId,
                    @StatId,
                    @Value
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
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<TimeSpan>(),
                    It.IsAny<double>()), 
                Times.Once);
        }
        #endregion
    }
}