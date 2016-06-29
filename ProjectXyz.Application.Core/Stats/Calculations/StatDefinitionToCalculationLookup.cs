using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Core.Stats.Calculations
{
    public sealed class StatDefinitionToCalculationLookup : IStatDefinitionToCalculationLookup
    {
        private readonly IStatCalculationNodeFactory _statCalculationNodeFactory;
        private readonly IReadOnlyDictionary<IIdentifier, string> _statDefinitionToCalculationMapping;

        public StatDefinitionToCalculationLookup(
            IStatCalculationNodeFactory statCalculationNodeFactory,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionToCalculationMapping)
        {
            _statCalculationNodeFactory = statCalculationNodeFactory;
            _statDefinitionToCalculationMapping = statDefinitionToCalculationMapping;
        }

        public IStatCalculationNode GetCalculationNode(IIdentifier statDefinitionId)
        {
            return _statDefinitionToCalculationMapping.ContainsKey(statDefinitionId)
                ? _statCalculationNodeFactory.Create(
                    statDefinitionId,
                    _statDefinitionToCalculationMapping[statDefinitionId])
                : new ValueStatCalculationNode(0);
        }
    }
}