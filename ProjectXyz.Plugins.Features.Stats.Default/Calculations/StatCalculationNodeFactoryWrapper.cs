using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Stats.Calculations;

namespace ProjectXyz.Plugins.Features.Stats.Default.Calculations
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