using System;
using System.Collections.Generic;
using System.Globalization;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Core.Stats.Calculations
{
    public sealed class StatCalculationValueNodeFactory : IStatCalculationNodeFactory
    {
        public bool TryCreate(
            IIdentifier statDefinitionId,
            string expression,
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
    }
}