using System.Collections.Generic;

namespace ProjectXyz.Api.Stats.Bounded
{
    public interface IStatDefinitionIdToBoundsMappingRepository
    {
        IEnumerable<IStatDefinitionIdToBoundsMapping> GetStatDefinitionIdToBoundsMappings();
    }
}
