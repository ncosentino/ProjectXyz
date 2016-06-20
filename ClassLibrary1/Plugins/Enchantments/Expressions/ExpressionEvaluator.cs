using System;
using System.Globalization;
using ClassLibrary1.Application.Interface.Stats;

namespace ClassLibrary1.Plugins.Enchantments.Expressions
{
    public sealed class ExpressionEvaluator : IExpressionEvaluator
    {
        #region Fields
        private readonly Func<string, double> _evaluateExpressionCallback;
        #endregion

        #region Constructors
        public ExpressionEvaluator(Func<string, double> evaluateExpressionCallback)
        {
            _evaluateExpressionCallback = evaluateExpressionCallback;
        }
        #endregion

        #region Methods
        public double Evaluate(
            IExpressionEnchantment expressionEnchantment,
            IStatCollection stats)
        {
            var expression = expressionEnchantment.Expression;

            foreach (var statExpressionId in expressionEnchantment.StatExpressionIds)
            {
                var statDefinitionId = expressionEnchantment.GetStatDefinitionIdForStatExpressionId(statExpressionId);
                var value = stats.GetValueOrDefault(statDefinitionId, 0);
                expression = expression.Replace(
                    statExpressionId, 
                    value.ToString(CultureInfo.InvariantCulture));
            }

            foreach (var valueExpressionId in expressionEnchantment.ValueExpressionIds)
            {
                var value = expressionEnchantment.GetValueForValueExpressionId(valueExpressionId);
                expression = expression.Replace(valueExpressionId, value.ToString(CultureInfo.InvariantCulture));
            }

            var newValue = _evaluateExpressionCallback.Invoke(expression);
            return newValue;
        }
        #endregion
    }
}
