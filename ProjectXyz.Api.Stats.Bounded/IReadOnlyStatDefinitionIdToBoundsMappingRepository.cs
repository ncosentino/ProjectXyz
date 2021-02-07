using System.Collections.Generic;

namespace ProjectXyz.Plugins.Features.BoundedStats.Api
{
    public interface IReadOnlyStatDefinitionIdToBoundsMappingRepository
    {
        IEnumerable<IStatDefinitionIdToBoundsMapping> GetStatDefinitionIdToBoundsMappings();
    }
}
