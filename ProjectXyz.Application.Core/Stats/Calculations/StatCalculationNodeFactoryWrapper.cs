using System;
using System.Collections.Generic;
using ProjectXyz.Application.Interface.Stats.Calculations;

namespace ProjectXyz.Application.Core.Stats.Calculations
{
    public sealed class StatCalculationNodeFactoryWrapper : IStatCalculationNodeFactory
    {
        private readonly IReadOnlyCollection<IStatCalculationNodeFactory> _statCalculationNodeFactories;

        public StatCalculationNodeFactoryWrapper(IReadOnlyCollection<IStatCalculationNodeFactory> statCalculationNodeFactories)
        {
            _statCalculationNodeFactories = statCalculationNodeFactories;
        }

        public bool TryCreate(
            string expression,
            IReadOnlyDictionary<string, IStatCalculationNode> termToStatCalculationNodeMapping,
            out IStatCalculationNode statCalculationNode)
        {
            foreach (var factory in _statCalculationNodeFactories)
            {
                if (factory.TryCreate(
                    expression,
                    termToStatCalculationNodeMapping,
                    out statCalculationNode))
                {
                    return true;
                }
            }

            statCalculationNode = null;
            return false;
        }
    }
}