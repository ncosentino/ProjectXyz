using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Moq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Plugins.Enchantments.Expression.Tests.Unit
{
    [ApplicationLayer]
    [Enchantments]
    public class DataTableExpressionEvaluatorTests
    {
        #region Methods
        [Fact]
        public void Evaluate_ConstantTerm_ExpectedValue()
        {
            // Setup
            const double EXPECTED_RESULT = 5;
            string expression = EXPECTED_RESULT.ToString(CultureInfo.InvariantCulture);
            
            var dataTableExpressionEvaluator = DataTableExpressionEvaluator.Create();

            // Execute
            var result = dataTableExpressionEvaluator.Evaluate(expression);

            // Assert
            Assert.Equal(EXPECTED_RESULT, result);
        }

        [Fact]
        public void Evaluate_AdditionExpression_ExpectedValue()
        {
            // Setup
            const double EXPECTED_RESULT = 10;
            string expression = "8 + 2";

            var dataTableExpressionEvaluator = DataTableExpressionEvaluator.Create();

            // Execute
            var result = dataTableExpressionEvaluator.Evaluate(expression);

            // Assert
            Assert.Equal(EXPECTED_RESULT, result);
        }

        [Fact]
        public void Evaluate_MultiplicationExpression_ExpectedValue()
        {
            // Setup
            const double EXPECTED_RESULT = 10;
            string expression = "5 * 2";

            var dataTableExpressionEvaluator = DataTableExpressionEvaluator.Create();

            // Execute
            var result = dataTableExpressionEvaluator.Evaluate(expression);

            // Assert
            Assert.Equal(EXPECTED_RESULT, result);
        }

        [Fact]
        public void Evaluate_BadMathematicalExpression_ThrowsException()
        {
            // Setup
            string expression = "not a number";

            var dataTableExpressionEvaluator = DataTableExpressionEvaluator.Create();
            
            // Execute
            Assert.ThrowsDelegateWithReturn method = () => dataTableExpressionEvaluator.Evaluate(expression);

            // Assert
            Assert.Throws<FormatException>(method);
        }
        #endregion
    }
}