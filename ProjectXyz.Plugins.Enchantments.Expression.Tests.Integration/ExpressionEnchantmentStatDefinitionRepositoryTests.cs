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
    public class ExpressionEnchantmentStatDefinitionRepositoryTests : DatabaseTest
    {
        #region Methods
        [Fact]
        public void Add_ValidEnchantmentStatDefinition_Success()
        {
            // Setup
            var id = Guid.NewGuid();
            var enchantmentDefinitionId = Guid.NewGuid();
            var statId = Guid.NewGuid();
            const string ID_FOR_EXPRESSION = "this is the id for the expression";
            
            var enchantmentDefinition = new Mock<IExpressionEnchantmentStatDefinition>(MockBehavior.Strict);
            enchantmentDefinition
                .Setup(x => x.Id)
                .Returns(id);
            enchantmentDefinition
                .Setup(x => x.EnchantmentDefinitionId)
                .Returns(enchantmentDefinitionId);
            enchantmentDefinition
                .Setup(x => x.StatId)
                .Returns(statId);
            enchantmentDefinition
                .Setup(x => x.IdForExpression)
                .Returns(ID_FOR_EXPRESSION);
            var factory = new Mock<IExpressionEnchantmentStatDefinitionFactory>(MockBehavior.Strict);
            
            var repository = ExpressionEnchantmentStatDefinitionRepository.Create(
                Database,
                factory.Object);

            // Execute
            repository.Add(enchantmentDefinition.Object);

            // Assert
            using (var reader = Database.Query("SELECT COUNT(1) FROM ExpressionEnchantmentStatDefinitions WHERE Id=@Id", "@Id", id))
            {
                Assert.True(reader.Read(), "Expecting the reader to read a single row.");
                Assert.Equal(1, reader.GetInt32(0));
            }

            enchantmentDefinition.Verify(x => x.Id, Times.Once);
            enchantmentDefinition.Verify(x => x.EnchantmentDefinitionId, Times.Once);
            enchantmentDefinition.Verify(x => x.StatId, Times.Once);
            enchantmentDefinition.Verify(x => x.IdForExpression, Times.Once);
        }

        [Fact]
        public void RemoveById_IdExists_Success()
        {
            // Setup
            var id = Guid.NewGuid();
            var enchantmentDefinitionId = Guid.NewGuid();
            var statId = Guid.NewGuid();
            const string ID_FOR_EXPRESSION = "this is the id for the expression";

            var factory = new Mock<IExpressionEnchantmentStatDefinitionFactory>(MockBehavior.Strict);

            var repository = ExpressionEnchantmentStatDefinitionRepository.Create(
                Database,
                factory.Object);

            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "EnchantmentDefinitionId", enchantmentDefinitionId },
                { "IdForExpression", ID_FOR_EXPRESSION },
                { "StatId", statId },
            };

            using (var command = Database.CreateCommand(
                @"
                INSERT INTO
                    ExpressionEnchantmentStatDefinitions
                (
                    Id,
                    EnchantmentDefinitionId,
                    IdForExpression,
                    StatId
                )
                VALUES
                (
                    @Id,
                    @EnchantmentDefinitionId,
                    @IdForExpression,
                    @StatId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            // Execute
            repository.RemoveById(id);

            // Assert
            using (var reader = Database.Query("SELECT COUNT(1) FROM ExpressionEnchantmentStatDefinitions WHERE Id=@Id", "@Id", id))
            {
                Assert.True(reader.Read(), "Expecting the reader to read a single row.");
                Assert.Equal(0, reader.GetInt32(0));
            }
        }

        [Fact]
        public void GetById_IdExists_Success()
        {
            // Setup
            var id = Guid.NewGuid();
            var enchantmentDefinitionId = Guid.NewGuid();
            var statId = Guid.NewGuid();
            const string ID_FOR_EXPRESSION = "this is the id for the expression";

            var enchantmentDefinition = new Mock<IExpressionEnchantmentStatDefinition>(MockBehavior.Strict);

            var factory = new Mock<IExpressionEnchantmentStatDefinitionFactory>(MockBehavior.Strict);
            factory
                .Setup(x => x.CreateEnchantmentStatDefinition(id, enchantmentDefinitionId, ID_FOR_EXPRESSION, statId))
                .Returns(enchantmentDefinition.Object);

            var repository = ExpressionEnchantmentStatDefinitionRepository.Create(
                Database,
                factory.Object);

            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "EnchantmentDefinitionId", enchantmentDefinitionId },
                { "IdForExpression", ID_FOR_EXPRESSION },
                { "StatId", statId },
            };

            using (var command = Database.CreateCommand(
                @"
                INSERT INTO
                    ExpressionEnchantmentStatDefinitions
                (
                    Id,
                    EnchantmentDefinitionId,
                    IdForExpression,
                    StatId
                )
                VALUES
                (
                    @Id,
                    @EnchantmentDefinitionId,
                    @IdForExpression,
                    @StatId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            // Execute
            var result = repository.GetById(id);

            // Assert
            Assert.Equal(enchantmentDefinition.Object, result);

            factory.Verify(
                x => x.CreateEnchantmentStatDefinition(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<string>(),
                    It.IsAny<Guid>()),
                Times.Once);
        }
        #endregion
    }
}