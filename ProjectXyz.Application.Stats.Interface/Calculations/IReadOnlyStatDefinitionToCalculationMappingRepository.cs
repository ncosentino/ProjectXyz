using System.Collections.Generic;

namespace ProjectXyz.Api.Stats.Calculations
{
    public interface IReadOnlyStatDefinitionToCalculationMappingRepository
    {
        IEnumerable<IStatDefinitionToCalculationMapping> GetStatDefinitionIdToCalculationMappings();
    }
}