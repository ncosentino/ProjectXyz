using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Plugins.Enchantments.Expression.Sql;
using ProjectXyz.Tests.Integration;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Enchantments.Expression.Tests.Integration
{
    [DataLayer]
    [Enchantments]
    public class ExpressionDefinitionRepositoryTests : DatabaseTest
    {
        #region Methods
        [Fact]
        public void Add_ValidDefinition_Success()
        {
            // Setup
            var id = Guid.NewGuid();
            const string EXPRESSION = "this is the expression";
            const int CALCULATION_PRIORITY = 123;
            
            var expressionDefinition = new Mock<IExpressionDefinition>(MockBehavior.Strict);
            expressionDefinition
                .Setup(x => x.Id)
                .Returns(id);
            expressionDefinition
                .Setup(x => x.Expression)
                .Returns(EXPRESSION);
            expressionDefinition
                .Setup(x => x.CalculationPriority)
                .Returns(CALCULATION_PRIORITY);

            var factory = new Mock<IExpressionDefinitionFactory>(MockBehavior.Strict);
            
            var repository = ExpressionDefinitionRepository.Create(
                Database,
                factory.Object);

            // Execute
            repository.Add(expressionDefinition.Object);

            // Assert
            using (var reader = Database.Query("SELECT COUNT(1) FROM ExpressionDefinitions WHERE Id=@Id", "@Id", id))
            {
                Assert.True(reader.Read(), "Expecting the reader to read a single row.");
                Assert.Equal(1, reader.GetInt32(0));
            }

            expressionDefinition.Verify(x => x.Id, Times.Once);
            expressionDefinition.Verify(x => x.Expression, Times.Once);
            expressionDefinition.Verify(x => x.CalculationPriority, Times.Once);
        }

        [Fact]
        public void RemoveById_IdExists_Success()
        {
            // Setup
            var id = Guid.NewGuid();
            const string EXPRESSION = "this is the expression";
            const int CALCULATION_PRIORITY = 123;

            var factory = new Mock<IExpressionDefinitionFactory>(MockBehavior.Strict);

            var repository = ExpressionDefinitionRepository.Create(
                Database,
                factory.Object);

            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "Expression", EXPRESSION},
                { "CalculationPriority", CALCULATION_PRIORITY },
            };

            using (var command = Database.CreateCommand(
                @"
                INSERT INTO
                    ExpressionDefinitions
                (
                    Id,
                    Expression,
                    CalculationPriority
                )
                VALUES
                (
                    @Id,
                    @Expression,
                    @CalculationPriority
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            // Execute
            repository.RemoveById(id);

            // Assert
            using (var reader = Database.Query("SELECT COUNT(1) FROM ExpressionDefinitions WHERE Id=@Id", "@Id", id))
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
            const string EXPRESSION = "this is the expression";
            const int CALCULATION_PRIORITY = 123;

            var expressionDefinition = new Mock<IExpressionDefinition>(MockBehavior.Strict);

            var factory = new Mock<IExpressionDefinitionFactory>(MockBehavior.Strict);
            factory
                .Setup(x => x.CreateExpressionDefinition(id, EXPRESSION, CALCULATION_PRIORITY))
                .Returns(expressionDefinition.Object);

            var repository = ExpressionDefinitionRepository.Create(
                Database,
                factory.Object);

            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "Expression", EXPRESSION},
                { "CalculationPriority", CALCULATION_PRIORITY },
            };

            using (var command = Database.CreateCommand(
                @"
                INSERT INTO
                    ExpressionDefinitions
                (
                    Id,
                    Expression,
                    CalculationPriority
                )
                VALUES
                (
                    @Id,
                    @Expression,
                    @CalculationPriority
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            // Execute
            var result = repository.GetById(id);

            // Assert
            Assert.Equal(expressionDefinition.Object, result);

            factory.Verify(
                x => x.CreateExpressionDefinition(
                    It.IsAny<Guid>(),
                    It.IsAny<string>(),
                    It.IsAny<int>()),
                Times.Once);
        }
        #endregion
    }
}