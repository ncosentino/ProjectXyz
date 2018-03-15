using System.Collections.Generic;

namespace ProjectXyz.Plugins.Features.BoundedStats.Api
{
    public interface IStatDefinitionIdToBoundsMappingRepository
    {
        IEnumerable<IStatDefinitionIdToBoundsMapping> GetStatDefinitionIdToBoundsMappings();
    }
}
