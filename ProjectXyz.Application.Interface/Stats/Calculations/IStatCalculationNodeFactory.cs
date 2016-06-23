using System.Collections.Generic;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Stats.Calculations
{
    public interface IStatCalculationNodeFactory
    {
        IStatCalculationNode Create(
            IIdentifier statDefinitionId,
            string expression,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionToTermMapping,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionToCalculationMapping);
    }
}