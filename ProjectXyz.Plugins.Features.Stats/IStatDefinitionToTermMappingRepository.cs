using System.Collections.Generic;

namespace ProjectXyz.Plugins.Features.Stats
{
    public interface IStatDefinitionToTermMappingRepository : IReadOnlyStatDefinitionToTermMappingRepository
    {
        void WriteStatDefinitionIdToTermMappings(IEnumerable<IStatDefinitionToTermMapping> statDefinitionToTermMappings);
    }
}