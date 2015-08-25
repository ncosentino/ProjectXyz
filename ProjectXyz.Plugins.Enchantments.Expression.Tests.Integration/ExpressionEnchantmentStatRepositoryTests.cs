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
    public class ExpressionEnchantmentStatRepositoryTests : DatabaseTest
    {
        #region Methods
        [Fact]
        public void Add_ValidEnchantmentStat_Success()
        {
            // Setup
            var enchantmentStatId = Guid.NewGuid();
            var expressionEnchantmentId = Guid.NewGuid();
            var statId = Guid.NewGuid();
            const string ID_FOR_EXPRESSION = "this is the id for the expression";
            
            var enchantmentStat = new Mock<IExpressionEnchantmentStat>(MockBehavior.Strict);
            enchantmentStat
                .Setup(x => x.Id)
                .Returns(enchantmentStatId);
            enchantmentStat
                .Setup(x => x.ExpressionEnchantmentId)
                .Returns(expressionEnchantmentId);
            enchantmentStat
                .Setup(x => x.StatId)
                .Returns(statId);
            enchantmentStat
                .Setup(x => x.IdForExpression)
                .Returns(ID_FOR_EXPRESSION);

            var factory = new Mock<IExpressionEnchantmentStatFactory>(MockBehavior.Strict);
            
            var repository = ExpressionEnchantmentStatRepository.Create(
                Database,
                factory.Object);

            // Execute
            repository.Add(enchantmentStat.Object);

            // Assert
            using (var reader = Database.Query("SELECT COUNT(1) FROM ExpressionEnchantmentStats WHERE Id=@Id", "@Id", enchantmentStatId))
            {
                Assert.True(reader.Read(), "Expecting the reader to read a single row.");
                Assert.Equal(1, reader.GetInt32(0));
            }

            enchantmentStat.Verify(x => x.Id, Times.Once);
            enchantmentStat.Verify(x => x.ExpressionEnchantmentId, Times.Once);
            enchantmentStat.Verify(x => x.StatId, Times.Once);
            enchantmentStat.Verify(x => x.IdForExpression, Times.Once);
        }

        [Fact]
        public void RemoveById_IdExists_Success()
        {
            // Setup
            var enchantmentStatId = Guid.NewGuid();
            var expressionEnchantmentId = Guid.NewGuid();
            var statId = Guid.NewGuid();
            const string ID_FOR_EXPRESSION = "this is the id for the expression";
            
            var factory = new Mock<IExpressionEnchantmentStatFactory>(MockBehavior.Strict);
            
            var repository = ExpressionEnchantmentStatRepository.Create(
                Database,
                factory.Object);

            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", enchantmentStatId },
                { "expressionEnchantmentId", expressionEnchantmentId },
                { "StatId", statId },
                { "IdForExpression", ID_FOR_EXPRESSION },
            };

            using (var command = Database.CreateCommand(
                @"
                INSERT INTO
                    ExpressionEnchantmentStats
                (
                    Id,
                    ExpressionEnchantmentId,
                    IdForExpression,
                    StatId
                )
                VALUES
                (
                    @Id,
                    @ExpressionEnchantmentId,
                    @IdForExpression,
                    @StatId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            // Execute
            repository.RemoveById(enchantmentStatId);

            // Assert
            using (var reader = Database.Query("SELECT COUNT(1) FROM ExpressionEnchantments WHERE EnchantmentId=@Id", "@Id", enchantmentStatId))
            {
                Assert.True(reader.Read(), "Expecting the reader to read a single row.");
                Assert.Equal(0, reader.GetInt32(0));
            }
        }

        [Fact]
        public void GetById_IdExists_Success()
        {
            // Setup
            var enchantmentStatId = Guid.NewGuid();
            var expressionEnchantmentId = Guid.NewGuid();
            var statId = Guid.NewGuid();
            const string ID_FOR_EXPRESSION = "this is the id for the expression";

            var enchantmentStat = new Mock<IExpressionEnchantmentStat>(MockBehavior.Strict);

            var factory = new Mock<IExpressionEnchantmentStatFactory>(MockBehavior.Strict);
            factory
                .Setup(x => x.CreateExpressionEnchantmentStat(enchantmentStatId, expressionEnchantmentId, ID_FOR_EXPRESSION, statId))
                .Returns(enchantmentStat.Object);
            
            var repository = ExpressionEnchantmentStatRepository.Create(
                Database,
                factory.Object);

            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", enchantmentStatId },
                { "expressionEnchantmentId", expressionEnchantmentId },
                { "StatId", statId },
                { "IdForExpression", ID_FOR_EXPRESSION },
            };

            using (var command = Database.CreateCommand(
                @"
                INSERT INTO
                    ExpressionEnchantmentStats
                (
                    Id,
                    ExpressionEnchantmentId,
                    IdForExpression,
                    StatId
                )
                VALUES
                (
                    @Id,
                    @ExpressionEnchantmentId,
                    @IdForExpression,
                    @StatId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            // Execute
            var result = repository.GetById(enchantmentStatId);

            // Assert
            Assert.Equal(enchantmentStat.Object, result);

            factory.Verify(
                x => x.CreateExpressionEnchantmentStat(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<string>(),
                    It.IsAny<Guid>()), 
                Times.Once);
        }
        #endregion
    }
}