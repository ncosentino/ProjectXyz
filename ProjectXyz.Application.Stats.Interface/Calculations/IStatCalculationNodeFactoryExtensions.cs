using System;
using System.Collections.Generic;

namespace ProjectXyz.Api.Stats.Calculations
{
    public static class IStatCalculationNodeFactoryExtensions
    {
        public static IStatCalculationNode Create(
            this IStatCalculationNodeFactory statCalculationNodeFactory,
            string expression,
            IReadOnlyDictionary<string, IStatCalculationNode> termToCalculationNodeMapping)
        {
            if (statCalculationNodeFactory.TryCreate(
                expression,
                termToCalculationNodeMapping,
                out var statCalculationNode))
            {
                return statCalculationNode;
            }

            throw new InvalidOperationException("Could not create IStatCalculationNode instance.");
        }
    }
}