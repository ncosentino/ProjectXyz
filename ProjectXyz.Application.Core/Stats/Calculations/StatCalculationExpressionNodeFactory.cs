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
        private readonly IStatDefinitionToCalculationLookup _statDefinitionToCalculationLookup;
        private readonly IReadOnlyDictionary<IIdentifier, string> _statDefinitionToTermMapping;

        public StatCalculationExpressionNodeFactory(
            IStringExpressionEvaluator stringExpressionEvaluator,
            IStatDefinitionToCalculationLookup statDefinitionToCalculationLookup,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionToTermMapping)
        {
            _stringExpressionEvaluator = stringExpressionEvaluator;
            _statDefinitionToCalculationLookup = statDefinitionToCalculationLookup;
            _statDefinitionToTermMapping = statDefinitionToTermMapping;
        }

        public bool TryCreate(
            IIdentifier statDefinitionId,
            string expression,
            out IStatCalculationNode statCalculationNode)
        {
            var newNodeStatDefinitionToTermMapping = _statDefinitionToTermMapping
                .Where(x => expression.Contains(x.Value))
                .Select(x => KeyValuePair.Create(
                    x.Value, 
                    _statDefinitionToCalculationLookup.GetCalculationNode(x.Key)))
                .ToDictionary();

            statCalculationNode = new ExpressionStatCalculationNode(
                _stringExpressionEvaluator,
                expression,
                newNodeStatDefinitionToTermMapping);
            return true;
        }
    }
}