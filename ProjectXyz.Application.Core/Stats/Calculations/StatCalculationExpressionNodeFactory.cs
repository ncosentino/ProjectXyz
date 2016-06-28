using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;
using ProjectXyz.Framework.Shared;

namespace ProjectXyz.Application.Core.Stats.Calculations
{
    public sealed class StatCalculationExpressionNodeFactory : IStatCalculationNodeFactory
    {
        private readonly IStringExpressionEvaluator _stringExpressionEvaluator;
        private readonly IStatCalculationNodeFactory _statCalculationNodeFactory;
        private readonly IReadOnlyDictionary<IIdentifier, string> _statDefinitionToTermMapping;
        private readonly IReadOnlyDictionary<IIdentifier, string> _statDefinitionToCalculationMapping;

        public StatCalculationExpressionNodeFactory(
            IStatCalculationNodeFactory statCalculationNodeFactory,
            IStringExpressionEvaluator stringExpressionEvaluator,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionToTermMapping,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionToCalculationMapping)
        {
            _statCalculationNodeFactory = statCalculationNodeFactory;
            _stringExpressionEvaluator = stringExpressionEvaluator;
            _statDefinitionToTermMapping = statDefinitionToTermMapping;
            _statDefinitionToCalculationMapping = statDefinitionToCalculationMapping;
        }

        public bool TryCreate(
            IIdentifier statDefinitionId,
            string expression,
            out IStatCalculationNode statCalculationNode)
        {
            var newNodeStatDefinitionToTermMapping = _statDefinitionToTermMapping
                .Where(x => expression.Contains(x.Value))
                .ToDictionary();
            var newNodeStatDefinitionToNodeMapping = newNodeStatDefinitionToTermMapping
                .Select(x => KeyValuePair.Create(
                    x.Key,
                    _statCalculationNodeFactory.Create(
                        x.Key,
                        _statDefinitionToCalculationMapping.GetValueOrDefault(x.Key, "0"))))
                .ToDictionary();

            statCalculationNode = new ExpressionStatCalculationNode(
                _stringExpressionEvaluator,
                expression,
                newNodeStatDefinitionToTermMapping,
                newNodeStatDefinitionToNodeMapping);
            return true;
        }
    }
}