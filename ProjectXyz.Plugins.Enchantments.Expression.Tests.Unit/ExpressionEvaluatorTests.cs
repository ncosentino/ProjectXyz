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
    public class ExpressionEvaluatorTests
    {
        #region Methods
        [Fact]
        public void Evaluate_SingleValueExpression_ExpectedResult()
        {
            // Setup
            const double VALUE = 10d;
            const string VALUE_IDENTIFIER = "Value";
            const string INITIAL_EXPRESSION = VALUE_IDENTIFIER;
            var finalExpression = INITIAL_EXPRESSION.Replace(VALUE_IDENTIFIER, VALUE.ToString(CultureInfo.InvariantCulture));

            var expressionEnchantment = new Mock<IExpressionEnchantment>(MockBehavior.Strict);
            expressionEnchantment
                .Setup(x => x.Expression)
                .Returns(INITIAL_EXPRESSION);
            expressionEnchantment
                .Setup(x => x.StatExpressionIds)
                .Returns(Enumerable.Empty<string>());
            expressionEnchantment
                .Setup(x => x.ValueExpressionIds)
                .Returns(new[] { VALUE_IDENTIFIER });
            expressionEnchantment
                .Setup(x => x.GetValueForValueExpressionId(VALUE_IDENTIFIER))
                .Returns(VALUE);

            var stats = new Mock<IStatCollection>(MockBehavior.Strict);

            var evaluateCallback = new Mock<Func<string, double>>(MockBehavior.Strict);
            evaluateCallback
                .Setup(x => x(finalExpression))
                .Returns(VALUE);

            var expressionEvaluator = ExpressionEvaluator.Create(evaluateCallback.Object);

            // Execute
            var result = expressionEvaluator.Evaluate(
                expressionEnchantment.Object,
                stats.Object);

            // Assert
            Assert.Equal(VALUE, result);

            expressionEnchantment.Verify(x => x.Expression, Times.Once);
            expressionEnchantment.Verify(x => x.StatExpressionIds, Times.Once);
            expressionEnchantment.Verify(x => x.ValueExpressionIds, Times.Once);
            expressionEnchantment.Verify(x => x.GetValueForValueExpressionId(It.IsAny<string>()), Times.Once);

            evaluateCallback.Verify(x => x(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Evaluate_SingleStatExpression_ExpectedResult()
        {
            // Setup
            const double STAT_VALUE = 10d;
            var statId = Guid.NewGuid();
            const string STAT_IDENTIFIER = "Stat";
            const string INITIAL_EXPRESSION = STAT_IDENTIFIER;
            var finalExpression = INITIAL_EXPRESSION.Replace(STAT_IDENTIFIER, STAT_VALUE.ToString(CultureInfo.InvariantCulture));

            var expressionEnchantment = new Mock<IExpressionEnchantment>(MockBehavior.Strict);
            expressionEnchantment
                .Setup(x => x.Expression)
                .Returns(INITIAL_EXPRESSION);
            expressionEnchantment
                .Setup(x => x.StatExpressionIds)
                .Returns(new[] { STAT_IDENTIFIER });
            expressionEnchantment
                .Setup(x => x.ValueExpressionIds)
                .Returns(Enumerable.Empty<string>());
            expressionEnchantment
                .Setup(x => x.GetStatIdForStatExpressionId(STAT_IDENTIFIER))
                .Returns(statId);

            var stat = new Mock<IStat>(MockBehavior.Strict);
            stat
                .Setup(x => x.Value)
                .Returns(STAT_VALUE);

            var stats = new Mock<IStatCollection>(MockBehavior.Strict);
            stats
                .Setup(x => x.Contains(statId))
                .Returns(true);
            stats
                .Setup(x => x[statId])
                .Returns(stat.Object);

            var evaluateCallback = new Mock<Func<string, double>>(MockBehavior.Strict);
            evaluateCallback
                .Setup(x => x(finalExpression))
                .Returns(STAT_VALUE);

            var expressionEvaluator = ExpressionEvaluator.Create(evaluateCallback.Object);

            // Execute
            var result = expressionEvaluator.Evaluate(
                expressionEnchantment.Object,
                stats.Object);

            // Assert
            Assert.Equal(STAT_VALUE, result);

            expressionEnchantment.Verify(x => x.Expression, Times.Once);
            expressionEnchantment.Verify(x => x.StatExpressionIds, Times.Once);
            expressionEnchantment.Verify(x => x.ValueExpressionIds, Times.Once);
            expressionEnchantment.Verify(x => x.GetStatIdForStatExpressionId(It.IsAny<string>()), Times.Once);

            stat.Verify(x => x.Value, Times.Once);

            stats.Verify(x => x.Contains(statId), Times.Once);
            stats.Verify(x => x[statId], Times.Once);

            evaluateCallback.Verify(x => x(It.IsAny<string>()), Times.Once);
        }
        #endregion
    }
}