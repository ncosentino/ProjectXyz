using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Stats.ExtensionMethods;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public sealed class ExpressionEvaluator : IExpressionEvaluator
    {
        #region Fields
        private readonly Func<string, double> _evaluateExpressionCallback;
        #endregion

        #region Constructors
        private ExpressionEvaluator(Func<string, double> evaluateExpressionCallback)
        {
            _evaluateExpressionCallback = evaluateExpressionCallback;
        }
        #endregion

        #region Methods
        public static IExpressionEvaluator Create(Func<string, double> evaluateExpressionCallback)
        {
            Contract.Requires<ArgumentNullException>(evaluateExpressionCallback != null);
            Contract.Ensures(Contract.Result<IExpressionEvaluator>() != null);

            var expressionEvaluator = new ExpressionEvaluator(evaluateExpressionCallback);
            return expressionEvaluator;
        }

        public double Evaluate(IExpressionEnchantment expressionEnchantment, IStatCollection stats)
        {
            var expression = expressionEnchantment.Expression;
            
            foreach (var statExpressionId in expressionEnchantment.StatExpressionIds)
            {
                var statId = expressionEnchantment.GetStatIdForStatExpressionId(statExpressionId);
                var value = stats.GetValueOrDefault(statId, 0);
                expression = expression.Replace(statExpressionId, value.ToString(CultureInfo.InvariantCulture));
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
