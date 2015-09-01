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
    public class ExpressionEnchantmentValueRepositoryTests : DatabaseTest
    {
        #region Methods
        [Fact]
        public void Add_ValidEnchantmentValue_Success()
        {
            // Setup
            var enchantmentValueId = Guid.NewGuid();
            var expressionEnchantmentId = Guid.NewGuid();
            const double VALUE = 123456;
            const string ID_FOR_EXPRESSION = "this is the id for the expression";
            
            var enchantmentValue = new Mock<IExpressionEnchantmentValue>(MockBehavior.Strict);
            enchantmentValue
                .Setup(x => x.Id)
                .Returns(enchantmentValueId);
            enchantmentValue
                .Setup(x => x.ExpressionEnchantmentId)
                .Returns(expressionEnchantmentId);
            enchantmentValue
                .Setup(x => x.Value)
                .Returns(VALUE);
            enchantmentValue
                .Setup(x => x.IdForExpression)
                .Returns(ID_FOR_EXPRESSION);

            var factory = new Mock<IExpressionEnchantmentValueFactory>(MockBehavior.Strict);
            
            var repository = ExpressionEnchantmentValueRepository.Create(
                Database,
                factory.Object);

            // Execute
            repository.Add(enchantmentValue.Object);

            // Assert
            using (var reader = Database.Query("SELECT COUNT(1) FROM ExpressionEnchantmentValues WHERE Id=@Id", "@Id", enchantmentValueId))
            {
                Assert.True(reader.Read(), "Expecting the reader to read a single row.");
                Assert.Equal(1, reader.GetInt32(0));
            }

            enchantmentValue.Verify(x => x.Id, Times.Once);
            enchantmentValue.Verify(x => x.ExpressionEnchantmentId, Times.Once);
            enchantmentValue.Verify(x => x.Value, Times.Once);
            enchantmentValue.Verify(x => x.IdForExpression, Times.Once);
        }

        [Fact]
        public void RemoveById_IdExists_Success()
        {
            // Setup
            var enchantmentValueId = Guid.NewGuid();
            var expressionEnchantmentId = Guid.NewGuid();
            const double VALUE = 123456;
            const string ID_FOR_EXPRESSION = "this is the id for the expression";
            
            var factory = new Mock<IExpressionEnchantmentValueFactory>(MockBehavior.Strict);
            
            var repository = ExpressionEnchantmentValueRepository.Create(
                Database,
                factory.Object);

            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", enchantmentValueId },
                { "expressionEnchantmentId", expressionEnchantmentId },
                { "Value", VALUE },
                { "IdForExpression", ID_FOR_EXPRESSION },
            };

            using (var command = Database.CreateCommand(
                @"
                INSERT INTO
                    ExpressionEnchantmentValues
                (
                    Id,
                    ExpressionEnchantmentId,
                    IdForExpression,
                    Value
                )
                VALUES
                (
                    @Id,
                    @ExpressionEnchantmentId,
                    @IdForExpression,
                    @Value
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            // Execute
            repository.RemoveById(enchantmentValueId);

            // Assert
            using (var reader = Database.Query("SELECT COUNT(1) FROM ExpressionEnchantments WHERE EnchantmentId=@Id", "@Id", enchantmentValueId))
            {
                Assert.True(reader.Read(), "Expecting the reader to read a single row.");
                Assert.Equal(0, reader.GetInt32(0));
            }
        }

        [Fact]
        public void GetById_IdExists_Success()
        {
            // Setup
            var enchantmentValueId = Guid.NewGuid();
            var expressionEnchantmentId = Guid.NewGuid();
            const double VALUE = 123456;
            const string ID_FOR_EXPRESSION = "this is the id for the expression";

            var enchantmentValue = new Mock<IExpressionEnchantmentValue>(MockBehavior.Strict);

            var factory = new Mock<IExpressionEnchantmentValueFactory>(MockBehavior.Strict);
            factory
                .Setup(x => x.CreateExpressionEnchantmentValue(enchantmentValueId, expressionEnchantmentId, ID_FOR_EXPRESSION, VALUE))
                .Returns(enchantmentValue.Object);
            
            var repository = ExpressionEnchantmentValueRepository.Create(
                Database,
                factory.Object);

            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", enchantmentValueId },
                { "expressionEnchantmentId", expressionEnchantmentId },
                { "Value", VALUE },
                { "IdForExpression", ID_FOR_EXPRESSION },
            };

            using (var command = Database.CreateCommand(
                @"
                INSERT INTO
                    ExpressionEnchantmentValues
                (
                    Id,
                    ExpressionEnchantmentId,
                    IdForExpression,
                    Value
                )
                VALUES
                (
                    @Id,
                    @ExpressionEnchantmentId,
                    @IdForExpression,
                    @Value
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            // Execute
            var result = repository.GetById(enchantmentValueId);

            // Assert
            Assert.Equal(enchantmentValue.Object, result);

            factory.Verify(
                x => x.CreateExpressionEnchantmentValue(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<string>(),
                    It.IsAny<double>()), 
                Times.Once);
        }
        #endregion
    }
}