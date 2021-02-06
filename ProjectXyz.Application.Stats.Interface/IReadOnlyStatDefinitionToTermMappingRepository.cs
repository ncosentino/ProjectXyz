using System.Collections.Generic;

namespace ProjectXyz.Api.Stats
{
    public interface IReadOnlyStatDefinitionToTermMappingRepository
    {
        IEnumerable<IStatDefinitionToTermMapping> GetStatDefinitionIdToTermMappings();
    }
}