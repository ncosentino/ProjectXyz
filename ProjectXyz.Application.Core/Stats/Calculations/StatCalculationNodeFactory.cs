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
        private readonly IStringExpressionEvaluator _stringExpressionEvaluator;
        private readonly Dictionary<IIdentifier, IStatCalculationNode> _statDefinitionToNodeCache;

        public StatCalculationNodeFactory(IStringExpressionEvaluator stringExpressionEvaluator)
        {
            _stringExpressionEvaluator = stringExpressionEvaluator;
            _statDefinitionToNodeCache = new Dictionary<IIdentifier, IStatCalculationNode>();
        }

        public IStatCalculationNode Create(
            IIdentifier statDefinitionId,
            string expression,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionToTermMapping,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionToCalculationMapping)
        {
            IStatCalculationNode cachedNode;
            if (_statDefinitionToNodeCache.TryGetValue(statDefinitionId, out cachedNode))
            {
                return cachedNode;
            }

            double expressionValue;
            if (double.TryParse(
                expression,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out expressionValue))
            {
                var node = new ValueStatCalculationNode(expressionValue);
                _statDefinitionToNodeCache[statDefinitionId] = node;
                return node;
            }

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

            var expressionNode = new ExpressionStatCalculationNode(
                _stringExpressionEvaluator,
                expression,
                newNodeStatDefinitionToTermMapping,
                newNodeStatDefinitionToNodeMapping);
            _statDefinitionToNodeCache[statDefinitionId] = expressionNode;
            return expressionNode;
        }
    }
}