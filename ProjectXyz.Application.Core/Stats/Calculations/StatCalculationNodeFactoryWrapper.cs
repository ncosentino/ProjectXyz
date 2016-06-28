using System;
using System.Collections.Generic;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Core.Stats.Calculations
{
    public sealed class StatCalculationNodeFactoryWrapper : IStatCalculationNodeFactory
    {
        private readonly IStatCalculationNodeFactoryProvider _statCalculationNodeFactoryProvider;
        private readonly IStatCalculationNodeWrapper _statCalculationNodeWrapper;

        public StatCalculationNodeFactoryWrapper(
            IStatCalculationNodeFactoryProvider statCalculationNodeFactoryProvider,
            IStatCalculationNodeWrapper statCalculationNodeWrapper)
        {
            _statCalculationNodeFactoryProvider = statCalculationNodeFactoryProvider;
            _statCalculationNodeWrapper = statCalculationNodeWrapper;
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
                    statCalculationNode = _statCalculationNodeWrapper.Wrap(
                        statDefinitionId,
                        statCalculationNode);
                    return true;
                }
            }

            statCalculationNode = null;
            return false;
        }
    }
}