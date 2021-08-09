using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using ProjectXyz.Plugins.Features.Stats.Calculations;

namespace ProjectXyz.Plugins.Features.Stats.Default.Calculations
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

            if (!double.TryParse(
                expression,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out var expressionValue))
            {
                return false;
            }

            statCalculationNode = new ValueStatCalculationNode(expressionValue);
            return true;
        }
    }
}