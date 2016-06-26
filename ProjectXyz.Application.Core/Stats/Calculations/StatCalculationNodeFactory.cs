using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;
using ProjectXyz.Framework.Shared;

namespace ProjectXyz.Application.Core.Stats.Calculations
{
    public sealed class StatCalculationNodeFactory : IStatCalculationNodeFactory
    {
        private readonly Func<IIdentifier, IStatCalculationNode, IStatCalculationNode> _wrapperCallback;
        private readonly IStringExpressionEvaluator _stringExpressionEvaluator;
        private readonly IReadOnlyList<TryMakeStatCalculationNodeDelegate> _tryMakeStatCalculationNodeMethods;

        public StatCalculationNodeFactory(
            IStringExpressionEvaluator stringExpressionEvaluator,
            Func<IIdentifier, IStatCalculationNode, IStatCalculationNode> wrapperCallback)
        {
            _stringExpressionEvaluator = stringExpressionEvaluator;
            _wrapperCallback = wrapperCallback;

            _tryMakeStatCalculationNodeMethods = new List<TryMakeStatCalculationNodeDelegate>()
            {
                TryMakeValueNode,
                TryMakeExpressionNode
            };
        }

        public IStatCalculationNode Create(
            IIdentifier statDefinitionId,
            string expression,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionToTermMapping,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionToCalculationMapping)
        {
            foreach (var tryMakeStatCalculationNodeDelegate in _tryMakeStatCalculationNodeMethods)
            {
                IStatCalculationNode statCalculationNode;
                if (tryMakeStatCalculationNodeDelegate(
                    statDefinitionId,
                    expression,
                    statDefinitionToTermMapping,
                    statDefinitionToCalculationMapping,
                    out statCalculationNode))
                {
                    statCalculationNode = _wrapperCallback(
                        statDefinitionId, 
                        statCalculationNode);
                    return statCalculationNode;
                }
            }

            throw new InvalidOperationException();
        }

        private bool TryMakeValueNode(
            IIdentifier statDefinitionId,
            string expression,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionToTermMapping,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionToCalculationMapping,
            out IStatCalculationNode statCalculationNode)
        {
            double expressionValue;
            if (double.TryParse(
                expression,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out expressionValue))
            {
                statCalculationNode = new ValueStatCalculationNode(expressionValue);
                return true;
            }

            statCalculationNode = null;
            return false;
        }

        private bool TryMakeExpressionNode(
            IIdentifier statDefinitionId,
            string expression,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionToTermMapping,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionToCalculationMapping,
            out IStatCalculationNode statCalculationNode)
        {
            var newNodeStatDefinitionToTermMapping = statDefinitionToTermMapping
                .Where(x => expression.Contains(x.Value))
                .ToDictionary();
            var newNodeStatDefinitionToNodeMapping = newNodeStatDefinitionToTermMapping
                .Select(x => KeyValuePair.Create(
                    x.Key,
                    Create(
                        x.Key,
                        statDefinitionToCalculationMapping.GetValueOrDefault(x.Key, "0"),
                        statDefinitionToTermMapping,
                        statDefinitionToCalculationMapping)))
                .ToDictionary();

            statCalculationNode = new ExpressionStatCalculationNode(
                _stringExpressionEvaluator,
                expression,
                newNodeStatDefinitionToTermMapping,
                newNodeStatDefinitionToNodeMapping,
                _wrapperCallback);
            return true;
        }

        private delegate bool TryMakeStatCalculationNodeDelegate(
            IIdentifier statDefinitionId,
            string expression,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionToTermMapping,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionToCalculationMapping,
            out IStatCalculationNode statCalculationNode);
    }
}