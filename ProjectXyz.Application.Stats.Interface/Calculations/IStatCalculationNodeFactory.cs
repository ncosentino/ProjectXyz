using System.Collections.Generic;

namespace ProjectXyz.Application.Stats.Interface.Calculations
{
    public interface IStatCalculationNodeFactory
    {
        bool TryCreate(
            string expression,
            IReadOnlyDictionary<string, IStatCalculationNode> termToCalculationNodeMapping,
            out IStatCalculationNode statCalculationNode);

    }
}