using System;
using System.Collections.Generic;

namespace ProjectXyz.Application.Interface.Stats.Calculations
{
    public static class IStatCalculationNodeFactoryExtensions
    {
        public static IStatCalculationNode Create(
            this IStatCalculationNodeFactory statCalculationNodeFactory,
            string expression,
            IReadOnlyDictionary<string, IStatCalculationNode> termToCalculationNodeMapping)
        {
            IStatCalculationNode statCalculationNode;
            if (statCalculationNodeFactory.TryCreate(
                expression,
                termToCalculationNodeMapping,
                out statCalculationNode))
            {
                return statCalculationNode;
            }

            throw new InvalidOperationException("Could not create IStatCalculationNode instance.");
        }
    }
}