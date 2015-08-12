using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Moq;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Sql;
using ProjectXyz.Plugins.Enchantments.OneShotNegate.Sql;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate.Tests.Integration
{
    [DataLayer]
    [Enchantments]
    public class OneShotNegateEnchantmentStoreRepositoryTests
    {
        #region Fields
        private readonly IDatabase _database;
        #endregion

        #region Constructors
        public OneShotNegateEnchantmentStoreRepositoryTests()
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
            
            var enchantmentStore = new Mock<IOneShotNegateEnchantmentStore>(MockBehavior.Strict);
            enchantmentStore
                .Setup(x => x.Id)
                .Returns(enchantmentStoreId);
            enchantmentStore
                .Setup(x => x.StatId)
                .Returns(statId);

            var factory = new Mock<IOneShotNegateEnchantmentStoreFactory>(MockBehavior.Strict);

            var enchantmentStoreRepository = new Mock<IEnchantmentStoreRepository<IEnchantmentStore>>(MockBehavior.Strict);
            enchantmentStoreRepository
                .Setup(x => x.Add(enchantmentStore.Object));

            var repository = OneShotNegateEnchantmentStoreRepository.Create(
                _database,
                enchantmentStoreRepository.Object,
                factory.Object);

            // Execute
            repository.Add(enchantmentStore.Object);

            // Assert
            using (var reader = _database.Query("SELECT COUNT(1) FROM OneShotNegateEnchantments WHERE EnchantmentId=@Id", "@Id", enchantmentStoreId))
            {
                Assert.True(reader.Read(), "Expecting the reader to read a single row.");
                Assert.Equal(1, reader.GetInt32(0));
            }

            enchantmentStore.Verify(x => x.Id, Times.Once);
            enchantmentStore.Verify(x => x.StatId, Times.Once);

            enchantmentStoreRepository.Verify(x => x.Add(It.IsAny<IEnchantmentStore>()), Times.Once);
        }

        [Fact]
        public void RemoveById_IdExists_Success()
        {
            // Setup
            var enchantmentStoreId = Guid.NewGuid();
            var statId = Guid.NewGuid();
            const double VALUE = 12345;
            
            var factory = new Mock<IOneShotNegateEnchantmentStoreFactory>(MockBehavior.Strict);

            var enchantmentStoreRepository = new Mock<IEnchantmentStoreRepository<IEnchantmentStore>>(MockBehavior.Strict);
            enchantmentStoreRepository
                .Setup(x => x.RemoveById(enchantmentStoreId));

            var repository = OneShotNegateEnchantmentStoreRepository.Create(
                _database,
                enchantmentStoreRepository.Object,
                factory.Object);

            var namedParameters = new Dictionary<string, object>()
            {
                { "EnchantmentId", enchantmentStoreId },
                { "StatId", statId }
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    OneShotNegateEnchantments
                (
                    EnchantmentId,
                    StatId
                )
                VALUES
                (
                    @EnchantmentId,
                    @StatId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            // Execute
            repository.RemoveById(enchantmentStoreId);

            // Assert
            using (var reader = _database.Query("SELECT COUNT(1) FROM OneShotNegateEnchantments WHERE EnchantmentId=@Id", "@Id", enchantmentStoreId))
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

            var enchantmentStore = new Mock<IOneShotNegateEnchantmentStore>(MockBehavior.Strict);

            var factory = new Mock<IOneShotNegateEnchantmentStoreFactory>(MockBehavior.Strict);
            factory
                .Setup(x => x.CreateEnchantmentStore(enchantmentStoreId, statId, triggerId, statusTypeId))
                .Returns(enchantmentStore.Object);

            var enchantmentStoreRepository = new Mock<IEnchantmentStoreRepository<IEnchantmentStore>>(MockBehavior.Strict);

            var repository = OneShotNegateEnchantmentStoreRepository.Create(
                _database,
                enchantmentStoreRepository.Object,
                factory.Object);

            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", enchantmentStoreId },
                { "EnchantmentTypeId", enchantmentTypeId },
                { "TriggerId", triggerId },
                { "StatusTypeId", statusTypeId },
                { "RemainingDuration", 0 },
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
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    OneShotNegateEnchantments
                (
                    EnchantmentId,
                    StatId
                )
                VALUES
                (
                    @EnchantmentId,
                    @StatId
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
                    It.IsAny<Guid>()), 
                Times.Once);
        }
        #endregion
    }
}