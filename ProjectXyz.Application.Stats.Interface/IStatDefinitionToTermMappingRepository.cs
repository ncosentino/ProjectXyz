using System.Collections.Generic;

namespace ProjectXyz.Api.Stats
{
    public interface IStatDefinitionToTermMappingRepository
    {
        IEnumerable<IStatDefinitionToTermMapping> GetStatDefinitionIdToTermMappings();
    }
}