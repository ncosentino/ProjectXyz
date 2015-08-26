using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Moq;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Sql;
using ProjectXyz.Plugins.Enchantments.Expression.Sql;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Enchantments.Expression.Tests.Integration
{
    [DataLayer]
    [Enchantments]
    public class ExpressionEnchantmentStoreRepositoryTests : DatabaseTest
    {
        #region Methods
        [Fact]
        public void Add_ValidEnchantmentStore_Success()
        {
            // Setup
            var enchantmentStoreId = Guid.NewGuid();
            var statId = Guid.NewGuid();
            var expressionId = Guid.NewGuid();
            var remainingDuration = TimeSpan.FromSeconds(123);
            
            var enchantmentStore = new Mock<IExpressionEnchantmentStore>(MockBehavior.Strict);
            enchantmentStore
                .Setup(x => x.Id)
                .Returns(enchantmentStoreId);
            enchantmentStore
                .Setup(x => x.StatId)
                .Returns(statId);
            enchantmentStore
                .Setup(x => x.ExpressionId)
                .Returns(expressionId);
            enchantmentStore
                .Setup(x => x.RemainingDuration)
                .Returns(remainingDuration);

            var factory = new Mock<IExpressionEnchantmentStoreFactory>(MockBehavior.Strict);
            
            var repository = ExpressionEnchantmentStoreRepository.Create(
                Database,
                factory.Object);

            // Execute
            repository.Add(enchantmentStore.Object);

            // Assert
            using (var reader = Database.Query("SELECT COUNT(1) FROM ExpressionEnchantments WHERE EnchantmentId=@Id", "@Id", enchantmentStoreId))
            {
                Assert.True(reader.Read(), "Expecting the reader to read a single row.");
                Assert.Equal(1, reader.GetInt32(0));
            }

            enchantmentStore.Verify(x => x.Id, Times.Once);
            enchantmentStore.Verify(x => x.StatId, Times.Once);
            enchantmentStore.Verify(x => x.ExpressionId, Times.Once);
            enchantmentStore.Verify(x => x.RemainingDuration, Times.Once);
        }

        [Fact]
        public void RemoveById_IdExists_Success()
        {
            // Setup
            var enchantmentStoreId = Guid.NewGuid();
            var statId = Guid.NewGuid();
            var expressionId = Guid.NewGuid();
            var remainingDuration = TimeSpan.FromSeconds(123);
            
            var factory = new Mock<IExpressionEnchantmentStoreFactory>(MockBehavior.Strict);
            
            var repository = ExpressionEnchantmentStoreRepository.Create(
                Database,
                factory.Object);

            var namedParameters = new Dictionary<string, object>()
            {
                { "EnchantmentId", enchantmentStoreId },
                { "StatId", statId },
                { "ExpressionId", expressionId },
                { "RemainingDuration", remainingDuration },
            };

            using (var command = Database.CreateCommand(
                @"
                INSERT INTO
                    ExpressionEnchantments
                (
                    EnchantmentId,
                    StatId,
                    ExpressionId,
                    RemainingDuration
                )
                VALUES
                (
                    @EnchantmentId,
                    @StatId,
                    @ExpressionId,
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
            using (var reader = Database.Query("SELECT COUNT(1) FROM ExpressionEnchantments WHERE EnchantmentId=@Id", "@Id", enchantmentStoreId))
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
            var expressionId = Guid.NewGuid();
            var remainingDuration = TimeSpan.FromSeconds(123);

            var enchantmentStore = new Mock<IExpressionEnchantmentStore>(MockBehavior.Strict);

            var factory = new Mock<IExpressionEnchantmentStoreFactory>(MockBehavior.Strict);
            factory
                .Setup(x => x.CreateEnchantmentStore(enchantmentStoreId, statId, expressionId, remainingDuration))
                .Returns(enchantmentStore.Object);
            
            var repository = ExpressionEnchantmentStoreRepository.Create(
                Database,
                factory.Object);

            var namedParameters = new Dictionary<string, object>()
            {
                { "EnchantmentId", enchantmentStoreId },
                { "StatId", statId },
                { "ExpressionId", expressionId },
                { "RemainingDuration", remainingDuration.TotalMilliseconds },
            };

            using (var command = Database.CreateCommand(
                @"
                INSERT INTO
                    ExpressionEnchantments
                (
                    EnchantmentId,
                    StatId,
                    ExpressionId,
                    RemainingDuration
                )
                VALUES
                (
                    @EnchantmentId,
                    @StatId,
                    @ExpressionId,
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
                    It.IsAny<Guid>(),
                    It.IsAny<TimeSpan>()), 
                Times.Once);
        }
        #endregion
    }
}