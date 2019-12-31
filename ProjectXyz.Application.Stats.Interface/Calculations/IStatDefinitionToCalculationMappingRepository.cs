using System.Collections.Generic;

namespace ProjectXyz.Api.Stats.Calculations
{
    public interface IStatDefinitionToCalculationMappingRepository
    {
        IEnumerable<IStatDefinitionToCalculationMapping> GetStatDefinitionIdToCalculationMappings();
    }
}