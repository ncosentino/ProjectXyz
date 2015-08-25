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
    public class ExpressionEnchantmentDefinitionRepositoryTests : DatabaseTest
    {
        #region Methods
        [Fact]
        public void Add_ValidEnchantmentDefinition_Success()
        {
            // Setup
            var id = Guid.NewGuid();
            var enchantmentDefinitionId = Guid.NewGuid();
            var statId = Guid.NewGuid();
            const string EXPRESSION = "this is the expression";
            var minimumDuration = TimeSpan.FromSeconds(123);
            var maximumDuration = TimeSpan.FromSeconds(456);
            
            var enchantmentDefinition = new Mock<IExpressionEnchantmentDefinition>(MockBehavior.Strict);
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
                .Setup(x => x.Expression)
                .Returns(EXPRESSION);
            enchantmentDefinition
                .Setup(x => x.MinimumDuration)
                .Returns(minimumDuration);
            enchantmentDefinition
                .Setup(x => x.MaximumDuration)
                .Returns(maximumDuration);

            var factory = new Mock<IExpressionEnchantmentDefinitionFactory>(MockBehavior.Strict);
            
            var repository = ExpressionEnchantmentDefinitionRepository.Create(
                Database,
                factory.Object);

            // Execute
            repository.Add(enchantmentDefinition.Object);

            // Assert
            using (var reader = Database.Query("SELECT COUNT(1) FROM ExpressionEnchantmentDefinitions WHERE Id=@Id", "@Id", id))
            {
                Assert.True(reader.Read(), "Expecting the reader to read a single row.");
                Assert.Equal(1, reader.GetInt32(0));
            }

            enchantmentDefinition.Verify(x => x.Id, Times.Once);
            enchantmentDefinition.Verify(x => x.EnchantmentDefinitionId, Times.Once);
            enchantmentDefinition.Verify(x => x.StatId, Times.Once);
            enchantmentDefinition.Verify(x => x.Expression, Times.Once);
            enchantmentDefinition.Verify(x => x.MinimumDuration, Times.Once);
            enchantmentDefinition.Verify(x => x.MaximumDuration, Times.Once);
        }

        [Fact]
        public void RemoveById_IdExists_Success()
        {
            // Setup
            var id = Guid.NewGuid();
            var enchantmentDefinitionId = Guid.NewGuid();
            var statId = Guid.NewGuid();
            const string EXPRESSION = "this is the expression";
            var minimumDuration = TimeSpan.FromSeconds(123);
            var maximumDuration = TimeSpan.FromSeconds(456);

            var factory = new Mock<IExpressionEnchantmentDefinitionFactory>(MockBehavior.Strict);

            var repository = ExpressionEnchantmentDefinitionRepository.Create(
                Database,
                factory.Object);

            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "EnchantmentDefinitionId", enchantmentDefinitionId },
                { "Expression", EXPRESSION },
                { "StatId", statId },
                { "MinimumDuration", minimumDuration.TotalMilliseconds },
                { "MaximumDuration", maximumDuration.TotalMilliseconds },
            };

            using (var command = Database.CreateCommand(
                @"
                INSERT INTO
                    ExpressionEnchantmentDefinitions
                (
                    Id,
                    EnchantmentDefinitionId,
                    Expression,
                    StatId,
                    MinimumDuration,
                    MaximumDuration
                )
                VALUES
                (
                    @Id,
                    @EnchantmentDefinitionId,
                    @Expression,
                    @StatId,
                    @MinimumDuration,
                    @MaximumDuration
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            // Execute
            repository.RemoveById(id);

            // Assert
            using (var reader = Database.Query("SELECT COUNT(1) FROM ExpressionEnchantmentDefinitions WHERE Id=@Id", "@Id", id))
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
            const string EXPRESSION = "this is the expression";
            var minimumDuration = TimeSpan.FromSeconds(123);
            var maximumDuration = TimeSpan.FromSeconds(456);

            var enchantmentDefinition = new Mock<IExpressionEnchantmentDefinition>(MockBehavior.Strict);

            var factory = new Mock<IExpressionEnchantmentDefinitionFactory>(MockBehavior.Strict);
            factory
                .Setup(x => x.CreateEnchantmentDefinition(id, enchantmentDefinitionId, EXPRESSION, statId, minimumDuration, maximumDuration))
                .Returns(enchantmentDefinition.Object);

            var repository = ExpressionEnchantmentDefinitionRepository.Create(
                Database,
                factory.Object);

            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "EnchantmentDefinitionId", enchantmentDefinitionId },
                { "Expression", EXPRESSION },
                { "StatId", statId },
                { "MinimumDuration", minimumDuration.TotalMilliseconds },
                { "MaximumDuration", maximumDuration.TotalMilliseconds },
            };

            using (var command = Database.CreateCommand(
                @"
                INSERT INTO
                    ExpressionEnchantmentDefinitions
                (
                    Id,
                    EnchantmentDefinitionId,
                    Expression,
                    StatId,
                    MinimumDuration,
                    MaximumDuration
                )
                VALUES
                (
                    @Id,
                    @EnchantmentDefinitionId,
                    @Expression,
                    @StatId,
                    @MinimumDuration,
                    @MaximumDuration
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
                x => x.CreateEnchantmentDefinition(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<string>(),
                    It.IsAny<Guid>(),
                    It.IsAny<TimeSpan>(),
                    It.IsAny<TimeSpan>()),
                Times.Once);
        }
        #endregion
    }
}