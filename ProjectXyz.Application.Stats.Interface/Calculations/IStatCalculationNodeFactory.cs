using System.Collections.Generic;

namespace ProjectXyz.Api.Stats.Calculations
{
    public interface IStatCalculationNodeFactory
    {
        bool TryCreate(
            string expression,
            IReadOnlyDictionary<string, IStatCalculationNode> termToCalculationNodeMapping,
            out IStatCalculationNode statCalculationNode);

    }
}