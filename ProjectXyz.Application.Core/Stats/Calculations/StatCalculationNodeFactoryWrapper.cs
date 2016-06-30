using System;
using System.Collections.Generic;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Core.Stats.Calculations
{
    public sealed class StatCalculationNodeFactoryWrapper : IStatCalculationNodeFactory
    {
        private readonly IStatCalculationNodeFactoryProvider _statCalculationNodeFactoryProvider;
        
        public StatCalculationNodeFactoryWrapper(IStatCalculationNodeFactoryProvider statCalculationNodeFactoryProvider)
        {
            _statCalculationNodeFactoryProvider = statCalculationNodeFactoryProvider;
        }

        public bool TryCreate(
            IIdentifier statDefinitionId,
            string expression,
            out IStatCalculationNode statCalculationNode)
        {
            foreach (var factory in _statCalculationNodeFactoryProvider.Factories)
            {
                if (factory.TryCreate(
                    statDefinitionId,
                    expression,
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