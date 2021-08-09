using System.Collections.Generic;

namespace ProjectXyz.Plugins.Features.Stats.Calculations
{
    public interface IReadOnlyStatDefinitionToCalculationMappingRepository
    {
        IEnumerable<IStatDefinitionToCalculationMapping> GetStatDefinitionIdToCalculationMappings();
    }
}