using System.Collections.Generic;
using ProjectXyz.Api.Stats.Calculations;

namespace ProjectXyz.Plugins.Stats.Calculations
{
    public sealed class ExpressionStatCalculationNode : IStatCalculationNode
    {
        private readonly IStringExpressionEvaluator _stringExpressionEvaluator;
        private readonly string _expression;
        private readonly IReadOnlyDictionary<string, IStatCalculationNode> _termToNodeMapping;

        public ExpressionStatCalculationNode(
            IStringExpressionEvaluator stringExpressionEvaluator,
            string expression,
            IReadOnlyDictionary<string, IStatCalculationNode> termToNodeMapping)
        {
            _stringExpressionEvaluator = stringExpressionEvaluator;
            _expression = expression;
            _termToNodeMapping = termToNodeMapping;
        }

        public double GetValue()
        {
            var expression = _expression;
            foreach (var termToNode in _termToNodeMapping)
            {
                var node = termToNode.Value;
                var termValue = node.GetValue();
                expression = expression.Replace(
                    termToNode.Key,
                    $"({termValue})");
            }

            var result = _stringExpressionEvaluator.Evaluate(expression);
            return result;
        }
    }
}