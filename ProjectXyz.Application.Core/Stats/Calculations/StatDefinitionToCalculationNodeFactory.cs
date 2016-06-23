using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;

namespace ProjectXyz.Application.Core.Stats.Calculations
{
    public sealed class StatDefinitionToCalculationNodeFactory
    {
        private readonly IStatCalculationNodeFactory _statCalculationNodeFactory;

        public StatDefinitionToCalculationNodeFactory(IStatCalculationNodeFactory statCalculationNodeFactory)
        {
            _statCalculationNodeFactory = statCalculationNodeFactory;
        }

        public IReadOnlyDictionary<IIdentifier, IStatCalculationNode> CreateMapping(
            IReadOnlyDictionary<IIdentifier, string> statDefinitionToTermMapping,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionToCalculationMapping)
        {
            return statDefinitionToCalculationMapping
                .Select(x =>
                    new KeyValuePair<IIdentifier, IStatCalculationNode>(
                        x.Key,
                        _statCalculationNodeFactory.Create(
                            x.Key,
                            x.Value,
                            statDefinitionToTermMapping,
                            statDefinitionToCalculationMapping)))
                .ToDictionary();
        }
    }
}