using System.Collections.Generic;

namespace ProjectXyz.Api.Stats
{
    public interface IStatDefinitionToTermMappingRepository : IReadOnlyStatDefinitionToTermMappingRepository
    {
        void WriteStatDefinitionIdToTermMappings(IEnumerable<IStatDefinitionToTermMapping> statDefinitionToTermMappings);
    }
}