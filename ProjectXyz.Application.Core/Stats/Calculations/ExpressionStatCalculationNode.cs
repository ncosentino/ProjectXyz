using System;
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
        private readonly Func<IIdentifier, IStatCalculationNode, IStatCalculationNode> _wrapperCallback;

        public ExpressionStatCalculationNode(
            IStringExpressionEvaluator stringExpressionEvaluator,
            string expression,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionToTermMapping,
            IReadOnlyDictionary<IIdentifier, IStatCalculationNode> statDefinitionToNodeMapping,
            Func<IIdentifier, IStatCalculationNode, IStatCalculationNode> wrapperCallback)
        {
            _stringExpressionEvaluator = stringExpressionEvaluator;
            _expression = expression;
            _statDefinitionToTermMapping = statDefinitionToTermMapping;
            _statDefinitionToNodeMapping = statDefinitionToNodeMapping;
            _wrapperCallback = wrapperCallback;
        }

        public double GetValue()
        {
            var expression = _expression;
            foreach (var statDefinitionToTerm in _statDefinitionToTermMapping)
            {
                var node = _statDefinitionToNodeMapping[statDefinitionToTerm.Key];
                node = _wrapperCallback(statDefinitionToTerm.Key, node);
                var termValue = node.GetValue();
                expression = expression.Replace(
                    statDefinitionToTerm.Value,
                    $"({termValue})");
            }

            var result = _stringExpressionEvaluator.Evaluate(expression);
            return result;
        }
    }
}