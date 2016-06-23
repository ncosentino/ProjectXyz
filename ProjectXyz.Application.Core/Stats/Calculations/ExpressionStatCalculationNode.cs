using System.Collections.Generic;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Core.Stats.Calculations
{
    public sealed class ExpressionStatCalculationNode : IStatCalculationNode
    {
        private readonly IStringExpressionEvaluator _stringExpressionEvaluator;
        private readonly string _expression;
        private readonly IReadOnlyDictionary<IIdentifier, string> _statDefinitionToTermMapping;
        private readonly IReadOnlyDictionary<IIdentifier, IStatCalculationNode> _statDefinitionToNodeMapping;

        public ExpressionStatCalculationNode(
            IStringExpressionEvaluator stringExpressionEvaluator,
            string expression,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionToTermMapping,
            IReadOnlyDictionary<IIdentifier, IStatCalculationNode> statDefinitionToNodeMapping)
        {
            _stringExpressionEvaluator = stringExpressionEvaluator;
            _expression = expression;
            _statDefinitionToTermMapping = statDefinitionToTermMapping;
            _statDefinitionToNodeMapping = statDefinitionToNodeMapping;
        }

        public double GetValue()
        {
            var expression = _expression;
            foreach (var statDefinitionToTerm in _statDefinitionToTermMapping)
            {
                var termValue = _statDefinitionToNodeMapping[statDefinitionToTerm.Key].GetValue();
                expression = expression.Replace(
                    statDefinitionToTerm.Value,
                    $"({termValue})");
            }

            var result = _stringExpressionEvaluator.Evaluate(expression);
            return result;
        }
    }
}