using System.Collections.Generic;

namespace ProjectXyz.Application.Interface.Stats.Calculations
{
    public interface IStatCalculationNodeFactory
    {
        bool TryCreate(
            string expression,
            IReadOnlyDictionary<string, IStatCalculationNode> termToCalculationNodeMapping,
            out IStatCalculationNode statCalculationNode);

    }
}