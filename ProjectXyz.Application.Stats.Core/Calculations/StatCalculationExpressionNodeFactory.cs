﻿using System.Collections.Generic;
using ProjectXyz.Application.Stats.Interface.Calculations;

namespace ProjectXyz.Application.Stats.Core.Calculations
{
    public sealed class StatCalculationExpressionNodeFactory : IStatCalculationNodeFactory
    {
        private readonly IStringExpressionEvaluator _stringExpressionEvaluator;

        public StatCalculationExpressionNodeFactory(IStringExpressionEvaluator stringExpressionEvaluator)
        {
            _stringExpressionEvaluator = stringExpressionEvaluator;
        }

        public bool TryCreate(
            string expression,
            IReadOnlyDictionary<string, IStatCalculationNode> termToCalculationNodeMapping,
            out IStatCalculationNode statCalculationNode)
        {
            statCalculationNode = new ExpressionStatCalculationNode(
                _stringExpressionEvaluator,
                expression,
                termToCalculationNodeMapping);
            return true;
        }
    }
}