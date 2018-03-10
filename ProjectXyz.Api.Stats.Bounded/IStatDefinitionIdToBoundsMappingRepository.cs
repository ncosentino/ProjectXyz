using System.Collections.Generic;

namespace ProjectXyz.Plugins.Api.Stats.Bounded
{
    public interface IStatDefinitionIdToBoundsMappingRepository
    {
        IEnumerable<IStatDefinitionIdToBoundsMapping> GetStatDefinitionIdToBoundsMappings();
    }
}
