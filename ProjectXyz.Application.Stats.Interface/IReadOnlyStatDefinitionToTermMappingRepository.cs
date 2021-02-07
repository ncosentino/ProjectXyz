using System.Collections.Generic;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Stats
{
    public interface IReadOnlyStatDefinitionToTermMappingRepository
    {
        IEnumerable<IStatDefinitionToTermMapping> GetStatDefinitionIdToTermMappings();

        IStatDefinitionToTermMapping GetStatDefinitionToTermMappingByTerm(string term);

        IStatDefinitionToTermMapping GetStatDefinitionToTermMappingById(IIdentifier statDefinitionId);
    }
}