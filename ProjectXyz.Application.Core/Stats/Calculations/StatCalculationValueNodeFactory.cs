using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ProjectXyz.Application.Interface.Stats.Calculations;

namespace ProjectXyz.Application.Core.Stats.Calculations
{
    public sealed class StatCalculationValueNodeFactory : IStatCalculationNodeFactory
    {
        public bool TryCreate(
            string expression,
            IReadOnlyDictionary<string, IStatCalculationNode> termToCalculationNodeMapping,
            out IStatCalculationNode statCalculationNode)
        {
            statCalculationNode = null;

            if (termToCalculationNodeMapping.Any())
            {
                return false;
            }

            double expressionValue;
            if (!double.TryParse(
                expression,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out expressionValue))
            {
                return false;
            }

            statCalculationNode = new ValueStatCalculationNode(expressionValue);
            return true;
        }
    }
}